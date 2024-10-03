using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WriteLetter_MainText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject MainText_effect;
    private WriteLetter_Manager GameManager;
    // Start is called before the first frame update

    void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<WriteLetter_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        MainText_effect.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        MainText_effect.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        GameManager.Delete_WriteText();
    }
}
