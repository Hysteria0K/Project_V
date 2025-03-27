using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Day_Saver : MonoBehaviour
{
    public int Day;
    public string Next_Dialogue_ID = "1_Begin"; //default / 다음 다이얼로그
    public string Next_Scene_Name = "Play"; //default / 다음 다이얼로그 후에 올 씬 / Play or WriteLetter
    public string WriteLetter_ID = "Test"; //default
    public string Current_Scene_Name = "Dialogue"; //default
    public int Saved_Dialogue_Index = 0; //Default;
    public bool Auto_Zoom_Act = false;

    public string chr1;
    public float chr1_x;
    public float chr1_y;
    public string chr2;
    public float chr2_x;
    public float chr2_y;
    public string chr3;
    public float chr3_x;
    public float chr3_y;

    //얘네들을 불러오는 세이브 데이터로 쓰면 될듯 호감도 같은 것도 따로 세이브
    //그렇다면 저장은 다이얼로그 씬에서 하고, 턴도 세이브 해두면 될듯

    public static Day_Saver instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        } //singleton
        
    }
}
