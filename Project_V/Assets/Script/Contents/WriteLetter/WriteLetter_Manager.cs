using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteLetter_Manager : MonoBehaviour
{
    [Header("Control")]
    public int Day;
    [Space(15f)]
    public int Turn;

    [Space(15f)]
    [Header("Prefab")]
    public GameObject WriteLetter_Text;

    [Space(15f)]
    [Header("GameObject")]
    public WriteLetter_JsonReader WriteLetter_JsonReader;
    public Transform TextPositionSet;
    public Transform Select_Text;

    void Awake()
    {
        // Day 불러오기 - 나중에 연결할 때 만들어야함 프로토타입에서는 인스펙터에서 제어함.
        Turn = 1;
    }

    private void Start()
    {
        Spawn_WriteText();
    }

    private void Update()
    {

    }

    public void Spawn_WriteText()
    {
        for(int i =1; i<= WriteLetter_JsonReader.WriteLetter_Dictionary[Day][Turn].Count; i++ )
        {
            WriteLetter_Text.GetComponent<WriteLetter_Text>().MainText = WriteLetter_JsonReader.WriteLetter_Dictionary[Day][Turn][i].Text;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().Tag1 = WriteLetter_JsonReader.WriteLetter_Dictionary[Day][Turn][i].Tag1;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().Tag2 = WriteLetter_JsonReader.WriteLetter_Dictionary[Day][Turn][i].Tag2;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().Tag3 = WriteLetter_JsonReader.WriteLetter_Dictionary[Day][Turn][i].Tag3;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().MasterPiece = WriteLetter_JsonReader.WriteLetter_Dictionary[Day][Turn][i].Masterpiece;

            Instantiate(WriteLetter_Text, TextPositionSet.GetChild(i - 1).transform.position, new Quaternion(0, 0, 0, 0), Select_Text);
        }
    }

    public void Delete_WriteText()
    {
        for (int i = 0; i < Select_Text.childCount; i++)
        {
            Destroy(Select_Text.GetChild(i).gameObject);
        }

        Turn++;
        if (Turn <= 3) Spawn_WriteText();
    }
}
