using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Telephone_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Image Talker;

    private Telephone Telephone;

    public string Parse_text;

    private float delay = 0.125f;

    private void Awake()
    {
        Telephone = gameObject.transform.parent.GetComponent<Telephone>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(textPrint(delay));
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

            yield return new WaitForSeconds(delay);
        }

        Telephone.Text_End_Check = true;
    }
}
