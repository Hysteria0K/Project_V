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
        // public Test_Dialogue_Attributes[] Test2; 이런식으로 추가가능
    }

    [SerializeField]
    private string Filename;

    public Test_Parse Test_Dialogue;

    private void Awake()
    {
        Test_Dialogue = JsonUtility.FromJson<Test_Parse>(ReadJson());
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string ReadJson()
    {
#if UNITY_EDITOR
        string JsonText = File.ReadAllText("Assets/StreamingAssets/resource/test/" + Filename + ".json");

        return (JsonText);
#else
        string JsonText = File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json");

        return (JsonText);
#endif
    }
}
