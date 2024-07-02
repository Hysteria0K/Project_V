using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class Letter : MonoBehaviour, IEndDragHandler
{
    [Header("Object")]
    [SerializeField] private RectTransform Table_Area_Transform;
    [SerializeField] private RectTransform Letter_Area_Transform;
    [SerializeField] private RectTransform PostBox_Area_Transform;
    [SerializeField] private JsonReader JsonReader;

    private RectTransform Letter_Transform;

    private Rect Table;
    private Rect Letter_Rect;
    private Rect PostBox;

    public GameObject Letter_Small;
    public GameObject Letter_Large;

    private bool OnTable;
    private bool Change_Check;

    [Header("Letter info")]
    [SerializeField] private int FirstName;
    [SerializeField] private int LastName;
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

    [Header("Stamp")]
    public int Stamp_Value; // 0 = 안함, 1 = stamp_1, 2 = stamp_Approved, 3 = stamp_Denied, 4 = 여러개 동시에 찍음

    private void Awake()
    {
        Table_Area_Transform = GameObject.Find("Table_Area").GetComponent<RectTransform>();
        Letter_Area_Transform = GameObject.Find("Letter_Area").GetComponent<RectTransform>();
        PostBox_Area_Transform = GameObject.Find("PostBox_Area").GetComponent<RectTransform>();
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Letter_Set();

        Letter_Transform = GetComponent<RectTransform>();

        OnTable = false;
        Change_Check = false;

        Table = new Rect(Table_Area_Transform.position.x - Table_Area_Transform.rect.width / 2,
                Table_Area_Transform.position.y - Table_Area_Transform.rect.height / 2,
                Table_Area_Transform.rect.width, Table_Area_Transform.rect.height);

        PostBox = new Rect(PostBox_Area_Transform.position.x - PostBox_Area_Transform.rect.width / 2,
                  PostBox_Area_Transform.position.y - PostBox_Area_Transform.rect.height / 2,
                  PostBox_Area_Transform.rect.width, PostBox_Area_Transform.rect.height);

        Stamp_Value = 0;
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
        Letter_Rect = new Rect(Letter_Transform.position.x,
                         Letter_Transform.position.y,
                         1, 1);

        Debug.Log(Letter_Rect);

        if (Letter_Rect.Overlaps(Table))
        {
            if (OnTable == false)
            {
                OnTable = true;
                Change_Check = true;
                this.transform.SetParent(Table_Area_Transform);
            }
        }
        else
        {
            if (OnTable == true)
            {
                OnTable = false;
                Change_Check = true;
                this.transform.SetParent(Letter_Area_Transform);
            }
        }

        if (Letter_Rect.Overlaps(PostBox))
        {
            this.transform.SetParent(PostBox_Area_Transform);
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
        LastName = Random.Range(0, JsonReader.NameList.namelist.Length);
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
                              JsonReader.NameList.namelist[LastName].LastName;
        Regiment_Battalion_text.text = JsonReader.ArmyUnit.armyunit[Battalion].Battalion + ", " +
                                       JsonReader.ArmyUnit.armyunit[Regiment].Regiment;
        APO_text.text = "APO " + JsonReader.ArmyUnit.armyunit[APO].APO.TrimEnd('.', '0');
        Force_text.text = JsonReader.ArmyUnit.armyunit[Force].Forces;
    }

    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Letter_Rect.Overlaps(PostBox))
        {
            Debug.Log("집하장행");

            Destroy(this.gameObject);
        }
    }
}
