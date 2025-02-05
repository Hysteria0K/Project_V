using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Newtonsoft.Json;
using System.IO;

public class Json_Patcher : MonoBehaviour
{
    private string AES_key = "0123456789ABCDEF0123456789ABCDEF";
    private string AES_iv = "ABCDEF0123456789";

    private void Awake()
    {
#if UNITY_EDITOR
        ExcelToJson("dialogue", "dialogue");
        ExcelToJson("armyunit", "armyunit");
        ExcelToJson("militaryrank", "militaryrank");
        ExcelToJson("namelist", "namelist");
        ExcelToJson("poststamp", "poststamp");
        ExcelToJson("rulebookdata", "rulebookdata");
        ExcelToJson("gamesettings", "settings");
        ExcelToJson("gamelevel", "gamelevel");
        ExcelToJson("character", "character");
        ExcelToJson("writeletter", "writeletter");
        ExcelToJson("dialogue_new", "dialogue_new");
#else
        Destroy(this.gameObject);
#endif
        Debug.Log("복사 완료");
        Destroy(this.gameObject);
    }

    //C:\Users\tksth\Desktop\Project_V\Project_V\Assets\ExcelFiles

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExcelToJson(string file, string Header)
    {
        string filename = file + ".json";
        string path = "Assets/StreamingAssets/resource/test/" + filename;
        File.WriteAllText(path, "{\""+ Header + "\":"+ ExcelToJsonConverter.ConvertExcelToJson("Assets/ExcelFiles/" + file + ".xlsx") + "}");

        Encrypt_Json(filename);

        //Debug.Log(path);
    }
    private void Encrypt_Json(string filename)
    {
        string path = "Assets/StreamingAssets/resource/encrypted/" + filename;
        string JsonFile = File.ReadAllText("Assets/StreamingAssets/resource/test/" + filename);
        string encrypted = AESUtil.Encrypt(JsonFile, AES_key, AES_iv);

        File.WriteAllText(path, encrypted);
    }
}
