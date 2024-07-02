using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_Drop : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private Vector2 Vect2;
    private Vector3 Saved_Position;

    private RectTransform ThisRect;

    // Start is called before the first frame update
    void Start()
    {
        ThisRect= GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        transform.position = eventData.position - (Vect2 - new Vector2(Saved_Position.x, Saved_Position.y));
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        Vect2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Saved_Position = ThisRect.position;
    }
}
