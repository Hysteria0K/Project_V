using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static JsonReader;
using System;
using UnityEditor.U2D.Aseprite;
using Unity.VisualScripting;

public class RulebookMaker : MonoBehaviour
{
    public JsonReader JsonReader;
    public Sprite_Reader SpriteReader;
    public RectTransform Left_Drawer_Area;

    // 히스 작업 추가
    public Transform Rulebook_Pos;

    #region 프리팹
    public GameObject Icon_Rulebook;
    public GameObject obj_Rulebook;
    public GameObject obj_PageType_1;
    public GameObject obj_PageType_2;
    public GameObject img_rulebook;
    public GameObject txt_rulebook_btn;
    public GameObject txt_rulebook_desc;
    #endregion 프리팹

    void Start()
    {
       Create("BaseBook", Rulebook_Pos);
    }

    // 함수 뒤에 좌표 지정 추가
    public void Create(string id, Transform Pos)
    {
        #region 룰북_기본 세팅
        Debug.Log("룰북 생성" + id);
        GameObject mainobj = Instantiate(obj_Rulebook, Vector3.zero, Quaternion.identity, Left_Drawer_Area);
        mainobj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

        RulebookPage expage = null;
        int PageCount = 1;
        Transform Page_List = mainobj.transform.Find("Page_List");
        //자유롭게 사용하는 임시 저장 오브젝트 변수
        GameObject exobj = null;
        #endregion 룰북_기본 세팅

        foreach (KeyValuePair<int, Rulebook_Attributes> Key in JsonReader.Rulebook_Dictionary[id])
        {
            Rulebook_Attributes v = JsonReader.Rulebook_Dictionary[id][Key.Key];

            switch (v.Type)
            {
                case "NewPage":
                    //페이지 레이아웃
                    //새 페이지 생성
                    if (PageCount == 1)
                    {
                        mainobj.GetComponent<Rulebook>().MaxPage++;
                        exobj = Instantiate(Page_List.GetChild(0).gameObject, Vector3.zero, Quaternion.identity, Page_List);
                        exobj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                        exobj.transform.name = "Page_" + mainobj.GetComponent<Rulebook>().MaxPage;

                        if (mainobj.GetComponent<Rulebook>().MaxPage == 1)
                            exobj.SetActive(true);
                    }

                    switch (float.Parse(v.Value1))
                    {
                        case 1:
                            expage = Instantiate(obj_PageType_1, Vector3.zero, Quaternion.identity, Page_List.GetChild(mainobj.GetComponent<Rulebook>().MaxPage)).GetComponent<RulebookPage>();
                            break;
                        case 2:
                            expage = Instantiate(obj_PageType_2, Vector3.zero, Quaternion.identity, Page_List.GetChild(mainobj.GetComponent<Rulebook>().MaxPage)).GetComponent<RulebookPage>();
                            break;
                    }

                    //홀수
                    if (PageCount == 1)
                    {
                        expage.transform.name = "LeftPage";
                        expage.GetComponent<RectTransform>().anchoredPosition = new Vector3(-172.5f, 0, 0);
                        expage.GetComponent<RectTransform>().sizeDelta = new Vector2(345, 436);
                        PageCount = 0;
                    }
                    //짝수
                    else
                    {
                        expage.transform.name = "RightPage";
                        expage.GetComponent<RectTransform>().anchoredPosition = new Vector3(172.5f, 0, 0);
                        expage.GetComponent<RectTransform>().sizeDelta = new Vector2(345, 436);
                        PageCount = 1;
                    }
                    break;

                case "Title":
                    expage.txt_title.text = v.Value1;
                    break;

                case "G_Text":
                    exobj = Instantiate(txt_rulebook_desc, Vector3.zero, Quaternion.identity, expage.Vertical_Layout_Group);
                    exobj.GetComponent<TextMeshProUGUI>().text = v.Value1;
                    exobj.GetComponent<TextMeshProUGUI>().fontSize = float.Parse(v.Value2);
                    break;

                case "G_Image":
                    break;

                case "G_Image2":
                    break;

                case "G_btnText":
                    exobj = Instantiate(txt_rulebook_btn, Vector3.zero, Quaternion.identity, expage.Vertical_Layout_Group);
                    exobj.GetComponent<TextMeshProUGUI>().text = v.Value1;
                    exobj.GetComponent<TextMeshProUGUI>().fontSize = float.Parse(v.Value2);
                    break;

                case "G_btnImage":
                    break;

                case "Text":
                    exobj = Instantiate(txt_rulebook_desc, Vector3.zero, Quaternion.identity, expage.transform);
                    exobj.GetComponent<RectTransform>().anchoredPosition = new Vector2(float.Parse(v.Value5), float.Parse(v.Value6));
                    exobj.GetComponent<TextMeshProUGUI>().text = v.Value1;
                    exobj.GetComponent<TextMeshProUGUI>().fontSize = float.Parse(v.Value2);
                    break;

                case "Image":
                    exobj = Instantiate(img_rulebook, Vector3.zero, Quaternion.identity, expage.transform);
                    exobj.GetComponent<RectTransform>().sizeDelta = new Vector2(float.Parse(v.Value2), float.Parse(v.Value3));
                    exobj.GetComponent<RectTransform>().anchoredPosition = new Vector2(float.Parse(v.Value5), float.Parse(v.Value6));
                    SpriteReader.LoadSprite(exobj.GetComponent<Image>(),v.Value1);
                    break;

                case "ButtonText":
                    break;

                case "ButtonImage":
                    break;
            }
        }

        #region 룰북_아이콘 세팅
        GameObject Iconobj = Instantiate(Icon_Rulebook, Pos.position, Quaternion.identity, Left_Drawer_Area);
        mainobj.transform.SetParent(Iconobj.transform,false);
        //추가
        Iconobj.GetComponent<RulebookIcon_Controller>().Rulebook_Large = mainobj;
        mainobj.SetActive(false);
        #endregion 룰북_아이콘 세팅
    }

}
