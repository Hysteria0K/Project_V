using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageButton_Rulebook : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public bool leftright;

    private Vector3 Saved_Position;

    public Rulebook Rulebook;

    void Update()
    {
       /* if (Button_Ready != true)
        {
            Button_Time += Time.deltaTime;

            if (Button_Time >= Button_Delay)
            {
                Button_Time = 0;
                Button_Ready = true;
            }
        }*/
    }


    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //GuideBook.transform.position = Saved_Position;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Rulebook.Icon.GetComponent<Transform>().transform.SetAsLastSibling();

        if (Rulebook.Button_Ready)
        {
            if (leftright)
            {
                if (Rulebook.PageCount < Rulebook.MaxPage)
                {
                    Rulebook.Next_Page();
                    Rulebook.Book_Animation_Play(false);
                    Rulebook.Button_Ready = false;
                }
            }
            else
            {
                if (Rulebook.PageCount != 1)
                {
                    Rulebook.Prev_Page();
                    Rulebook.Book_Animation_Play(true);
                    Rulebook.Button_Ready = false;
                }
            }
            Rulebook.Button_Delay_Start();
        }

        Saved_Position = Rulebook.Icon.transform.position;
    }
}
