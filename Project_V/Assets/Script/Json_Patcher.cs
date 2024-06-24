using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using Newtonsoft.Json;
using System.IO;

public class Json_Patcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExcelToJson("dialoguetest");
    }

    //C:\Users\tksth\Desktop\Project_V\Project_V\Assets\ExcelFiles

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ExcelToJson(string file)
    {
        string filename = file + ".json";
        string path = "C:/Users/tksth/Desktop/Project_V/Project_V/Assets/StreamingAssets/resource/test/" + filename;
        File.WriteAllText(path, ExcelToJsonConverter.ConvertExcelToJson("C:/Users/tksth/Desktop/Project_V/Project_V/Assets/ExcelFiles/" + file +".xlsx"));

        Debug.Log(path);
    }
}
