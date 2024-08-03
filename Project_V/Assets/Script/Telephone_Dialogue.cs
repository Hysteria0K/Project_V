using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Telephone_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI Text;
    //public Image Talker;

    public TextMeshProUGUI Name;

    public GameObject Dialogue_Invisible;
    public RectTransform Dialogue_Visible;

    private TextMeshProUGUI Invisible_Text;

    private Telephone Telephone;

    public string Parse_text;

    public int Index;

    private bool Size_Check = false;

    [Header("Control")]
    [SerializeField] private float Text_Delay = 0.125f;
    [SerializeField] private float Wait_Delay = 0.3f;
    [SerializeField] private float Min_Text = 300.0f;
    [SerializeField] private float Max_Text = 600.0f;

    private void Awake()
    {
        Telephone = gameObject.transform.parent.GetComponent<Telephone>();

        Invisible_Text = Dialogue_Invisible.GetComponent<TextMeshProUGUI>();
        Invisible_Text.text = Parse_text;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Dialogue_Invisible.GetComponent<RectTransform>().rect.width);
        if (Size_Check == false)
        {
            if (Dialogue_Invisible.GetComponent<RectTransform>().rect.width >= Min_Text)
            {
                this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().rect.width +
                    (Dialogue_Invisible.GetComponent<RectTransform>().rect.width - Min_Text), this.GetComponent<RectTransform>().rect.height);
                this.transform.position += new Vector3((Dialogue_Invisible.GetComponent<RectTransform>().rect.width - Min_Text) / 2, 0);

                Dialogue_Visible.sizeDelta = new Vector2(Dialogue_Visible.rect.width +
                    (Dialogue_Invisible.GetComponent<RectTransform>().rect.width - Min_Text), Dialogue_Visible.rect.height);
            }
            Size_Check = true;

            StartCoroutine(textPrint(Text_Delay));
        }
    }

    IEnumerator textPrint(float d)
    {
        int count = 0;

        while (count != Parse_text.Length)
        {
            if (count < Parse_text.Length)
            {
                Text.text += Parse_text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(d);
        }

        yield return new WaitForSeconds(Wait_Delay);

        //Telephone.Up_Check = true;
        Telephone.Text_End_Check = true;
        Destroy(this.gameObject);
    }

    /*
    IEnumerator Move_Coroutine()
    {
        float Value_Count = 0;
        while (Index != Telephone.MaxIndex)
        {
            yield return new WaitForSeconds(0.03f);

            if (Value_Count >= Telephone.Up_Value)
            {
                break;
            }
            this.transform.position += new Vector3(0, Telephone.Up_Value / 10, 0);
            Value_Count += Telephone.Up_Value / 10;
        }

        Telephone.Text_End_Check = true;
    }
    */
}
