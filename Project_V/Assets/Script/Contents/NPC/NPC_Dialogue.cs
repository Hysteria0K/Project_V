using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC_Dialogue : MonoBehaviour, IPointerDownHandler
{
    public GameObject Choice_UI;

    public Telephone_Saver Telephone_Saver;

    public Transform Dialogue_obj;

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

            if (Dialogue_obj.childCount != 0)
            {
                Destroy(Dialogue_obj.GetChild(0).gameObject);
            }

            Choice_UI.GetComponent<Choice_UI>().Fade_In();
        }
    }
}
