using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageButton_Rulebook : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public bool leftright;

    private Vector3 Saved_Position;

    public Rulebook Rulebook;

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //GuideBook.transform.position = Saved_Position;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Rulebook.Icon.GetComponent<Transform>().transform.SetAsLastSibling();

        if (leftright)
        {
            if (Rulebook.PageCount < Rulebook.MaxPage)
            {
                Rulebook.Next_Page();
                //Rulebook.Book_Animation_Play();
            }
        }
        else
        {
            if (Rulebook.PageCount != 1)
            {
                Rulebook.Prev_Page();
            }
        }

        Saved_Position = Rulebook.Icon.transform.position;
    }
}
