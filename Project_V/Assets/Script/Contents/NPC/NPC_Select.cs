using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Select : MonoBehaviour
{
    public JsonReader Jsonreader;
    public GameObject Select_Closed;
    public GameObject Select_Open;
    public bool Is_Open;
    public string Selected_Character;

    // Start is called before the first frame update
    void Start()
    {
        Is_Open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change_UI()
    {
        if (Is_Open == false)
        {
            Select_Closed.SetActive(false);
            Select_Open.SetActive(true);
            Is_Open = true;
        }

        else
        {
            Select_Closed.SetActive(true);
            Select_Open.SetActive(false);
            Is_Open = false;
        }
    }
}
