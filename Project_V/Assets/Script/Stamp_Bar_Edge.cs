using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stamp_Bar_Edge : MonoBehaviour, IPointerDownHandler
{
    public RectTransform Check_Stamp;
    public RectTransform Check_Bar;
    public RectTransform Check_BarEdge;

    public bool isOpen;

    public float X_Position_Saved;

    #region 애니변수
    private Vector2 vec1;
    private Vector2 vec2;
    //반동거리
    #endregion 애니변수

    void Awake()
    {
        X_Position_Saved = Check_Stamp.position.x - 960;

        vec1 = new Vector2(Check_Stamp.anchoredPosition.x, Check_Stamp.anchoredPosition.y);
        vec2 = new Vector2(Check_Bar.sizeDelta.x * -1, Check_Stamp.anchoredPosition.y);
    }

    void Start()
    {
        isOpen = false;
    }

    #region 이벤트 함수
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (isOpen == false)
        {
            //Check_Stamp.position = Check_Stamp.position - new Vector3(X_Position_Saved, 0, 0);
            isOpen = true;
            StartCoroutine("Ani_StampBarMove");
        }

        else
        {
            //Check_Stamp.position = Check_Stamp.position + new Vector3(X_Position_Saved, 0, 0);
            isOpen = false;
            StartCoroutine("Ani_StampBarMove");
        }
    }
    #endregion 이벤트 함수

    #region 애니메이션 함수
    IEnumerator Ani_StampBarMove()
    {
        float speed = 0.07f;
        float value = 0;
        int step = 0;

        while (true)
        {
            switch (step)
            {
                case 0:
                    value = value + speed;
                    if (isOpen)
                        Check_Stamp.anchoredPosition = Vector2.Lerp(vec1, vec2, Mathf.Min(value, 1));
                    else
                        Check_Stamp.anchoredPosition = Vector2.Lerp(vec2, vec1, Mathf.Min(value, 1));
                    break;
                case 1:
                    value = value + speed;
                    if (isOpen)
                        Check_Stamp.anchoredPosition = Vector2.Lerp(vec2, new Vector2(vec2.x * 0.9f, vec2.y), Mathf.Min(value, 1));
                    else
                        Check_Stamp.anchoredPosition = Vector2.Lerp(vec1, new Vector2(vec2.x * 0.1f, vec2.y), Mathf.Min(value, 1));
                    break;
                case 2:
                    value = value + speed * 0.8f;
                    if (isOpen)
                        Check_Stamp.anchoredPosition = Vector2.Lerp(new Vector2(vec2.x * 0.9f, vec2.y), vec2, Mathf.Min(value, 1));
                    else
                        Check_Stamp.anchoredPosition = Vector2.Lerp(new Vector2(vec2.x * 0.1f, vec2.y), vec1, Mathf.Min(value, 1));
                    break;
            }

            if (value >= 1)
            {
                if (step == 2)
                {
                    yield break;
                }
                else
                {
                    value = 0;
                    step++;
                }
            }
            else
                yield return new WaitForFixedUpdate();
        }
    }
    #endregion 애니메이션 함수
}
