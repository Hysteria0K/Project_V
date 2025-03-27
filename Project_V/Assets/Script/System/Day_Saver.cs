using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Day_Saver : MonoBehaviour
{
    public int Day;
    public string Next_Dialogue_ID = "1_Begin"; //default / ���� ���̾�α�
    public string Next_Scene_Name = "Play"; //default / ���� ���̾�α� �Ŀ� �� �� / Play or WriteLetter
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

    //��׵��� �ҷ����� ���̺� �����ͷ� ���� �ɵ� ȣ���� ���� �͵� ���� ���̺�
    //�׷��ٸ� ������ ���̾�α� ������ �ϰ�, �ϵ� ���̺� �صθ� �ɵ�

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
