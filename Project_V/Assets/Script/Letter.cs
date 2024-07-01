using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private RectTransform Table_Area_Transform;
    [SerializeField] private RectTransform Check_Stamp_Area_Transform;
    [SerializeField] private JsonReader JsonReader;

    private RectTransform Letter_Transform;

    private Rect rect1;
    private Rect rect2;
    private Rect Check_Stamp;

    public GameObject Letter_Small;
    public GameObject Letter_Large;

    private bool OnTable;
    private bool Change_Check;

    private float OriginWidth;
    private float OriginHeight;

    [Header("Letter info")]
    [SerializeField] private int FirstName;
    [SerializeField] private int SecondName;
    [SerializeField] private int Rank;
    [SerializeField] private int Regiment;
    [SerializeField] private int Battalion;
    [SerializeField] private int APO;
    [SerializeField] private int Force;
    [SerializeField] private int Stamp;

    [Header("Text")]
    public TextMeshProUGUI Rank_Name_text;
    public TextMeshProUGUI Regiment_Battalion_text;
    public TextMeshProUGUI APO_text;
    public TextMeshProUGUI Force_text;

    private void Awake()
    {
        Table_Area_Transform = GameObject.Find("Table_Area").GetComponent<RectTransform>();
        Check_Stamp_Area_Transform = GameObject.Find("Check_Stamp_Area").GetComponent<RectTransform>();
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Letter_Set();

        Letter_Transform = GetComponent<RectTransform>();

        OnTable = false;
        Change_Check = false;

        OriginWidth = Letter_Transform.rect.width;
        OriginHeight= Letter_Transform.rect.height;

        rect1 = new Rect(Table_Area_Transform.position.x - Table_Area_Transform.rect.width / 2,
                Table_Area_Transform.position.y - Table_Area_Transform.rect.height / 2,
                Table_Area_Transform.rect.width, Table_Area_Transform.rect.height);

        Check_Stamp = new Rect(Check_Stamp_Area_Transform.position.x - OriginWidth / 2,
                      Check_Stamp_Area_Transform.position.y - OriginWidth / 2,
                      OriginWidth, OriginHeight);
    }

    // Update is called once per frame
    void Update()
    {
        Letter_Collider();

        if (Change_Check)
        {
            if (OnTable == true)
            {
                Letter_On_Table();
            }

            else
            {
                Letter_Not_On_Table();
            }

            Change_Check = false;
        }
    }

    private void Letter_Collider()
    {
        rect2 = new Rect(Letter_Transform.position.x - OriginWidth / 2,
                         Letter_Transform.position.y - OriginHeight / 2,
                         OriginWidth, OriginHeight);

        if (rect2.Overlaps(rect1))
        {
            if (OnTable == false)
            {
                OnTable = true;
                Change_Check = true;
            }
        }
        else
        {
            if (OnTable == true)
            {
                OnTable = false;
                Change_Check = true;
            }
        }

        if (rect2.Overlaps(Check_Stamp))
        {
            Letter_Transform.position = Check_Stamp_Area_Transform.position;
        }
    }

    private void Letter_On_Table()
    {
        Letter_Small.SetActive(false);
        Letter_Large.SetActive(true);

    }

    private void Letter_Not_On_Table()
    {
        Letter_Small.SetActive(true);
        Letter_Large.SetActive(false);
    }
    private void Letter_Set()
    {
        // 올바른 편지 (틀린 부분 없음)
        FirstName = Random.Range(0, JsonReader.NameList.namelist.Length);
        SecondName = Random.Range(0, JsonReader.NameList.namelist.Length);
        Rank = Random.Range(0, JsonReader.Rank.militaryrank.Length);
        Regiment = Random.Range(0, JsonReader.ArmyUnit.armyunit.Length);
        Battalion = Regiment;
        APO = Regiment;
        Force = Regiment;
        //Stamp = 우표 나중에 여러개 넣고 돌리면 될듯

        Letter_Text();
    }

    private void Letter_Text()
    {
        Rank_Name_text.text = JsonReader.Rank.militaryrank[Rank].Rank + ", " + 
                              JsonReader.NameList.namelist[FirstName].FirstName + " " +
                              JsonReader.NameList.namelist[SecondName].SecondName;
        Regiment_Battalion_text.text = JsonReader.ArmyUnit.armyunit[Battalion].Battalion + ", " +
                                       JsonReader.ArmyUnit.armyunit[Regiment].Regiment;
        APO_text.text = "APO " + JsonReader.ArmyUnit.armyunit[APO].APO.TrimEnd('.', '0');
        Force_text.text = JsonReader.ArmyUnit.armyunit[Force].Forces;
    }
}
