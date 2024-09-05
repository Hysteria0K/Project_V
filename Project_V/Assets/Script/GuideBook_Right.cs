using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideBook_Right : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector3 Saved_Position;

    public GameObject GuideBook;

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GuideBook.transform.position = Saved_Position;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GuideBook.GetComponent<Transform>().transform.SetAsLastSibling();
        if (GuideBook.GetComponent<GuideBook>().Guidebook_Page != GuideBook.GetComponent<GuideBook>().Guidebook_EndPage)
        {
            GuideBook.GetComponent<GuideBook>().Next_Page();
        }

        Saved_Position = GuideBook.transform.position;
        GuideBook.GetComponent<GuideBook>().Book_Animation_Play();
    }
}
