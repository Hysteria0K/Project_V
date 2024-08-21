using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.UIElements;
using System;

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
        public string Id;
        public int Index;
        public string Name;
        public string Text;
        public string Sprite1;
        public int Pos1;
        public int Layer1;
        public string Sprite2;
        public int Pos2;
        public int Layer2;
        public string Sprite3;
        public int Pos3;
        public int Layer3;
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
    #endregion Parse

    public NameList_Parse NameList;
    public ArmyUnit_Parse ArmyUnit;
    public Rank_Parse Rank;
    public PostStamp_Parse PostStamp;
    public Dialogue_Parse Dialogue;
    public RulebookData_Parse RulebookData;
    public GameLevel_Parse GameLevel;
    public Settings_Parse Settings;

    public Dictionary<string, Dictionary<int, Dialogue_Attributes>> Dialogue_Dictionary;
    public Dictionary<string, Dictionary<int, Rulebook_Attributes>> Rulebook_Dictionary;
    public Dictionary<int, GameLevel_Attributes> GameLevel_Dictionary;

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

        Dialogue_Dictionary = new Dictionary<string, Dictionary<int, Dialogue_Attributes>>();
        Dialogue_To_Dictionary();

        Rulebook_Dictionary = new Dictionary<string, Dictionary<int, Rulebook_Attributes>>();
        RulebookData_To_Dictionary();

        GameLevel_Dictionary = new Dictionary<int, GameLevel_Attributes>();
        GameLevel_To_Dictionary();

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
        string JsonText = File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json");

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
    #endregion 배열, 자료형으로 변환
}