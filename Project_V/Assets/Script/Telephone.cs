using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    private JsonReader JsonReader;

    public GameObject Right_Dialogue;
    public GameObject Left_Dialogue;

    public string Reason;

    [SerializeField] private int Index;
    [SerializeField] private int MaxIndex;
    [SerializeField] private string Parse_text;
    [SerializeField] private string Talker;

    private RectTransform thisRect;

    public bool Text_End_Check;

    private bool Destroy_Check;

    private void Awake()
    {
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();
        thisRect = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Text_End_Check = true;

        Index = 0;

        Destroy_Check = false;

        if (Reason == "Stamp")
        {
            MaxIndex = JsonReader.Telephone.Stamp.Length-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Index > MaxIndex && Text_End_Check == true)
        {
            Destroy(this.gameObject);
            Destroy_Check = true;
        }

        if (Text_End_Check && Destroy_Check == false)
        {
            Move_Dialogue();
            Make_Dialogue();
            Index++;
            Text_End_Check = false;
        }
    }

    private void Make_Dialogue()
    {
        if (Reason == "Stamp")
        {
            Parse_text = JsonReader.Telephone.Stamp[Index].Text;
            // Talker = JsonReader.Telephone.Stamp[Index].Sprite;
            Talker = JsonReader.Telephone.Stamp[Index].Talk;
        }

        if (Talker == "Right")
        {
            Right_Dialogue.GetComponent<Telephone_Dialogue>().Parse_text = Parse_text;
            Instantiate(Right_Dialogue, thisRect.position, thisRect.rotation, thisRect);
        }

        if (Talker == "Left")
        {
            Left_Dialogue.GetComponent<Telephone_Dialogue>().Parse_text = Parse_text;
            Instantiate(Left_Dialogue, thisRect.position, thisRect.rotation, thisRect);
        }
    }

    private void Move_Dialogue()
    {
        int Child_Count = this.transform.childCount;

        for (int i = Child_Count - 1; i >= 0; i--)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            child.transform.position += new Vector3(0, 80, 0);
        }
    }
}
