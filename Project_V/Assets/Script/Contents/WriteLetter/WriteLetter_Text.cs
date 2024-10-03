using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class WriteLetter_Text : MonoBehaviour
{
    [Header("Variable")]
    public string MainText;
    public string Tag1;
    public string Tag2;
    public string Tag3;
    public bool MasterPiece;

    [Space(15f)]

    [Header("GameObject")]
    public TextMeshProUGUI MainText_txt;
    public TextMeshProUGUI MainText_effect;
    public TextMeshProUGUI Tag1_txt;
    public TextMeshProUGUI Tag2_txt;
    public TextMeshProUGUI Tag3_txt;
    public GameObject SizeSet;
    public GameObject After_SizeCheck;

    private WriteLetter_Manager GameManager;
    private Selected_Text Selected_Text;
    private Transform Text_List;

    [Space(15f)]
    [SerializeField] private bool SizeChecker = false;
    private float MainText_Size;
    private float Temp_Size;
    private Vector2 Temp_Position;

    private void Awake()
    {
        SizeSet.GetComponent<TextMeshProUGUI>().text = MainText;

        GameManager = GameObject.Find("GameManager").GetComponent<WriteLetter_Manager>();
        Selected_Text = GameObject.Find("Selected_Text").GetComponent<Selected_Text>();
        Text_List = GameObject.Find("Text_List").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainText_txt.text = MainText;
        MainText_effect.text = MainText;

        if (Tag1 != null) Tag1_txt.text = "#" + Tag1;
        if (Tag2 != null) Tag2_txt.text = "#" + Tag2;
        if (Tag3 != null) Tag3_txt.text = "#" + Tag3;
    }

    // Update is called once per frame
    void Update()
    {
        if (SizeChecker == false)
        {
            SizeCheck();
        }
    }

    #region 크기 체크
    private void SizeCheck()
    {
        if (SizeSet.GetComponent<RectTransform>().rect.width != 0)
        {
            MainText_Size = SizeSet.GetComponent<RectTransform>().rect.width;

            Temp_Size = MainText_txt.GetComponent<RectTransform>().rect.width;
            Temp_Position = MainText_txt.GetComponent<RectTransform>().position;

            MainText_txt.GetComponent<RectTransform>().sizeDelta = new Vector2(MainText_Size, MainText_txt.GetComponent<RectTransform>().rect.height);
            MainText_txt.GetComponent<RectTransform>().position = new Vector2(Temp_Position.x + (MainText_Size - Temp_Size) / 2, Temp_Position.y);

            MainText_effect.GetComponent<RectTransform>().sizeDelta = MainText_txt.GetComponent<RectTransform>().sizeDelta;
            MainText_effect.GetComponent<RectTransform>().position = MainText_txt.GetComponent<RectTransform>().position;

            After_SizeCheck.SetActive(true);
            SizeChecker = true;
        }
    }
    #endregion

    #region 문장, 태그 관리

    public void Add_Text()
    {
        Text_List.GetChild(GameManager.Turn - 1).gameObject.GetComponent<TextMeshProUGUI>().text = MainText;

        Selected_Text.Add_Tag(Tag1);
        Selected_Text.Add_Tag(Tag2);
        Selected_Text.Add_Tag(Tag3);

        if (MasterPiece == false)
        {
            Selected_Text.Masterpiece = false;
        }
    }

    #endregion
}
