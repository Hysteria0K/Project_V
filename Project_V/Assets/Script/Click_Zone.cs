using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click_Zone : MonoBehaviour, IPointerDownHandler
{
    public Dialogue_Manager Dialogue_Manager;

    public Dialogue_Manager_New Dialogue_Manager_New;

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
        Dialogue_Manager_New.Next();
        //Dialogue_Manager.Next();
    }
}
