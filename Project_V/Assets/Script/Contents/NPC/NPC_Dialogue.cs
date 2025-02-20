using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC_Dialogue : MonoBehaviour, IPointerDownHandler
{
    public GameObject Choice_UI;

    public Telephone_Saver Telephone_Saver;

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
        if (Telephone_Saver.IsLocked == false)
        {
            Choice_UI.SetActive(true);
            Choice_UI.GetComponent<Choice_UI>().Fade_In();
        }
    }
}
