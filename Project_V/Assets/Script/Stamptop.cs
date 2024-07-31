using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stamptop : MonoBehaviour, IPointerDownHandler
{
    public Check_Stamp Stamp;


    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Stamp.Stamp_Work();
    }
}
