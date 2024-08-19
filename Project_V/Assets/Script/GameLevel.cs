using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    public JsonReader JsonReader;

    [Header("Control")]
    public int Day;

    [Header("DataTable")]
    public bool Is_Dead;
    public int Dead_Count;
    public int Dead_Percentage;
    public bool Is_Invalid_Stamp;

    private void Awake()
    {
        Is_Dead = JsonReader.GameLevel_Dictionary[Day].Is_Dead;
        Dead_Count = JsonReader.GameLevel_Dictionary[Day].Dead_Count;
        Dead_Percentage = JsonReader.GameLevel_Dictionary[Day].Dead_Percentage;
        Is_Invalid_Stamp = JsonReader.GameLevel_Dictionary[Day].Is_Invalid_Stamp;
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
