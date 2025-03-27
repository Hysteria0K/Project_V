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
        public bool Auto_Zoom_Act;

        public string chr1;
        public float chr1_x;
        public float chr1_y;
        public string chr2;
        public float chr2_x;
        public float chr2_y;
        public string chr3;
        public float chr3_x;
        public float chr3_y;

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
        //Save_Data(0); 초기화용
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
            Dialogue_Manager_New Dialogue_M = GameObject.Find("Dialogue_Manager_New").GetComponent<Dialogue_Manager_New>();

            Temp.Dialogue_Index = Dialogue_M.Index;
            Temp.Auto_Zoom_Act = Dialogue_M.Auto_Zoom_Act;

            Temp.chr1 = Dialogue_M.chr1;
            Temp.chr2 = Dialogue_M.chr2;
            Temp.chr3 = Dialogue_M.chr3;    
            Temp.chr1_x = Dialogue_M.chr1_x;
            Temp.chr2_x = Dialogue_M.chr2_x;
            Temp.chr3_x = Dialogue_M.chr3_x;
            Temp.chr1_y = Dialogue_M.chr1_y;
            Temp.chr2_y = Dialogue_M.chr2_y;
            Temp.chr3_y = Dialogue_M.chr3_y;

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
        File.WriteAllText(Application.persistentDataPath + "/resource/userdata/" + "userdata.json", AESUtil.Encrypt(JsonConvert.SerializeObject(SaveData, Formatting.Indented), "0123456789ABCDEF0123456789ABCDEF", "ABCDEF0123456789"));

#endif
    }
    private string ReadJson(string Filename)
    {
#if UNITY_EDITOR
        string JsonText = File.ReadAllText("Assets/StreamingAssets/resource/savedata/" + Filename + ".json");

        return (JsonText);
#else
        string JsonText = AESUtil.Decrypt(File.ReadAllText(Application.persistentDataPath + "/resource/userdata/" + Filename + ".json"), "0123456789ABCDEF0123456789ABCDEF", "ABCDEF0123456789");

        return (JsonText);
#endif
    }

    public void Reload_Json()
    {
        SaveData = JsonConvert.DeserializeObject<SaveData_Parse>(ReadJson("userdata"));
    }

    private void Encrypt_Userdata_Json(string filename)
    {
        string path = "Assets/StreamingAssets/resource/encrypted/userdata/" + filename;
        string JsonFile = File.ReadAllText("Assets/StreamingAssets/resource/savedata/" + filename);
        string encrypted = AESUtil.Encrypt(JsonFile, "0123456789ABCDEF0123456789ABCDEF", "ABCDEF0123456789");

        File.WriteAllText(path, encrypted);
    }

    public void Delete_Data(int index)
    {
        SaveData_Attributes Temp;
        Temp = new SaveData_Attributes();

        Temp.Day = 0;
        Temp.Next_Dialogue_ID = "";
        Temp.Next_Scene_Name = "";
        Temp.WriteLetter_ID = "";
        Temp.Current_Scene_Name = "";

        Temp.Dialogue_Index = 0;

        Temp.Text = null;
        Temp.Tag_Dictionary = null;
        Temp.Masterpiece = false;

        SaveData.savedata[index] = Temp;

        OverWriteDataFile();
    }
}
