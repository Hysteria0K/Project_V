using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageButton_Rulebook : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public bool leftright;

    private Vector3 Saved_Position;

    public Rulebook Rulebook;

    private bool Button_Ready = true;
    private float Button_Time = 0;

    [Header("Control")]
    private float Button_Delay = 0.2f;

    void Update()
    {
        if (Button_Ready != true)
        {
            Button_Time += Time.deltaTime;

            if (Button_Time >= Button_Delay)
            {
                Button_Time = 0;
                Button_Ready = true;
            }
        }
    }


    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //GuideBook.transform.position = Saved_Position;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Rulebook.Icon.GetComponent<Transform>().transform.SetAsLastSibling();

        if (Button_Ready)
        {
            if (leftright)
            {
                if (Rulebook.PageCount < Rulebook.MaxPage)
                {
                    Rulebook.Next_Page();
                    Rulebook.Book_Animation_Play(false);
                    Button_Ready = false;
                }
            }
            else
            {
                if (Rulebook.PageCount != 1)
                {
                    Rulebook.Prev_Page();
                    Rulebook.Book_Animation_Play(true);
                    Button_Ready = false;
                }
            }
        }

        Saved_Position = Rulebook.Icon.transform.position;
    }
}
