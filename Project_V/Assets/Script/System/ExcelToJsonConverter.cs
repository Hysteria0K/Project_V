using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Data;
using ExcelDataReader;
using Newtonsoft.Json;

/// <summary>
/// ���� ������ Json���� ��ȯ�մϴ�.
/// </summary>
public static class ExcelToJsonConverter
{
    public static string ConvertExcelToJson(string filePath)
    {
        // ���� ������ �н��ϴ�.
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        IExcelDataReader reader;

        if (Path.GetExtension(filePath).Equals(".xls"))
        {
            // Excel 97-2003 ���� 
            reader = ExcelReaderFactory.CreateBinaryReader(stream);
        }
        else if (Path.GetExtension(filePath).Equals(".xlsx"))
        {
            // Excel 2007 ���� 
            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        }
        else
        {
            Debug.LogError("�������� �ʴ� �����Դϴ�. .xls�� .xlsx ���ϸ� �����մϴ�.");
            return null;
        }

        // ���� ���Ͽ��� ������ �б�
        DataSet dataSet = reader.AsDataSet();
        reader.Close();

        // ���� ������ JSON���� ��ȯ
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

        // ����
        return JsonConvert.SerializeObject(jsonData, Formatting.Indented);
    }
}