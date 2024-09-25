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

    public GameObject Page_List;
    public GameObject Book_Animation;
    public GameObject Prev_Page_Button;
    public GameObject Next_Page_Button;

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
        PageCount++;
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(true);

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
        PageCount--;
        Page_List.transform.GetChild(PageCount).gameObject.SetActive(true);

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
    public void Book_Animation_Play()
    {
        StartCoroutine(Animation_Coroutine());
    }

    IEnumerator Animation_Coroutine()
    {
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
    }
    #endregion 애니메이션
}
