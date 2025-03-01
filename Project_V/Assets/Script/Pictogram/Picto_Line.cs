using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picto_Line : MonoBehaviour
{
    public float Max_Scale;

    public bool Next_Double;
    public bool End;

    public GameObject Next_1;
    public GameObject Next_2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Start_Picto(Max_Scale));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Start_Picto(float Max_Scale)
    {
        float a = 0.0f;

        Vector3 Origin_Scale = this.gameObject.GetComponent<RectTransform>().localScale;

        while (true)
        {
            if (a >= Max_Scale) break;

            a += 0.01f;
            Origin_Scale.x = a;

            this.gameObject.GetComponent<RectTransform>().localScale = Origin_Scale;
            yield return new WaitForSeconds(0.005f);
        }

        if (End != true)
        {
            if (Next_Double)
            {
                Next_1.SetActive(true);
                Next_2.SetActive(true);
            }
            else
            {
                Next_1.SetActive(true);
            }
        }
    }
}
