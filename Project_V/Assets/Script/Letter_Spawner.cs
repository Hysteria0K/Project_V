using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Letter_Spawner : MonoBehaviour
{
    public GameLevel GameLevel;
    public JsonReader JsonReader;
    public TextMeshProUGUI Dead_People_1;
    public TextMeshProUGUI Dead_People_2;
    public TextMeshProUGUI Dead_People_3;

    public class DeadPeople
    {
        public int FirstName;
        public int LastName;
        public int Rank;
        public int ArmyUnit_Index;
    }

    public DeadPeople[] DeadList;

    private void Awake()
    {
        if (GameLevel.Is_Dead)
        {
            DeadList = new DeadPeople[GameLevel.Dead_Count];

            for (int i = 0; i < GameLevel.Dead_Count; i++)
            {
                DeadList[i] = new DeadPeople();
            }
        }
    }

    void Start()
    {
        if (GameLevel.Is_Dead)
        {
            Spawn_DeadPeople();
            Text_DeadPeople();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn_DeadPeople()
    {
        for (int i = 0; i < GameLevel.Dead_Count; i++)
        {
            Create_DeadPeople_Info(i);

            for (int a = 0; a < i; a++)
            {
                while(true)
                {
                    if (DeadList[i].FirstName == DeadList[a].FirstName &&
                        DeadList[i].LastName == DeadList[a].LastName &&
                        DeadList[i].Rank == DeadList[a].Rank &&
                        DeadList[i].ArmyUnit_Index == DeadList[a].ArmyUnit_Index)
                    {
                        Create_DeadPeople_Info(i);
                    }

                    else
                    {
                        break;
                    }
                }
            }
        }
    }

    private int Set_Rank()
    {
        int Value;
        int Max_Value = 0;
        int Stacked_Value = 0;

        for (int i = 0; i < JsonReader.Rank.militaryrank.Length; i++)
        {
            Max_Value += JsonReader.Rank.militaryrank[i].Ratio;
        }

        Value = Random.Range(1, Max_Value + 1);

        for (int i = 0; i < JsonReader.Rank.militaryrank.Length; i++)
        {
            Stacked_Value += JsonReader.Rank.militaryrank[i].Ratio;

            if (Value <= Stacked_Value)
            {
                return i;
            }
        }

        return 0;
    }
    private void Create_DeadPeople_Info(int i)
    {
        DeadList[i].FirstName = Random.Range(0, JsonReader.NameList.namelist.Length);
        DeadList[i].LastName = Random.Range(0, JsonReader.NameList.namelist.Length);
        DeadList[i].Rank = Set_Rank();
        DeadList[i].ArmyUnit_Index = Random.Range(0, JsonReader.ArmyUnit.armyunit.Length);
    }

    private void Text_DeadPeople()
    {
        for(int i = 0; i < GameLevel.Dead_Count; i++)
        {
            if (i < 10)
            {
                Dead_People_1.text += Add_Text(i);
            }

            else if (i >= 10 && i < 20)
            {
                Dead_People_2.text += Add_Text(i);
            }

            else if (i >= 20 && i < 30)
            {
                Dead_People_3.text += Add_Text(i);
            }
        }
    }

    private string Add_Text(int i)
    {
        string text = "";

        text += JsonReader.Rank.militaryrank[DeadList[i].Rank].Rank + " " + JsonReader.NameList.namelist[DeadList[i].FirstName].FirstName + " "
             + JsonReader.NameList.namelist[DeadList[i].LastName].LastName + ", " + JsonReader.ArmyUnit.armyunit[DeadList[i].ArmyUnit_Index].Battalion + ", "
             + JsonReader.ArmyUnit.armyunit[DeadList[i].ArmyUnit_Index].Regiment + ", " + JsonReader.ArmyUnit.armyunit[DeadList[i].ArmyUnit_Index].Forces + "\n";

        return text;
    }
}
