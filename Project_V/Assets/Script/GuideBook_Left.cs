using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideBook_Left : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector3 Saved_Position;

    public GameObject GuideBook;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GuideBook.transform.position = Saved_Position;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        GuideBook.GetComponent<Transform>().transform.SetAsLastSibling();
        if (GuideBook.GetComponent<GuideBook>().Guidebook_Page != 1)
        {
            GuideBook.GetComponent<GuideBook>().Prev_Page();
        }

        Saved_Position = GuideBook.transform.position;
    }
}
