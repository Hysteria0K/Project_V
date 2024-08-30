using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using ExcelDataReader;
using Newtonsoft.Json;

/// <summary>
/// 엑셀 파일을 Json으로 변환합니다.
/// </summary>
public static class ExcelToJsonConverter
{
    public static string ConvertExcelToJson(string filePath)
    {
        // 엑셀 파일을 읽습니다.
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader reader;

        if (Path.GetExtension(filePath).Equals(".xls"))
        {
            // Excel 97-2003 포맷 
            reader = ExcelReaderFactory.CreateBinaryReader(stream);
        }
        else if (Path.GetExtension(filePath).Equals(".xlsx"))
        {
            // Excel 2007 포맷 
            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        }
        else
        {
            Debug.LogError("지원하지 않는 포맷입니다. .xls과 .xlsx 파일만 지원합니다.");
            return null;
        }

        // 엑셀 파일에서 데이터 읽기
        DataSet dataSet = reader.AsDataSet();
        reader.Close();

        // 엑셀 파일을 JSON으로 변환
        List<Dictionary<string, object>> jsonData = new List<Dictionary<string, object>>();
        DataTable table = dataSet.Tables[0];
        for (int i = 1; i < table.Rows.Count; i++)
        {
            DataRow row = table.Rows[i];
            Dictionary<string, object> rowData = new Dictionary<string, object>();
            for (int j = 0; j < table.Columns.Count; j++)
            {
                string key = table.Rows[0][j].ToString();
                object value = row[j];
                rowData[key] = value;
            }
            jsonData.Add(rowData);
        }

        // 리턴
        return JsonConvert.SerializeObject(jsonData, Formatting.Indented);
    }
}