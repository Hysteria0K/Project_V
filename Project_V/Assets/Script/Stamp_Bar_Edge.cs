using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp_Bar_Edge : MonoBehaviour, IPointerDownHandler
{
    public RectTransform Check_Stamp;

    [SerializeField] private bool isOpen;
    [SerializeField] private float X_Position_Saved;
    // Start is called before the first frame update
    void Start()
    {
        X_Position_Saved = Check_Stamp.position.x - 960;
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
