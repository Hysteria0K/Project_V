using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Title_Test : MonoBehaviour
{
    public RectTransform Mask1;
    public RectTransform Mask2;
    public RectTransform Mask3;

    private float Mask_Limit = 125.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Mask_Update());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Mask_Update()
    {
        float temp = 0.0f;
        Vector2 Size = Mask1.sizeDelta;

        while(true)
        {
            temp += 1.5f;
            Size.y = temp;
            Mask1.sizeDelta = Size;
            Mask2.sizeDelta = Size;
            Mask3.sizeDelta = Size;

            if (temp >= Mask_Limit) break;

            yield return new WaitForSeconds(0.01f);
        }
    }
}
