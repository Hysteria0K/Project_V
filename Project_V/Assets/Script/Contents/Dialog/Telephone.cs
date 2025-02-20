using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    private JsonReader JsonReader;

    private GameLevel GameLevel;

    public GameObject Dialogue;

    private Telephone_Saver Telephone_Saver;
    private Vector3 Dialogue_Position;

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
    public float Up_Value = 80;

    [Header("Control")]
    //public float Destroy_Delay = 1.0f;
    [SerializeField] private float Dialogue_Delay;

    private void Awake()
    {
        Telephone_Saver = GameObject.Find("Telephone_Saver").GetComponent<Telephone_Saver>();
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();
        GameLevel = GameObject.Find("GameManager").GetComponent<GameLevel>();

        thisRect = GetComponent<RectTransform>();

        Dialogue_Position = GameObject.Find("DialogueText_Position").transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        Text_End_Check = true;

        Index = 0;

        Destroy_Check = false;

        MaxIndex = JsonReader.Dialogue_Dictionary[Reason].Count - 1;

        Dialogue_Delay = JsonReader.Settings.settings[0].Dialogue_Delay;

        Dialogue.GetComponent<Telephone_Dialogue>().Text_Delay = JsonReader.Settings.settings[0].Text_Delay;
        Dialogue.GetComponent<Telephone_Dialogue>().Wait_Delay = JsonReader.Settings.settings[0].Wait_Delay;
        Dialogue.GetComponent<Telephone_Dialogue>().Min_Text = JsonReader.Settings.settings[0].Min_Text;
        Dialogue.GetComponent<Telephone_Dialogue>().Max_Text = JsonReader.Settings.settings[0].Max_Text;
        Dialogue.GetComponent<Telephone_Dialogue>().Increase_Height = JsonReader.Settings.settings[0].Increase_Height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Index > MaxIndex && Text_End_Check == true)
        {
            Telephone_Saver.IsLocked = false;
            Destroy(this.gameObject);
        }

        if (Text_End_Check == true && Destroy_Check == false)
        {
            Text_End_Check = false;
            StartCoroutine(Delay_Make_Dialogue(Dialogue_Delay));
        }

        /*
        if (Up_Check == true && Destroy_Check == false)
        {
            Move_Dialogue();
            Up_Check = false;
        }*/
    }

    private void Make_Dialogue()
    {

        Parse_text = JsonReader.Dialogue_Dictionary[Reason][Index].Text;

        // 명령어로 문장 변경
        if (Parse_text.Contains("<!Goal>"))
        {
            Parse_text = Parse_text.Replace("<!Goal>", GameLevel.Goal);
        }



        // Talker_Sprite = JsonReader.Telephone.Stamp[Index].Sprite;
        Talker = JsonReader.Dialogue_Dictionary[Reason][Index].Name;

        Dialogue.GetComponent<Telephone_Dialogue>().Parse_text = Parse_text;
        Dialogue.GetComponent<Telephone_Dialogue>().Index = Index;
        Dialogue.GetComponent<Telephone_Dialogue>().Name.text = Talker;
        Dialogue.GetComponent<Telephone_Dialogue>().Dialogue_Position = Dialogue_Position;
        Instantiate(Dialogue, Dialogue_Position - new Vector3(0, 1600, 0), thisRect.rotation, thisRect);
    }

    /*
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
    */

    IEnumerator Delay_Make_Dialogue(float d)
    {
        yield return new WaitForSeconds(d);

        Make_Dialogue();
        Index++;
    }
}
