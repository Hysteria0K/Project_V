using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventSystem : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 60;

        string filePath = Path.Combine(Application.persistentDataPath, "resource");

        if (!Directory.Exists(filePath))
        {
            BetterStreamingAssets.Initialize();
            Debug.Log(filePath);
            CopyFilesToPersistentDataPath();
        }

        else
        {
            Debug.Log("복사불필요");
        }

        Cursor.lockState = CursorLockMode.Confined;

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

        string JsonFolderPath = targetFolderPath + "/jsonfiles";

        string ImageFolderPath = targetFolderPath + "/imagefiles";

        string[] jsonFiles = BetterStreamingAssets.GetFiles("/", "*.json", SearchOption.AllDirectories);

        string[] imageFiles = BetterStreamingAssets.GetFiles("/", "*.png", SearchOption.AllDirectories);

        Directory.CreateDirectory(JsonFolderPath);

        Directory.CreateDirectory(ImageFolderPath);

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

        foreach (string imageFilePath in imageFiles)
        {
            string fileName = Path.GetFileName(imageFilePath);
            string targetFilePath = Path.Combine(ImageFolderPath, fileName);

            if (!File.Exists(targetFilePath))
            {
                byte[] fileBytes = BetterStreamingAssets.ReadAllBytes(imageFilePath);
                File.WriteAllBytes(targetFilePath, fileBytes);
            }
        }

        Debug.Log("복사 완료");
    }
}
