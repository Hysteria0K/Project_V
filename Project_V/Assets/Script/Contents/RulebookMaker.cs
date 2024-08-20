using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RulebookMaker : MonoBehaviour
{
    public JsonReader JsonReader;
    public RectTransform Left_Drawer_Area;

    #region 橇府普
    public GameObject Rulebook;
    public GameObject img_rulebook;
    public GameObject PageType_1;
    public GameObject PageType_2;
    public GameObject txt_rulebook_btn;
    public GameObject txt_rulebook_desc;
    #endregion 橇府普


    void Start()
    {
        //Create("BaseBook");
    }

    public void Create(string id)
    {
        Debug.Log("逢合积己_" + id);
        GameObject exo = Instantiate(Rulebook, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), Left_Drawer_Area);
        exo.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

}
