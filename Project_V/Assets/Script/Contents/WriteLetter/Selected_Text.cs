using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selected_Text : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 Origin_Pos;

    public Dictionary<string, int> Tag_Dictionary;

    [Header("Variables")]
    public bool Masterpiece;

    [Space(15f)]

    [Header("GameObject")]
    public TextMeshProUGUI Tag_List;

    [Space(15f)]

    [Header("Control")]
    [SerializeField] private float Up_Value = 600;
    // Start is called before the first frame update
    void Start()
    {
        Origin_Pos = this.transform.position;
        Masterpiece = true;
        Tag_Dictionary = new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region 커서 컨트롤
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        this.transform.position = new Vector2(Origin_Pos.x, Origin_Pos.y + Up_Value);
        Tag_List.gameObject.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        this.transform.position = Origin_Pos;
        Tag_List.gameObject.SetActive(false);
    }
    #endregion

    #region 태그 딕셔너리 관리

    public void Add_Tag(string tag)
    {
        if (Tag_Dictionary.ContainsKey(tag))
        {
            Tag_Dictionary[tag]++;
        }

        else
        {
            Tag_Dictionary.Add(tag, 1);
        }

        Refresh_Tag_List();
    }

    public void Refresh_Tag_List()
    {
        Tag_List.text = string.Empty;
        foreach (string key in Tag_Dictionary.Keys)
        {
            if (Tag_Dictionary[key] != 1)
            {
                Tag_List.text += "#" + key + " x" + Tag_Dictionary[key];
            }

            else Tag_List.text += "#" + key;

            Tag_List.text += "\n";
        }
    }

    #endregion
}
