using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Dialogue_New_JsonReader : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
    public class Dialogue_Attributes
    {
        public string Id; // 사용될 씬 이름
        public int Index; // Index
        public string Type; // main_talk = 옆에 얼굴 나오는 대화 , talk = 옆에 얼굴 안나오는 대화 , order 단순 명령어 처리
        public bool Set; // True 일 경우 해당 인덱스 바로 아래 열도 동시에 처리
        public bool Next; // 해당 작업이 끝나면 다음 인덱스로 이동 활성화
        public string Name; // 대화 주체 이름 출력
        public string Text; // 대화 스크립트
        public string MainTalk_Sprite; // Type이 main_talk 일 경우 출력될 스프라이트
        public string Cmd; // fade_out , fade_in , rumbling (화면 확대되었다가 축소되었다가 그 연출 추가 예정) 
        public string Cmd_Target; // fade_in 될 때 대상이 될 배경
        public bool Move; // True일 경우 서서히 움직이기, False면 텔레포트
        public float Move_X; // 목표 위치
        public float Move_Y; // 목표 위치
        public float Move_Spd; // 움직이는 속도
    }
        #endregion

    #region Parse
    public class Dialogue_Parse
    {
        public Dialogue_Attributes[] dialogue_new;
    }

    #endregion

    public Dialogue_Parse Dialogue;

    public Dictionary<string, Dictionary<int, Dialogue_Attributes>> Dialogue_Dictionary;

    private void Awake()
    {
        Dialogue = JsonUtility.FromJson<Dialogue_Parse>(ReadJson("dialogue_new"));

        Dialogue_Dictionary = new Dictionary<string, Dictionary<int, Dialogue_Attributes>>();
        Dialogue_To_Dictionary();

        Debug.Log("JsonParse Complete");
    }
    private string ReadJson(string Filename)
    {
#if UNITY_EDITOR
        string JsonText = File.ReadAllText("Assets/StreamingAssets/resource/test/" + Filename + ".json");

        return (JsonText);
#else
        string JsonText = AESUtil.Decrypt(File.ReadAllText(Application.persistentDataPath + "/resource/data/" + Filename + ".json"), "0123456789ABCDEF0123456789ABCDEF", "ABCDEF0123456789");

        return (JsonText);
#endif
    }
    private void Dialogue_To_Dictionary()
    {
        Dictionary<int, Dialogue_Attributes> Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();

        string Saved_ID = Dialogue.dialogue_new[0].Id;

        for (int i = 0; i < Dialogue.dialogue_new.Length; i++)
        {
            if (Saved_ID != Dialogue.dialogue_new[i].Id)
            {
                //Debug.Log("변환");
                Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);
                Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();
                Saved_ID = Dialogue.dialogue_new[i].Id;
            }

            Temp_dictionary.Add(Dialogue.dialogue_new[i].Index, Dialogue.dialogue_new[i]);
        }

        Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);

    }
}