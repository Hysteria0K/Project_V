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
        string filename = "dialoguetest.json";
        string path = "C:/Users/tksth/Desktop/Project_V/Project_V/Assets/StreamingAssets/resource/test/" + filename;
        File.WriteAllText(path, ExcelToJsonConverter.ConvertExcelToJson("C:/Users/tksth/Desktop/Project_V/Project_V/Assets/ExcelFiles" + "/dialoguetest.xlsx"));

        Debug.Log("¿Ï·á");
        Debug.Log(path);
    }

    //C:\Users\tksth\Desktop\Project_V\Project_V\Assets\ExcelFiles

    // Update is called once per frame
    void Update()
    {
        
    }
}
