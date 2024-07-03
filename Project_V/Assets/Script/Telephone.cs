using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    private JsonReader JsonReader;

    public GameObject Right_Dialogue;
    public GameObject Left_Dialogue;

    private Telephone_Saver Telephone_Saver;

    public string Reason;

    public int Index;
    public int MaxIndex;
    [SerializeField] private string Parse_text;
    [SerializeField] private string Talker;

    private RectTransform thisRect;

    public bool Text_End_Check;

    public bool Up_Check;

    private bool Destroy_Check;

    [Header("Settings")]
    public float Up_Value;

    private void Awake()
    {
        Telephone_Saver = GameObject.Find("Telephone_Saver").GetComponent<Telephone_Saver>();
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();
        thisRect = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Text_End_Check = true;

        Index = 0;

        Up_Value = 80;

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
            Destroy_Check = true;

            StartCoroutine(Delay_Destroy(1.0f));
        }

        if (Text_End_Check == true && Destroy_Check == false)
        {
            Make_Dialogue();
            Index++;
            Text_End_Check = false;
        }

        if (Up_Check == true && Destroy_Check == false)
        {
            Move_Dialogue();
            Up_Check = false;
        }
    }

    private void Make_Dialogue()
    {
        if (Reason == "Stamp")
        {
            Parse_text = JsonReader.Telephone.Stamp[Index].Text;
            // Talker_Sprite = JsonReader.Telephone.Stamp[Index].Sprite;
            Talker = JsonReader.Telephone.Stamp[Index].Talk;
        }

        if (Talker == "Right")
        {
            Right_Dialogue.GetComponent<Telephone_Dialogue>().Parse_text = Parse_text;
            Right_Dialogue.GetComponent<Telephone_Dialogue>().Index = Index;
            Instantiate(Right_Dialogue, thisRect.position, thisRect.rotation, thisRect);
        }

        if (Talker == "Left")
        {
            Left_Dialogue.GetComponent<Telephone_Dialogue>().Parse_text = Parse_text;
            Left_Dialogue.GetComponent<Telephone_Dialogue>().Index = Index;
            Instantiate(Left_Dialogue, thisRect.position, thisRect.rotation, thisRect);
        }
    }

    private void Move_Dialogue()
    {
        int Child_Count = this.transform.childCount;

        for (int i = Child_Count - 2; i >= 0; i--)
        {
            GameObject child = this.transform.GetChild(i).gameObject;
            StartCoroutine(Move_Coroutine(child));
        }
    }

    IEnumerator Move_Coroutine(GameObject obj)
    {
        float Value_Count = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.03f);

            if (Value_Count >= Up_Value)
            {
                break;
            }
            obj.transform.position += new Vector3(0, Up_Value / 10, 0);
            Value_Count += Up_Value /10;
        }
    }

    IEnumerator Delay_Destroy(float d)
    {
        yield return new WaitForSeconds(d);
        Destroy(this.gameObject);
        Telephone_Saver.IsLocked = false;
    }
}
