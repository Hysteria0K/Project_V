using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.UIElements;
using System;
using UnityEngine.TextCore.Text;

public class JsonReader : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
    public class NameList_Attributes
    {
        public int Index;
        public string FirstName;
        public string LastName;
    }

    [System.Serializable]
    public class ArmyUnit_Attributes
    {
        public int Index;
        public string Regiment;
        public string Battalion;
        public string APO;
        public string Forces;
    }

    [System.Serializable]
    public class Militaryrank_Attributes
    {
        public int Index;
        public string Rank;
        public int Ratio;
    }

    [System.Serializable]
    public class PostStamp_Attributes
    {
        public int Index;
        public string Sprite;
        public int Ratio;
        public bool Valid;
    }

    [System.Serializable]
    public class Dialogue_Attributes
    {
        public string Id; // 사용될 씬 이름
        public int Index; // Index
        public string Type; // main_talk = 옆에 얼굴 나오는 대화 , talk = 옆에 얼굴 안나오는 대화 , order 단순 명령어 처리
        public bool Set; // True 일 경우 해당 인덱스 바로 아래 열도 동시에 처리, 비워두는 걸로 구분
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

    [System.Serializable]
    public class Rulebook_Attributes
    {
        public string Id;
        public int Order;
        public string Type;
        public string Value1;
        public string Value2;
        public string Value3;
        public string Value4;
        public string Value5;
        public string Value6;
    }

    [System.Serializable]
    public class GameLevel_Attributes
    {
        public int Day;
        public int Letter_Count_Max;
        public float Limit_Time;
        public bool Is_Dead;
        public int Dead_Count;
        public int Dead_Percentage;
        public bool Is_Invalid_Stamp;
        public string Start_Story_Id;
        public string End_Story_Id;
        public string Letter_Id;
        public string Goal;

    }

    [System.Serializable]
    public class Settings_Attributes
    {
        public float Text_Delay;
        public float Wait_Delay;
        public float Min_Text;
        public float Max_Text;
        public float Increase_Height;
        public float Dialogue_Delay;
        public float L_Spawn_Y;
        public float L_Move_Speed;
        public float Telephone_Delay;
    }

    [System.Serializable]

    public class Character_Attributes
    {
        public int Index;
        public string Name;
        public string Face_Sprite;
        public string Face_Sprite_Mono;
        public string Standing_Sprite;
    }
    #endregion Attributes

    #region Parse
    public class NameList_Parse
    {
        public NameList_Attributes[] namelist;
    }

    public class ArmyUnit_Parse
    {
        public ArmyUnit_Attributes[] armyunit;
    }

    public class Rank_Parse
    {
        public Militaryrank_Attributes[] militaryrank;
    }

    public class PostStamp_Parse
    {
        public PostStamp_Attributes[] poststamp;
    }
    
    public class Dialogue_Parse
    {
        public Dialogue_Attributes[] dialogue;
    }

    public class RulebookData_Parse
    {
        public Rulebook_Attributes[] rulebookdata;
    }

    public class GameLevel_Parse
    {
        public GameLevel_Attributes[] gamelevel;
    }

    public class Settings_Parse
    {
        public Settings_Attributes[] settings;
    }

    public class Character_Parse
    {
        public Character_Attributes[] character;
    }
    #endregion Parse

    public NameList_Parse NameList;
    public ArmyUnit_Parse ArmyUnit;
    public Rank_Parse Rank;
    public PostStamp_Parse PostStamp;
    public Dialogue_Parse Dialogue;
    public RulebookData_Parse RulebookData;
    public GameLevel_Parse GameLevel;
    public Settings_Parse Settings;
    public Character_Parse Character;

    public Dictionary<string, Dictionary<int, Dialogue_Attributes>> Dialogue_Dictionary;
    public Dictionary<string, Dictionary<int, Rulebook_Attributes>> Rulebook_Dictionary;
    public Dictionary<int, GameLevel_Attributes> GameLevel_Dictionary;
    public Dictionary<string, Character_Attributes> Character_Dictionary;

    private void Awake()
    {
        NameList = JsonUtility.FromJson<NameList_Parse>(ReadJson("namelist"));
        ArmyUnit = JsonUtility.FromJson<ArmyUnit_Parse>(ReadJson("armyunit"));
        Rank = JsonUtility.FromJson<Rank_Parse>(ReadJson("militaryrank"));
        PostStamp = JsonUtility.FromJson<PostStamp_Parse>(ReadJson("poststamp"));
        Dialogue = JsonUtility.FromJson<Dialogue_Parse>(ReadJson("dialogue"));
        RulebookData = JsonUtility.FromJson<RulebookData_Parse>(ReadJson("rulebookdata"));
        GameLevel = JsonUtility.FromJson<GameLevel_Parse>(ReadJson("gamelevel"));
        Settings = JsonUtility.FromJson<Settings_Parse>(ReadJson("gamesettings"));
        Character = JsonUtility.FromJson<Character_Parse>(ReadJson("character"));

        Dialogue_Dictionary = new Dictionary<string, Dictionary<int, Dialogue_Attributes>>();
        Dialogue_To_Dictionary();

        Rulebook_Dictionary = new Dictionary<string, Dictionary<int, Rulebook_Attributes>>();
        RulebookData_To_Dictionary();

        GameLevel_Dictionary = new Dictionary<int, GameLevel_Attributes>();
        GameLevel_To_Dictionary();

        Character_Dictionary = new Dictionary<string, Character_Attributes>();
        Character_To_Dictionary();

        Debug.Log("JsonParse Complete");
        //Debug.Log(Rulebook_Dictionary["BaseBook"][6].Value1);
    }

    void Start()
    {
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


    #region 배열, 자료형으로 변환
    private void Dialogue_To_Dictionary()
    {
        Dictionary<int, Dialogue_Attributes> Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();

        string Saved_ID = Dialogue.dialogue[0].Id;

        for (int i = 0; i < Dialogue.dialogue.Length; i++)
        {
            if (Saved_ID != Dialogue.dialogue[i].Id)
            {
                //Debug.Log("변환");
                Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);
                Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();
                Saved_ID = Dialogue.dialogue[i].Id;
            }

            Temp_dictionary.Add(Dialogue.dialogue[i].Index, Dialogue.dialogue[i]);
        }

        Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);
    }

    private void RulebookData_To_Dictionary()
    {
        Dictionary<int, Rulebook_Attributes> Temp_dictionary = new Dictionary<int, Rulebook_Attributes>();

        string Saved_ID = RulebookData.rulebookdata[0].Id;

        for (int i = 0; i < RulebookData.rulebookdata.Length; i++)
        {
            if (Saved_ID != RulebookData.rulebookdata[i].Id.ToString())
            {
                Rulebook_Dictionary.Add(Saved_ID, Temp_dictionary);
                Temp_dictionary = new Dictionary<int, Rulebook_Attributes>();
                Saved_ID = RulebookData.rulebookdata[i].Id;
            }

            Temp_dictionary.Add(RulebookData.rulebookdata[i].Order, RulebookData.rulebookdata[i]);
        }

        Rulebook_Dictionary.Add(Saved_ID, Temp_dictionary);
    }

    private void GameLevel_To_Dictionary()
    {
        for (int i = 0; i < GameLevel.gamelevel.Length; i++)
        {
            GameLevel_Dictionary.Add(GameLevel.gamelevel[i].Day, GameLevel.gamelevel[i]);
        }
    }
    private void Character_To_Dictionary()
    {
        for (int i = 0; i < Character.character.Length; i++)
        {
            Character_Dictionary.Add(Character.character[i].Name, Character.character[i]);
        }
    }
    #endregion 배열, 자료형으로 변환
}