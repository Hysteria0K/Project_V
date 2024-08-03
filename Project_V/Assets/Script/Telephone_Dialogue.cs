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

    private Telephone Telephone;

    public string Parse_text;

    public int Index;

    [Header("Control")]
    [SerializeField] private float Text_Delay = 0.125f;
    [SerializeField] private float Wait_Delay = 0.3f;

    private void Awake()
    {
        Telephone = gameObject.transform.parent.GetComponent<Telephone>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(textPrint(Text_Delay));
    }

    // Update is called once per frame
    void Update()
    {
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
