using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rulebook : MonoBehaviour
{
    public GameObject Icon;

    [Header("Page")]
    public int MaxPage = 0;
    public int PageCount = 1;

    [Header("Control")]
    [SerializeField] private float Animation_Speed = 0.03f;
    [SerializeField] private float Fade_Time = 0.5f;
    [SerializeField] private float Fade_Delay = 0.05f;
    [SerializeField] private float Button_Delay = 0.4f;

    [Header("Button")]
    public bool Button_Ready = true;
    private float Button_Time = 0;

    public GameObject Page_List;
    public GameObject Book_Animation;
    public GameObject Prev_Page_Button;
    public GameObject Next_Page_Button;

    Coroutine Fade_Now;

    void Start()
    {
        Icon = transform.parent.gameObject;
    }

    #region 버튼
    public void Next_Page()
    {
        Prev_Page_Button.SetActive(true);
        Next_Page_Button.SetActive(true);

        Page_List.transform.GetChild(PageCount).gameObject.SetActive(false);
        Page_List.transform.GetChild(PageCount).GetComponent<CanvasGroup>().alpha = 0;

        PageCount++;
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(true);
        Page_List.transform.GetChild(PageCount).GetComponent<CanvasGroup>().alpha = 0;

        if (PageCount ==  MaxPage)
        {
            Next_Page_Button.SetActive(false);
        }
    }

    public void Prev_Page()
    {
        Prev_Page_Button.SetActive(true);
        Next_Page_Button.SetActive(true);

        if (PageCount ==  MaxPage)
        {
            Next_Page_Button.SetActive(true);
        }

        Page_List.transform.GetChild(PageCount).gameObject.SetActive(false);
        Page_List.transform.GetChild(PageCount).GetComponent<CanvasGroup>().alpha = 0;

        PageCount--;
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(true);
        Page_List.transform.GetChild(PageCount).GetComponent<CanvasGroup>().alpha = 0;

        if (PageCount == 1)
        {
            Prev_Page_Button.SetActive(false);
        }
    }

    public void Button_GotoHome()
    {
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(false);
        PageCount = 1;
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(true);
        Prev_Page_Button.SetActive(false);
        Next_Page_Button.SetActive(true);
    }

    public void Button_GotoPage(int pagenumner)
    {
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(false);
        PageCount = pagenumner;
        Page_List.transform.GetChild(pagenumner).gameObject.SetActive(true);

        Prev_Page_Button.SetActive(true);
        if (PageCount ==  MaxPage)
            Next_Page_Button.SetActive(false);
    }
    #endregion 버튼

    #region 애니메이션
    public void Book_Animation_Play(bool prev)
    {
        StartCoroutine(Animation_Coroutine(prev));
    }

    IEnumerator Animation_Coroutine(bool prev)
    {
        if (prev)
        {
            Book_Animation.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        else
        {
            Book_Animation.transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        for (int i = 0; i < Book_Animation.transform.childCount; i++)
        {
            Book_Animation.transform.GetChild(i).gameObject.SetActive(true);

            if (i != 0)
            {
                Book_Animation.transform.GetChild(i - 1).gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(Animation_Speed);
        }

        Book_Animation.transform.GetChild(Book_Animation.transform.childCount - 1).gameObject.SetActive(false);

        if (Fade_Now != null)
        {
            StopCoroutine(Fade_Now);
        }

        Fade_Now = StartCoroutine(Fade_Coroutine(Fade_Delay, PageCount));
    }
    #endregion 애니메이션

    #region Fade연출
    IEnumerator Fade_Coroutine(float fade_delay, int Page_Count)
    {
        float i = 0;

        while(true)
        {
            if (i >= 1) break;

            i += Fade_Delay / Fade_Time;
            Page_List.transform.GetChild(Page_Count).GetComponent<CanvasGroup>().alpha = i;

            yield return new WaitForSeconds(fade_delay);
        }

        Fade_Now = null;
    }

    #endregion

    #region 버튼 딜레이

    public void Button_Delay_Start()
    {
        StartCoroutine(Button_Coroutine());
    }

    IEnumerator Button_Coroutine()
    {
        while (true)
        {
            if (Button_Time >= Button_Delay)
            {
                break;
            }

            Button_Time += Time.deltaTime;

            yield return null;
        }

        Button_Time = 0;
        Button_Ready = true;
    }


    #endregion
}
