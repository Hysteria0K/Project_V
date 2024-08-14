using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Number_Button : MonoBehaviour, IPointerDownHandler
{
    public int Number;

    public TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        Number = 0;
        Text.text = Number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_Click()
    {
        if (Number == 9)
        {
            Number = 0;
        }

        else
        {
            Number++;
        }

        Text.text = Number.ToString();
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        transform.parent.GetComponent<Drag_Drop>().OnPointerDown();
    }
}
