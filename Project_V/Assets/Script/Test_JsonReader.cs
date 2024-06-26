using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class Test_JsonReader : MonoBehaviour
{
    [System.Serializable]
    public class Test_Dialogue_Attributes
    {
        public string Chapter;
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

    public class Test_Parse
    {
        public Test_Dialogue_Attributes[] Test;
    }


    [SerializeField]
    private string Filename;

    // Start is called before the first frame update
    void Start()
    {
        Test_Parse Test_Dialogue = JsonUtility.FromJson<Test_Parse>(ReadJson());

        Debug.Log(Test_Dialogue.Test[0].Text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string ReadJson()
    {
        string JsonText = File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json");
        Debug.Log(JsonText);

        return (JsonText);
    }
}
