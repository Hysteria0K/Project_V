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

    public NameList_Parse NameList;
    public ArmyUnit_Parse ArmyUnit;
    public Rank_Parse Rank;
    public Telephone_Parse Telephone;

    private void Awake()
    {
        NameList = JsonUtility.FromJson<NameList_Parse>(ReadJson("namelist"));
        ArmyUnit = JsonUtility.FromJson<ArmyUnit_Parse>(ReadJson("armyunit"));
        Rank = JsonUtility.FromJson<Rank_Parse>(ReadJson("militaryrank"));
        Telephone = JsonUtility.FromJson<Telephone_Parse>(ReadJson("telephone"));
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