using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Newtonsoft.Json;
using System.IO;

public class Json_Patcher : MonoBehaviour
{
    // Start is called before the first frame update
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
#else
        Destroy(this.gameObject);
#endif
        Debug.Log("복사완료");
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

        //Debug.Log(path);
    }
}
