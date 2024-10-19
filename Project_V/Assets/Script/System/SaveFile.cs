using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveFile : MonoBehaviour
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

    public SaveData_Attributes SaveData;

    public DataSave Data_Manager;
    public TextMeshProUGUI Info;
    public TextMeshProUGUI Date;

    public int Index_Num;

    // Start is called before the first frame update
    void Start()
    {
        SaveData= new SaveData_Attributes();

        Load_Data(Index_Num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Load_Data(int index)
    {
        if (Data_Manager.SaveData.savedata[index].Day == 0)
        {
            //empty
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

                if (Data_Manager.SaveData.savedata[index].Text.Length != 0)
                {
                    SaveData.Text = Data_Manager.SaveData.savedata[index].Text;
                    SaveData.Tag_Dictionary = new Dictionary<string, int>(Data_Manager.SaveData.savedata[index].Tag_Dictionary);
                    SaveData.Masterpiece = Data_Manager.SaveData.savedata[index].Masterpiece;
                }
            }
        }
    }
}
