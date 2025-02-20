using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public JsonReader JsonReader;

    [Header("Control")]
    public int Day; // 데이 값을 저장하거나 불러오는 식으로 하면 댈듯?
    public string Start_Story_Id;
    public string End_Story_Id;
    public string Letter_Id;

    [Header("DataTable")]
    public bool Is_Dead;
    public int Dead_Count;
    public int Dead_Percentage;
    public bool Is_Invalid_Stamp;
    public string Goal;

    private void Awake()
    {
        if (Day_Saver.instance != null)
        {
            Day = Day_Saver.instance.Day;
        }
        Is_Dead = JsonReader.GameLevel_Dictionary[Day].Is_Dead;
        Dead_Count = JsonReader.GameLevel_Dictionary[Day].Dead_Count;
        Dead_Percentage = JsonReader.GameLevel_Dictionary[Day].Dead_Percentage;
        Is_Invalid_Stamp = JsonReader.GameLevel_Dictionary[Day].Is_Invalid_Stamp;
        Start_Story_Id = JsonReader.GameLevel_Dictionary[Day].Start_Story_Id;
        End_Story_Id = JsonReader.GameLevel_Dictionary[Day].End_Story_Id;
        Letter_Id = JsonReader.GameLevel_Dictionary[Day].Letter_Id;
        Goal = JsonReader.GameLevel_Dictionary[Day].Goal;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
