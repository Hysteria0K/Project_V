using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class DataSave : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
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
    #endregion

    #region Parse
    public class SaveData_Parse
    {
        public SaveData_Attributes[] savedata;
    }
    #endregion

    public SaveData_Parse SaveData;

    private void Awake()
    {
        Reload_Json();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Save_Data(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save_Data(int index)
    {
        SaveData_Attributes Temp;
        Temp = new SaveData_Attributes();

        Temp.Day = Day_Saver.instance.Day;
        Temp.Next_Dialogue_ID = Day_Saver.instance.Next_Dialogue_ID;
        Temp.Next_Scene_Name = Day_Saver.instance.Next_Scene_Name;
        Temp.WriteLetter_ID = Day_Saver.instance.WriteLetter_ID;
        Temp.Current_Scene_Name = Day_Saver.instance.Current_Scene_Name;

        if (Temp.Current_Scene_Name == "Dialogue")
        {
            Temp.Dialogue_Index = GameObject.Find("Dialogue_Manager").GetComponent<Dialogue_Manager>().Index;
        }

        if (Result_Data.instance != null)
        {
            Temp.Text = Result_Data.instance.Text;
            Temp.Tag_Dictionary = new Dictionary<string, int>(Result_Data.instance.Tag_Dictionary);
            Temp.Masterpiece = Result_Data.instance.Masterpiece;
        }

        SaveData.savedata[index] = Temp;

        OverWriteDataFile();
    }

    private void OverWriteDataFile()
    {

#if UNITY_EDITOR
        File.WriteAllText("Assets/StreamingAssets/resource/savedata/userdata" + ".json", JsonConvert.SerializeObject(SaveData, Formatting.Indented));

#else
        File.WriteAllText(Application.persistentDataPath + "/resource/jsonfiles/" + userdata + ".json"), JsonConvert.SerializeObject(SaveData, Formatting.Indented));

#endif
    }
    private string ReadJson(string Filename)
    {
#if UNITY_EDITOR
        string JsonText = File.ReadAllText("Assets/StreamingAssets/resource/savedata/" + Filename + ".json");

        return (JsonText);
#else
        string JsonText = AESUtil.Decrypt(File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json"), "0123456789ABCDEF0123456789ABCDEF", "ABCDEF0123456789");

        return (JsonText);
#endif
    }

    public void Reload_Json()
    {
        SaveData = JsonConvert.DeserializeObject<SaveData_Parse>(ReadJson("userdata"));
    }
}
