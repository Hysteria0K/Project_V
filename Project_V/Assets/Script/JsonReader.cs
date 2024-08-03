using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Data;
using UnityEngine.UIElements;

public class JsonReader : MonoBehaviour
{
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
    public class Telephone_Attributes
    {
        public string Order;
        public int Index;
        public string Talk;
        public string Text;
        public string Sprite;
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

    public class NameList_Parse
    {
        public NameList_Attributes[] namelist;
        // public Test_Dialogue_Attributes[] Test2; 이런식으로 추가가능
    }

    public class ArmyUnit_Parse
    {
        public ArmyUnit_Attributes[] armyunit;
    }

    public class Rank_Parse
    {
        public Militaryrank_Attributes[] militaryrank;
    }

    public class Telephone_Parse
    {
        public Telephone_Attributes[] Stamp;
        // Ex) public Telephone_Attributes[] WrongSomething;
    }

    public class PostStamp_Parse
    {
        public PostStamp_Attributes[] poststamp;
    }
    
    public class Dialogue_Parse
    {
        public Dialogue_Attributes[] dialogue;
    }

    public NameList_Parse NameList;
    public ArmyUnit_Parse ArmyUnit;
    public Rank_Parse Rank;
    public Telephone_Parse Telephone;
    public PostStamp_Parse PostStamp;
    public Dialogue_Parse Dialogue;

    public Dictionary<string, Dictionary<int, Dialogue_Attributes>> Dialogue_Dictionary;

    private void Awake()
    {
        NameList = JsonUtility.FromJson<NameList_Parse>(ReadJson("namelist"));
        ArmyUnit = JsonUtility.FromJson<ArmyUnit_Parse>(ReadJson("armyunit"));
        Rank = JsonUtility.FromJson<Rank_Parse>(ReadJson("militaryrank"));
        Telephone = JsonUtility.FromJson<Telephone_Parse>(ReadJson("telephone"));
        PostStamp = JsonUtility.FromJson<PostStamp_Parse>(ReadJson("poststamp"));
        Dialogue = JsonUtility.FromJson<Dialogue_Parse>(ReadJson("dialogue"));

        Dialogue_Dictionary = new Dictionary<string, Dictionary<int, Dialogue_Attributes>>();
        Dialogue_To_Dictionary();
        //Debug.Log(Dialogue_Dictionary["Test"][0].Text);
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        Debug.Log(NameList.namelist[1].FirstName + NameList.namelist[1].SecondName);
        Debug.Log(ArmyUnit.armyunit[0].Regiment);
        Debug.Log(Rank.militaryrank[10].Rank);
        */
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
}