using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp_Bar_Edge : MonoBehaviour, IPointerDownHandler
{
    public RectTransform Check_Stamp;

    public bool isOpen;

    public float X_Position_Saved;

    void Awake()
    {
        X_Position_Saved = Check_Stamp.position.x - 960;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (isOpen == false)
        {
            Check_Stamp.position = Check_Stamp.position - new Vector3(X_Position_Saved, 0, 0);
            isOpen = true;
        }

        else
        {
            Check_Stamp.position = Check_Stamp.position + new Vector3(X_Position_Saved, 0, 0);
            isOpen = false;
        }
    }
}
