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

    [Space(15f)]
    [SerializeField] private bool SizeChecker = false;
    private float MainText_Size;
    private float Temp_Size;
    private Vector2 Temp_Position;

    private void Awake()
    {
        SizeSet.GetComponent<TextMeshProUGUI>().text = MainText;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainText_txt.text = MainText;
        MainText_effect.text = MainText;
        Tag1_txt.text = "#"+Tag1;
        Tag2_txt.text = "#"+Tag2;
        Tag3_txt.text = "#"+Tag3;
    }

    // Update is called once per frame
    void Update()
    {
        if (SizeChecker == false)
        {
            SizeCheck();
        }
    }

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

}
