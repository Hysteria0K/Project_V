using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_UI : MonoBehaviour
{
    public bool UI_Ready;
    private Scene_Manager SceneManager;

    private void Awake()
    {
        SceneManager = GameObject.Find("SceneManager").GetComponent<Scene_Manager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UI_Ready = false;
    }

    private void Update()
    {
        if (UI_Ready == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.Save_UI_Disable();
            }
        }
    }


}
