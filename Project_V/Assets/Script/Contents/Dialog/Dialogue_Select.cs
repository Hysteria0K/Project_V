using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Select : MonoBehaviour
{
    public string Type;

    public Transform Select;

    public Dialogue_Manager_New Dialogue_Manager;
    // Start is called before the first frame update
    private void Awake()
    {
        Select = GameObject.Find("Select").transform;

        Dialogue_Manager = GameObject.Find("Dialogue_Manager_New").GetComponent<Dialogue_Manager_New>();
    }

    public void Select_Button()
    {
        Dialogue_Manager.Dialogue_Id = Type;
        Dialogue_Manager.Index = 0;
        Dialogue_Manager.MaxIndex = Dialogue_Manager.JsonReader.Dialogue_Dictionary[Type].Count - 1;
        Dialogue_Manager.Next_Dialogue(0, Dialogue_Manager.JsonReader.Dialogue_Dictionary[Type]);
        
        for (int i = 0; i < Select.childCount; i++)
        {
            Destroy(Select.GetChild(i).gameObject);
        }
    }

}
