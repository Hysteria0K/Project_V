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
        public int Day;
        public int Turn;
        public int Order;
        public string Text;
        public string Tag1;
        public string Tag2;
        public string Tag3;
        public bool Masterpiece;
    }
    #endregion Attributes

    #region Parse
    public class WriteLetter_Parse
    {
        public WriteLetter_Attributes[] writeletter;
    }
   
    #endregion Parse

    public WriteLetter_Parse WriteLetter;
    public Dictionary<int, Dictionary<int, Dictionary<int, WriteLetter_Attributes>>> WriteLetter_Dictionary;

    private void Awake()
    {
        WriteLetter = JsonUtility.FromJson<WriteLetter_Parse>(ReadJson("writeletter"));

        WriteLetter_Dictionary = new Dictionary<int, Dictionary<int, Dictionary<int, WriteLetter_Attributes>>>();
        WriteLetter_To_Dictionary();

        Debug.Log("JsonParse Complete");
        //Debug.Log(WriteLetter_Dictionary[1][3][1].Tag1); //WriteLetter_Dictionary[Day][Turn][Order].<Attributes>
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
    private void WriteLetter_To_Dictionary()
    {
        Dictionary<int, Dictionary<int, WriteLetter_Attributes>> Day_Dictionary = new Dictionary<int, Dictionary<int, WriteLetter_Attributes>>();
        Dictionary<int, WriteLetter_Attributes> Turn_Dictionary = new Dictionary<int, WriteLetter_Attributes>();

        int Saved_Day = WriteLetter.writeletter[0].Day;
        int Saved_Turn = WriteLetter.writeletter[0].Turn;

        for (int i = 0; i < WriteLetter.writeletter.Length; i++)
        {
            if (Saved_Day != WriteLetter.writeletter[i].Day)
            {
                Day_Dictionary.Add(Saved_Turn, Turn_Dictionary);
                WriteLetter_Dictionary.Add(Saved_Day, Day_Dictionary);
                Day_Dictionary = new Dictionary<int, Dictionary<int, WriteLetter_Attributes>>();
                Turn_Dictionary = new Dictionary<int, WriteLetter_Attributes>();
                Saved_Day = WriteLetter.writeletter[i].Day;
                Saved_Turn = WriteLetter.writeletter[i].Turn;
            }

            if (Saved_Turn != WriteLetter.writeletter[i].Turn)
            {
                Day_Dictionary.Add(Saved_Turn, Turn_Dictionary);
                Turn_Dictionary = new Dictionary<int, WriteLetter_Attributes>();
                Saved_Turn = WriteLetter.writeletter[i].Turn;
            }

            Turn_Dictionary.Add(WriteLetter.writeletter[i].Order, WriteLetter.writeletter[i]);
        }
        Day_Dictionary.Add(Saved_Turn, Turn_Dictionary);
        WriteLetter_Dictionary.Add(Saved_Day, Day_Dictionary);
    }
    #endregion 배열, 자료형으로 변환
}