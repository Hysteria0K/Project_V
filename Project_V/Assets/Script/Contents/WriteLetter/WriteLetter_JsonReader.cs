using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.UIElements;
using System;
using UnityEngine.TextCore.Text;

public class WriteLetter_JsonReader : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
    public class WriteLetter_Attributes
    {
        public string Id;
        public int Turn;
        public int Order;
        public string Text;
        public string Tag1;
        public string Tag2;
        public string Tag3;
        public bool Masterpiece;
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
    }

    #endregion Attributes

    #region Parse
    public class WriteLetter_Parse
    {
        public WriteLetter_Attributes[] writeletter;
    }
    public class GameLevel_Parse
    {
        public GameLevel_Attributes[] gamelevel;
    }

    #endregion Parse

    public WriteLetter_Parse WriteLetter;
    public GameLevel_Parse GameLevel;

    public Dictionary<string, Dictionary<int, Dictionary<int, WriteLetter_Attributes>>> WriteLetter_Dictionary;
    public Dictionary<int, GameLevel_Attributes> GameLevel_Dictionary;

    private void Awake()
    {
        WriteLetter = JsonUtility.FromJson<WriteLetter_Parse>(ReadJson("writeletter"));
        GameLevel = JsonUtility.FromJson<GameLevel_Parse>(ReadJson("gamelevel"));

        WriteLetter_Dictionary = new Dictionary<string, Dictionary<int, Dictionary<int, WriteLetter_Attributes>>>();
        WriteLetter_To_Dictionary();

        GameLevel_Dictionary = new Dictionary<int, GameLevel_Attributes>();
        GameLevel_To_Dictionary();

        Debug.Log("JsonParse Complete");
        //Debug.Log(WriteLetter_Dictionary[1][3][1].Tag1); //WriteLetter_Dictionary[Id][Turn][Order].<Attributes>
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
    private void WriteLetter_To_Dictionary()
    {
        Dictionary<int, Dictionary<int, WriteLetter_Attributes>> Id_Dictionary = new Dictionary<int, Dictionary<int, WriteLetter_Attributes>>();
        Dictionary<int, WriteLetter_Attributes> Turn_Dictionary = new Dictionary<int, WriteLetter_Attributes>();

        string Saved_Id = WriteLetter.writeletter[0].Id;
        int Saved_Turn = WriteLetter.writeletter[0].Turn;

        for (int i = 0; i < WriteLetter.writeletter.Length; i++)
        {
            if (Saved_Id != WriteLetter.writeletter[i].Id)
            {
                Id_Dictionary.Add(Saved_Turn, Turn_Dictionary);
                WriteLetter_Dictionary.Add(Saved_Id, Id_Dictionary);
                Id_Dictionary = new Dictionary<int, Dictionary<int, WriteLetter_Attributes>>();
                Turn_Dictionary = new Dictionary<int, WriteLetter_Attributes>();
                Saved_Id = WriteLetter.writeletter[i].Id;
                Saved_Turn = WriteLetter.writeletter[i].Turn;
            }

            if (Saved_Turn != WriteLetter.writeletter[i].Turn)
            {
                Id_Dictionary.Add(Saved_Turn, Turn_Dictionary);
                Turn_Dictionary = new Dictionary<int, WriteLetter_Attributes>();
                Saved_Turn = WriteLetter.writeletter[i].Turn;
            }

            Turn_Dictionary.Add(WriteLetter.writeletter[i].Order, WriteLetter.writeletter[i]);
        }
        Id_Dictionary.Add(Saved_Turn, Turn_Dictionary);
        WriteLetter_Dictionary.Add(Saved_Id, Id_Dictionary);
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