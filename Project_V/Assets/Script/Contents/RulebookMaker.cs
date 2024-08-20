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

    #region ������
    public GameObject Rulebook;
    public GameObject img_rulebook;
    public GameObject PageType_1;
    public GameObject PageType_2;
    public GameObject txt_rulebook_btn;
    public GameObject txt_rulebook_desc;
    #endregion ������


    void Start()
    {
        //Create("BaseBook");
    }

    public void Create(string id)
    {
        Debug.Log("��ϻ���_" + id);
        GameObject exo = Instantiate(Rulebook, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), Left_Drawer_Area);
        exo.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
    }

}
