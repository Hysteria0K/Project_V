using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_Drop : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler, IPointerUpHandler
{
    private Vector2 Vect2;
    private Vector3 Saved_Position;

    private RectTransform ThisRect;

    public bool Mouse_Center;
    public bool Is_Drag;

    // Start is called before the first frame update
    void Start()
    {
        ThisRect= GetComponent<RectTransform>();

        Mouse_Center = false;
        Is_Drag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Is_Drag == false)
        {
            Is_Drag = true;
        }

        if (Mouse_Center == false)
        {
            transform.position = eventData.position - (Vect2 - new Vector2(Saved_Position.x, Saved_Position.y));
        }

        else
        {
            transform.position = eventData.position;
        }
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        OnPointerDown();
    }

    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Mouse_Center = false;
        Is_Drag = false;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Mouse_Center = false;
        Is_Drag = false;
    }

    public void OnPointerDown()
    {
        transform.SetAsLastSibling();
        Vect2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Saved_Position = ThisRect.position;
    }
}
