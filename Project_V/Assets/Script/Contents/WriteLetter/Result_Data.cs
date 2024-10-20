using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result_Data : MonoBehaviour
{
    public string[] Text;

    public Dictionary<string, int> Tag_Dictionary;
    public bool Masterpiece;

    public static Result_Data instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } //singleton
    }

    private void Update()
    {
        Debug.Log(instance.Tag_Dictionary["еб╠в1"]);
    }
}
