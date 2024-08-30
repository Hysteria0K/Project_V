using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Image_PreLoad : MonoBehaviour
{
    public JsonReader JsonReader;
    public GameObject NPC_Select;
    public GameObject NPC_Select_Opened;
    public int Loaded_Count;

    private void Awake()
    {
        Loaded_Count = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (JsonReader.Character_Dictionary.Count == Loaded_Count)
        {
            NPC_Select_Opened.transform.SetParent(NPC_Select.transform);
            NPC_Select_Opened.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
