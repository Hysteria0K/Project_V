using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileChecker : MonoBehaviour
{
    private void Awake()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "resource");

        BetterStreamingAssets.Initialize();

        if (!Directory.Exists(filePath))
        {
            Debug.Log(filePath);
            CopyFilesToPersistentDataPath();
            Create_Userdata();
            SceneManager.LoadScene("Title");
        }

        else
        {
            Debug.Log("리소스 폴더 존재, 파일 체크");

            if (!Directory.Exists(filePath + "/userdata") || !File.Exists(filePath + "/userdata/userdata.json"))
            {
                Create_Userdata();
            }
            CheckJsonFiles();
            SceneManager.LoadScene("Title");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CopyFilesToPersistentDataPath()
    {
        string targetFolderPath = Application.persistentDataPath + "/resource";

        string JsonFolderPath = targetFolderPath + "/data";

        //string ImageFolderPath = targetFolderPath + "/imagefiles";

        string[] jsonFiles = BetterStreamingAssets.GetFiles("/resource/encrypted", "*.json", SearchOption.TopDirectoryOnly);

        //string[] imageFiles = BetterStreamingAssets.GetFiles("/", "*.png", SearchOption.AllDirectories);

        Directory.CreateDirectory(JsonFolderPath);

        //Directory.CreateDirectory(ImageFolderPath);

        foreach (string jsonFilePath in jsonFiles)
        {
            string fileName = Path.GetFileName(jsonFilePath);
            string targetFilePath = Path.Combine(JsonFolderPath, fileName);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(jsonFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
            }
        }

        /*foreach (string imageFilePath in imageFiles)
        {
            string fileName = Path.GetFileName(imageFilePath);
            string targetFilePath = Path.Combine(ImageFolderPath, fileName);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(imageFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
            }
        }*/

        Debug.Log("복사 완료");
    }

    private void CheckJsonFiles()
    {
        string targetFolderPath = Application.persistentDataPath + "/resource";

        string JsonFolderPath = targetFolderPath + "/data";

        string[] jsonFiles = BetterStreamingAssets.GetFiles("/resource/encrypted", "*.json", SearchOption.TopDirectoryOnly);

        foreach (string jsonFilePath in jsonFiles)
        {
            string fileName = Path.GetFileName(jsonFilePath);

            string targetFilePath = Path.Combine(JsonFolderPath, fileName);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(jsonFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
                Debug.Log(fileName + "수정 완료");
            }

            else
            {
                string JsonText = BetterStreamingAssets.ReadAllText(jsonFilePath);
                string Directory_JsonFile = File.ReadAllText(JsonFolderPath +"/"+ fileName);

                if (JsonText != Directory_JsonFile)
                {
                    byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(jsonFilePath);
                    File.WriteAllBytes(targetFilePath, fileBytes);
                    Debug.Log(fileName + "수정 완료");
                }
            }
        }
    }

    private void Create_Userdata()
    {
        string Folderpath = Application.persistentDataPath + "/resource/userdata";

        Directory.CreateDirectory(Folderpath);

        string[] jsonFiles = BetterStreamingAssets.GetFiles("/resource/encrypted/userdata", "*.json", SearchOption.TopDirectoryOnly);

        foreach (string jsonFilePath in jsonFiles)
        {
            string fileName = Path.GetFileName(jsonFilePath);
            string targetFilePath = Path.Combine(Folderpath, fileName);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(jsonFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
            }
        }

        Debug.Log("유저 데이터 파일 생성 완료");
    }
}
