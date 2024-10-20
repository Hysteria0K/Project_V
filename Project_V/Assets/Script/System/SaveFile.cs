using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;

public class SaveFile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public class SaveData_Attributes
    {
        public int Day;
        public string Next_Dialogue_ID;
        public string Next_Scene_Name;
        public string WriteLetter_ID;
        public string Current_Scene_Name;
        public int Dialogue_Index;

        public string[] Text;
        public Dictionary<string, int> Tag_Dictionary;
        public bool Masterpiece;
    }


    private DataSave Data_Manager;

    public SaveData_Attributes SaveData;
    public Image Panel;
    public TextMeshProUGUI Info;
    public TextMeshProUGUI Date;

    public GameObject Result_Data_Prefab;

    public int Index_Num;

    private bool is_empty = false;

    private void Awake()
    {
        Data_Manager = GameObject.Find("Data_Manager").GetComponent<DataSave>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveData = new SaveData_Attributes();

        Load_Data(Index_Num);
        Data_Text();
    }

    #region 데이터 불러오고 텍스트로 출력
    private void Load_Data(int index)
    {
        if (Data_Manager.SaveData.savedata[index].Day == 0)
        {
            is_empty = true;
        }

        else
        {
            SaveData.Day = Data_Manager.SaveData.savedata[index].Day;
            SaveData.Next_Dialogue_ID = Data_Manager.SaveData.savedata[index].Next_Dialogue_ID;
            SaveData.Next_Scene_Name = Data_Manager.SaveData.savedata[index].Next_Scene_Name;
            SaveData.WriteLetter_ID = Data_Manager.SaveData.savedata[index].WriteLetter_ID;
            SaveData.Current_Scene_Name = Data_Manager.SaveData.savedata[index].Current_Scene_Name;
            
            if (Data_Manager.SaveData.savedata[index].Current_Scene_Name == "Dialogue")
            {
                SaveData.Dialogue_Index = Data_Manager.SaveData.savedata[index].Dialogue_Index;

                if (Data_Manager.SaveData.savedata[index].Text != null)
                {
                    //Debug.Log(Data_Manager.SaveData.savedata[index].Tag_Dictionary["태그2"]);
                    SaveData.Text = Data_Manager.SaveData.savedata[index].Text;
                    SaveData.Tag_Dictionary = new Dictionary<string, int>(Data_Manager.SaveData.savedata[index].Tag_Dictionary);
                    SaveData.Masterpiece = Data_Manager.SaveData.savedata[index].Masterpiece;
                }
            }
        }
    }

    private void Data_Text()
    {
        if (is_empty == true)
        {
            Info.text = string.Empty;
            Date.text = string.Empty;
        }

        else
        {
            string temp = string.Empty;

            switch (SaveData.Current_Scene_Name)
            {
                case "Play":
                    {
                        temp = "편지 분류 중";
                        break;
                    }
                case "WriteLetter":
                    {
                        temp = "편지 쓰는 중";
                        break;
                    }
                case "Dialogue":
                    {
                        switch (SaveData.Next_Scene_Name)
                        {
                            case "Play":
                                {
                                    temp = "분류 작업 전";
                                    break;
                                }
                            case "WriteLetter":
                                {
                                    temp = "분류 작업 후";
                                    break;
                                }
                        }
                        break;
                    }
            }

            Info.text = SaveData.Day + "일 째, " + temp;
            //Date.text 설정필요
        }
    }
    #endregion

    #region 커서 컨트롤
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (is_empty == false)
        {
            Panel.color = new Color(100 / 255f, 100 / 255f, 100 / 255f, 218f / 255f);
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (is_empty == false)
        {
            Panel.color = new Color(0, 0, 0, 218f / 255f);
        }
    }
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Day_Saver.instance.Day = SaveData.Day;
        Day_Saver.instance.Next_Dialogue_ID = SaveData.Next_Dialogue_ID;
        Day_Saver.instance.Next_Scene_Name = SaveData.Next_Scene_Name;
        Day_Saver.instance.WriteLetter_ID = SaveData.WriteLetter_ID;
        Day_Saver.instance.Current_Scene_Name = SaveData.Current_Scene_Name;
        Day_Saver.instance.Saved_Dialogue_Index = SaveData.Dialogue_Index;

        if (Data_Manager.SaveData.savedata[Index_Num].Text != null)
        {
            Instantiate(Result_Data_Prefab);
            Result_Data.instance.Text = SaveData.Text;
            Result_Data.instance.Tag_Dictionary = new Dictionary<string, int>(SaveData.Tag_Dictionary);
            Result_Data.instance.Masterpiece = SaveData.Masterpiece;
        }

        SceneManager.LoadScene(Day_Saver.instance.Current_Scene_Name);
    }
    #endregion
}
