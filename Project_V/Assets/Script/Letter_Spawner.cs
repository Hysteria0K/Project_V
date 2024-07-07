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

    public Letter Letter;

    public class DeadPeople
    {
        public int FirstName;
        public int LastName;
        public int Rank;
        public int ArmyUnit_Index;
    }

    public DeadPeople[] DeadList;

    private bool Dead_Spawn = false;

    public bool Generate_Complete;

    private bool Check_Dead;
    private bool Check_Invalid_Stamp;

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
    public void Spawn_Letter()
    {
        if (GameLevel.Is_Dead)
        {
            int Value;
            Value = Random.Range(0, 100);

            if (Value < GameLevel.Dead_Percentage)
            {
                Dead_Spawn = true;
            }

            if (Dead_Spawn == true)
            {
                int Value_2;
                Value_2 = Random.Range(0, DeadList.Length);

                Letter.FirstName = DeadList[Value_2].FirstName;
                Letter.LastName = DeadList[Value_2].LastName;
                Letter.Rank = DeadList[Value_2].Rank;
                Letter.Regiment = DeadList[Value_2].ArmyUnit_Index;
                Letter.Battalion = DeadList[Value_2].ArmyUnit_Index;
                Letter.APO = DeadList[Value_2].ArmyUnit_Index;
                Letter.Force = DeadList[Value_2].ArmyUnit_Index;
                Letter.Problem = true;
                Check_Dead = true;
            }

            else
            {
                Letter_Set();

                for (int i = 0; i < DeadList.Length; i++)
                {
                    while (true)
                    {
                        if (DeadList[i].FirstName == Letter.FirstName &&
                            DeadList[i].LastName == Letter.LastName &&
                            DeadList[i].Rank == Letter.Rank &&
                            DeadList[i].ArmyUnit_Index == Letter.Regiment)
                        {
                            Letter_Set();
                        }
                        else break;
                    }
                }
                Check_Dead = false;
            }
            Dead_Spawn = false;
        }

        if (GameLevel.Is_Invalid_Stamp)
        {
            Letter.PostStamp = Set_PostStamp();

            if (JsonReader.PostStamp.poststamp[Letter.PostStamp].Valid == false) { Check_Invalid_Stamp = true; }

            else { Check_Invalid_Stamp = false; }
        }

        if (Check_Dead == false && Check_Invalid_Stamp == false)
        {
            Letter.Problem = false;
        }

        else { Letter.Problem = true; }

        Generate_Complete = true;
    }

    private void Letter_Set()
    {
        Letter.FirstName = Random.Range(0, JsonReader.NameList.namelist.Length);
        Letter.LastName = Random.Range(0, JsonReader.NameList.namelist.Length);
        Letter.Rank = Set_Rank();
        Letter.Regiment = Random.Range(0, JsonReader.ArmyUnit.armyunit.Length);
        Letter.Battalion = Letter.Regiment;
        Letter.APO = Letter.Regiment;
        Letter.Force = Letter.Regiment;
    }

    private int Set_PostStamp()
    {
        int Value;
        int Max_Value = 0;
        int Stacked_Value = 0;

        for (int i = 0; i < JsonReader.PostStamp.poststamp.Length; i++)
        {
            Max_Value += JsonReader.PostStamp.poststamp[i].Ratio;
        }

        Value = Random.Range(1, Max_Value + 1);

        for (int i = 0; i < JsonReader.PostStamp.poststamp.Length; i++)
        {
            Stacked_Value += JsonReader.PostStamp.poststamp[i].Ratio;

            if (Value <= Stacked_Value)
            {
                return i;
            }
        }

        return 0;
    }
}
