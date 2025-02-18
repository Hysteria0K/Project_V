using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue_Button : MonoBehaviour
{
    public string Type = "daily";

    public GameObject Telephone_Saver;

    public GameObject Telephone_Saved;

    private void Awake()
    {
        Telephone_Saver = GameObject.Find("Telephone_Saver");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_Click()
    {
        Telephone_Saved.GetComponent<Telephone_Saved>().Reason = "chr1_" + Type;
        Instantiate(Telephone_Saved, Telephone_Saver.GetComponent<RectTransform>());
    }
}
