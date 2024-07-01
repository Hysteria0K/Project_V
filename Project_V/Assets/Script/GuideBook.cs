using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuideBook : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private RectTransform Table_Area_Transform;
    [SerializeField] private JsonReader JsonReader;
    public TextMeshProUGUI Guidebook_Page_Text;

    private RectTransform Guidebook_Transform;

    private Rect Table;
    private Rect Guidebook_Rect;

    public GameObject Guidebook_Small;
    public GameObject Guidebook_Large;

    private bool OnTable;
    private bool Change_Check;

    private float OriginWidth;
    private float OriginHeight;

    [Header("Page")]
    public int Guidebook_Page;
    public int Guidebook_EndPage = 10; // 나중에 데이터파싱해서 사용

    private void Awake()
    {
        Table_Area_Transform = GameObject.Find("Table_Area").GetComponent<RectTransform>();
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();

        OnTable = false;
        Change_Check = false;

        Table = new Rect(Table_Area_Transform.position.x - Table_Area_Transform.rect.width / 2,
                Table_Area_Transform.position.y - Table_Area_Transform.rect.height / 2,
                Table_Area_Transform.rect.width, Table_Area_Transform.rect.height);
    }

    // Start is called before the first frame update
    void Start()
    {
        Guidebook_Transform = GetComponent<RectTransform>();

        OriginWidth = Guidebook_Transform.rect.width;
        OriginHeight = Guidebook_Transform.rect.height;

        Guidebook_Page = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Guidebook_Collider();

        if (Change_Check)
        {
            if (OnTable == true)
            {
                Guidebook_On_Table();
            }

            else
            {
                Guidebook_Not_On_Table();
            }

            Change_Check = false;
        }

        if (OnTable == true)
        {
            Guidebook_Page_Text.text = $"{Guidebook_Page}" + "Page";
        }
    }

    private void Guidebook_Collider()
    {
        Guidebook_Rect = new Rect(Guidebook_Transform.position.x - OriginWidth / 2,
                         Guidebook_Transform.position.y - OriginHeight / 2,
                         OriginWidth, OriginHeight);

        if (Guidebook_Rect.Overlaps(Table))
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
    }

    private void Guidebook_On_Table()
    {
        Guidebook_Small.SetActive(false);
        Guidebook_Large.SetActive(true);
    }

    private void Guidebook_Not_On_Table()
    {
        Guidebook_Small.SetActive(true);
        Guidebook_Large.SetActive(false);
    }
}
