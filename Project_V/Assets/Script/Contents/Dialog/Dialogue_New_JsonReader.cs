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
        public string Id; // ���� �� �̸�
        public int Index; // Index
        public string Type; // main_talk = ���� �� ������ ��ȭ , talk = ���� �� �ȳ����� ��ȭ , order �ܼ� ��ɾ� ó��
        public bool Set; // True �� ��� �ش� �ε��� �ٷ� �Ʒ� ���� ���ÿ� ó��
        public bool Next; // �ش� �۾��� ������ ���� �ε����� �̵� Ȱ��ȭ
        public string Name; // ��ȭ ��ü �̸� ���
        public string Text; // ��ȭ ��ũ��Ʈ
        public string MainTalk_Sprite; // Type�� main_talk �� ��� ��µ� ��������Ʈ
        public string Cmd; // fade_out , fade_in , rumbling (ȭ�� Ȯ��Ǿ��ٰ� ��ҵǾ��ٰ� �� ���� �߰� ����) 
        public string Cmd_Target; // fade_in �� �� ����� �� ���
        public bool Move; // True�� ��� ������ �����̱�, False�� �ڷ���Ʈ
        public float Move_X; // ��ǥ ��ġ
        public float Move_Y; // ��ǥ ��ġ
        public float Move_Spd; // �����̴� �ӵ�
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
                //Debug.Log("��ȯ");
                Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);
                Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();
                Saved_ID = Dialogue.dialogue_new[i].Id;
            }

            Temp_dictionary.Add(Dialogue.dialogue_new[i].Index, Dialogue.dialogue_new[i]);
        }

        Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);

    }
}