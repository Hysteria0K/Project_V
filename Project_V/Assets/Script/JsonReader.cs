using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonReader : MonoBehaviour
{
    [System.Serializable]
    public class NameList_Attributes
    {
        public int Index;
        public string FirstName;
        public string SecondName;
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

    public NameList_Parse NameList;
    public ArmyUnit_Parse ArmyUnit;
    public Rank_Parse Rank;

    private void Awake()
    {
        NameList = JsonUtility.FromJson<NameList_Parse>(ReadJson("namelist"));
        ArmyUnit = JsonUtility.FromJson<ArmyUnit_Parse>(ReadJson("armyunit"));
        Rank = JsonUtility.FromJson<Rank_Parse>(ReadJson("militaryrank"));
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

    // Update is called once per frame
    void Update()
    {

    }

    private string ReadJson(string Filename)
    {
        string JsonText = File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json");

        return (JsonText);
    }
}