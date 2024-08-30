using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static JsonReader;

public class RulebookMaker : MonoBehaviour
{
    public JsonReader JsonReader;
    public RectTransform Left_Drawer_Area;

    #region 프리팹
    public GameObject Rulebook;
    public GameObject PageType_1;
    public GameObject PageType_2;
    public GameObject img_rulebook;
    public GameObject txt_rulebook_btn;
    public GameObject txt_rulebook_desc;
    #endregion 프리팹


    void Start()
    {
        Create("BaseBook");
    }

    public void Create(string id)
    {
        Debug.Log("룰북 생성" + id);
        GameObject exo = Instantiate(Rulebook, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), Left_Drawer_Area);
        exo.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        GameObject Page_List = exo.transform.Find("Page_List").gameObject;

        foreach (KeyValuePair<int,Rulebook_Attributes> Key in JsonReader.Rulebook_Dictionary[id])
        {
            Debug.Log(JsonReader.Rulebook_Dictionary[id][Key.Key].Type);
        }
    }

}
