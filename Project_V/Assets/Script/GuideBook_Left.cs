using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideBook_Left : MonoBehaviour, IPointerDownHandler
{
    public GuideBook GuideBook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (GuideBook.Guidebook_Page != 1)
        {
            GuideBook.Guidebook_Page--;
            Debug.Log(GuideBook.Guidebook_Page);
        }
    }
}
