using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuideBook_Left : MonoBehaviour, IPointerDownHandler
{
    public GameObject GuideBook;

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
        GuideBook.GetComponent<Transform>().transform.SetAsLastSibling();
        if (GuideBook.GetComponent<GuideBook>().Guidebook_Page != 1)
        {
            GuideBook.GetComponent<GuideBook>().Guidebook_Page--;
            Debug.Log(GuideBook.GetComponent<GuideBook>().Guidebook_Page);
        }
    }
}
