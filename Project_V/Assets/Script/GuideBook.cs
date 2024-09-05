using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GuideBook : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private RectTransform Big_Border_Transform;
    [SerializeField] private JsonReader JsonReader;

    private GameObject Left_Drawer_Area;

    public TextMeshProUGUI Guidebook_Page_Text;

    private RectTransform Guidebook_Transform;

    private Rect Table_Rect;
    private Rect Letter_Rect;
    private Rect Left_Drawer_Area_Rect;

    public GameObject Guidebook_Small;
    public GameObject Guidebook_Large;
    public Drag_Drop Drag_Drop;
    public GameObject Page_List;
    public GameObject Next_Page_Button;
    public GameObject Prev_Page_Button;
    public GameObject Book_Animation;

    private bool OnTable;
    private bool Change_Check;

    private float Half_Width;
    private float Half_Height;
    private Vector3 Origin_Pos;

    [Header("Page")]
    public int Guidebook_Page;
    public int Guidebook_EndPage; // 나중에 데이터파싱해서 사용 -> 자식 오브젝트 수로 판단

    [Header("Control")]
    [SerializeField] private float Animation_Speed = 0.03f;

    private void Awake()
    {
        Left_Drawer_Area = GameObject.Find("Left_Drawer_Area");

        Big_Border_Transform = GameObject.Find("Big_Border").GetComponent<RectTransform>();

        //JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();

        OnTable = false;
        Change_Check = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Left_Drawer_Area_Rect = Left_Drawer_Area.GetComponent<Rect_Area>().Rect;

        Guidebook_Transform = GetComponent<RectTransform>();

        Guidebook_Page = 1;
        Guidebook_EndPage = Page_List.transform.childCount;

        Half_Width = Guidebook_Transform.rect.width / 2;
        Half_Height = Guidebook_Transform.rect.height / 2;

        Origin_Pos = this.transform.position;
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
                Guidebook_Transform.position = Input.mousePosition;
                Drag_Drop.Mouse_Center = true;
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

            if (this.transform.position != Origin_Pos && Drag_Drop.Is_Drag == false)
            {
                this.transform.position = Origin_Pos;
            }
        }

    }

    private void Guidebook_Collider()
    {
        if (Drag_Drop.Is_Drag == true && (Input.mousePosition.y >= Left_Drawer_Area_Rect.yMin && Input.mousePosition.y <= Left_Drawer_Area_Rect.yMax
            && Input.mousePosition.x >= Left_Drawer_Area_Rect.xMin && Input.mousePosition.x <= Left_Drawer_Area_Rect.xMax))
        {
            if (OnTable == false)
            {
                OnTable = true;
                Change_Check = true;
                this.transform.SetParent(Left_Drawer_Area.transform);
            }
        }
        else
        {
            if (OnTable == true && Drag_Drop.Is_Drag == true)
            {
                OnTable = false;
                Change_Check = true;
                this.transform.SetParent(Big_Border_Transform);
            }
        }

        Move_Limit(Guidebook_Transform, Big_Border_Transform);
    }

    private void Guidebook_On_Table()
    {
        Guidebook_Small.SetActive(true);
        Guidebook_Large.SetActive(false);
    }

    private void Guidebook_Not_On_Table()
    {
        Guidebook_Small.SetActive(false);
        Guidebook_Large.SetActive(true);
    }

    private void Move_Limit(RectTransform Move, RectTransform Limit)
    {
        // rectToLimit의 현재 위치
        Vector3[] rectCorners = new Vector3[4];
        Move.GetWorldCorners(rectCorners);

        // boundingRect의 경계 위치
        Vector3[] boundingCorners = new Vector3[4];
        Limit.GetWorldCorners(boundingCorners);

        Vector3 limitedPosition = Move.position;

        // 왼쪽 경계 제한
        if (rectCorners[0].x < boundingCorners[0].x - Half_Width)
        {
            limitedPosition.x += boundingCorners[0].x - rectCorners[0].x - Half_Width;
        }

        // 오른쪽 경계 제한
        if (rectCorners[2].x > boundingCorners[2].x + Half_Width)
        {
            limitedPosition.x -= rectCorners[2].x - boundingCorners[2].x - Half_Width;
        }

        // 아래쪽 경계 제한
        if (rectCorners[0].y < boundingCorners[0].y - Half_Height)
        {
            limitedPosition.y += boundingCorners[0].y - rectCorners[0].y - Half_Height;
        }

        // 위쪽 경계 제한
        if (rectCorners[1].y > boundingCorners[1].y + Half_Height)
        {
            limitedPosition.y -= rectCorners[1].y - boundingCorners[1].y - Half_Height;
        }

        // 제한된 위치 적용
        Move.position = limitedPosition;
    }

    public void Next_Page()
    {
        if (Guidebook_Page == 1)
        {
            Prev_Page_Button.SetActive(true);
        }

        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(false);
        Guidebook_Page++;
        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(true);

        if (Guidebook_Page == Guidebook_EndPage)
        {
            Next_Page_Button.SetActive(false);
        }
    }

    public void Prev_Page()
    {
        if (Guidebook_Page == Guidebook_EndPage)
        {
            Next_Page_Button.SetActive(true);
        }

        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(false);
        Guidebook_Page--;
        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(true);

        if (Guidebook_Page == 1)
        {
            Prev_Page_Button.SetActive(false);
        }
    }

    public void Button_GotoHome()
    {
        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(false);
        Guidebook_Page = 1;
        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(true);
        Prev_Page_Button.SetActive(false);
        Next_Page_Button.SetActive(true);
    }

    public void Button_GotoPage(int pagenumner)
    {
        Page_List.transform.GetChild(Guidebook_Page - 1).gameObject.SetActive(false);
        Guidebook_Page = pagenumner;
        Page_List.transform.GetChild(pagenumner - 1).gameObject.SetActive(true);

        Prev_Page_Button.SetActive(true);
        if (Guidebook_Page == Guidebook_EndPage)
            Next_Page_Button.SetActive(false);
    }

    public void Book_Animation_Play()
    {
        StartCoroutine(Animation_Coroutine());
    }

    IEnumerator Animation_Coroutine()
    {
        for(int i = 0; i < Book_Animation.transform.childCount; i++)
        {
            Book_Animation.transform.GetChild(i).gameObject.SetActive(true);

            if (i != 0)
            {
                Book_Animation.transform.GetChild(i - 1).gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(Animation_Speed);
        }

        Book_Animation.transform.GetChild(Book_Animation.transform.childCount-1).gameObject.SetActive(false);
    }
}
