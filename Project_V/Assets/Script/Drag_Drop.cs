using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_Drop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private Image Image;
    private Vector2 BeginPosition;
    // Start is called before the first frame update
    void Start()
    {
        Image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IBeginDragHandler.OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        BeginPosition = transform.position;
    }

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log("ÀÌµ¿ ¿Ï");
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
