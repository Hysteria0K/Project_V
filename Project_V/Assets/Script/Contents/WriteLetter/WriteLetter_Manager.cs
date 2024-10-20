using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class WriteLetter_Manager : MonoBehaviour
{
    [Header("Control")]
    public string Id;
    [Space(15f)]
    public int Turn;

    [Space(15f)]
    [Header("Prefab")]
    public GameObject WriteLetter_Text;
    public GameObject Result_Data_Prefab;

    [Space(15f)]
    [Header("GameObject")]
    public WriteLetter_JsonReader WriteLetter_JsonReader;
    public Transform TextPositionSet;
    public Transform Select_Text;

    public GameObject Selected_Text;
    public Transform Selected_Text_List;

    [Space(15f)]
    public GameObject Result;
    public Transform Result_Text_List;
    public GameObject Result_Masterpiece;
    public GameObject Result_Tag;

    void Awake()
    {
        if (Day_Saver.instance != null)
        {
            Id = Day_Saver.instance.WriteLetter_ID;
        }
        // Id 불러오기 - 나중에 연결할 때 만들어야함 프로토타입에서는 인스펙터에서 제어함.
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
        for(int i =1; i<= WriteLetter_JsonReader.WriteLetter_Dictionary[Id][Turn].Count; i++ )
        {
            WriteLetter_Text.GetComponent<WriteLetter_Text>().MainText = WriteLetter_JsonReader.WriteLetter_Dictionary[Id][Turn][i].Text;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().Tag1 = WriteLetter_JsonReader.WriteLetter_Dictionary[Id][Turn][i].Tag1;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().Tag2 = WriteLetter_JsonReader.WriteLetter_Dictionary[Id][Turn][i].Tag2;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().Tag3 = WriteLetter_JsonReader.WriteLetter_Dictionary[Id][Turn][i].Tag3;
            WriteLetter_Text.GetComponent<WriteLetter_Text>().MasterPiece = WriteLetter_JsonReader.WriteLetter_Dictionary[Id][Turn][i].Masterpiece;

            Instantiate(WriteLetter_Text, TextPositionSet.GetChild(i - 1).transform.position, new Quaternion(0, 0, 0, 0), Select_Text);
        }
    }

    public void Delete_WriteText()
    {
        for (int i = 0; i < Select_Text.childCount; i++)
        {
            Destroy(Select_Text.GetChild(i).gameObject);
        }

        if (Turn <= 3) Spawn_WriteText();

        else
        {
            Set_Result();
        }
    }

    public void Set_Result()
    {
        Instantiate(Result_Data_Prefab);

        for (int i = 0; i < Selected_Text_List.childCount;i++)
        {
            Result_Data.instance.Text[i] = Selected_Text_List.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text;
            Result_Text_List.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text = Result_Data.instance.Text[i];
        }

        if (Selected_Text.GetComponent<Selected_Text>().Masterpiece == true)
        {
            Result_Data.instance.Masterpiece = true;
            Result_Data.instance.Tag_Dictionary = new Dictionary<string, int>(Selected_Text.GetComponent<Selected_Text>().Tag_Dictionary);
            Result_Masterpiece.SetActive(true);
        }

        else
        {
            int temp = 0;
            Result_Data.instance.Masterpiece = false;
            Result_Data.instance.Tag_Dictionary = new Dictionary<string, int>(Selected_Text.GetComponent<Selected_Text>().Tag_Dictionary);

            Result_Tag.GetComponent<TextMeshProUGUI>().text = string.Empty;

            foreach (string key in Result_Data.instance.Tag_Dictionary.Keys)
            {
                if (Result_Data.instance.Tag_Dictionary[key] != 1)
                {
                    Result_Tag.GetComponent<TextMeshProUGUI>().text += "#" + key + " x" + Result_Data.instance.Tag_Dictionary[key];
                }

                else Result_Tag.GetComponent<TextMeshProUGUI>().text += "#" + key;

                Result_Tag.GetComponent<TextMeshProUGUI>().text += "  ";
                temp++;

                if (temp == 3)
                {
                    Result_Tag.GetComponent<TextMeshProUGUI>().text += "\n";
                    temp = 0;
                }
            }
        }

        Selected_Text.SetActive(false);
        Result.gameObject.SetActive(true);
    }

    public void Next_Scene()
    {
        if (Day_Saver.instance != null)
        {   Day_Saver.instance.Day++;
            Day_Saver.instance.Next_Dialogue_ID = WriteLetter_JsonReader.GameLevel_Dictionary[Day_Saver.instance.Day].Start_Story_Id;
            Day_Saver.instance.Next_Scene_Name = "Play";
        }
        Day_Saver.instance.Current_Scene_Name = "Dialogue";
        SceneManager.LoadScene("Dialogue");
    }
}
