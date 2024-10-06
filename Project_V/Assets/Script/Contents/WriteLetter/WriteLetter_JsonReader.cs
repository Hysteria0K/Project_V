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
    #endregion Attributes

    #region Parse
    public class WriteLetter_Parse
    {
        public WriteLetter_Attributes[] writeletter;
    }
   
    #endregion Parse

    public WriteLetter_Parse WriteLetter;
    public Dictionary<string, Dictionary<int, Dictionary<int, WriteLetter_Attributes>>> WriteLetter_Dictionary;

    private void Awake()
    {
        WriteLetter = JsonUtility.FromJson<WriteLetter_Parse>(ReadJson("writeletter"));

        WriteLetter_Dictionary = new Dictionary<string, Dictionary<int, Dictionary<int, WriteLetter_Attributes>>>();
        WriteLetter_To_Dictionary();

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
        string JsonText = File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json");

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
    #endregion 배열, 자료형으로 변환
}