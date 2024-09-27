using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RulebookIcon_Controller : MonoBehaviour
{
    [Header("Find_Object")]
    [SerializeField] private RectTransform Big_Border_Transform;

    private GameObject Left_Drawer_Area;
    private RectTransform Guidebook_Transform;
    private Rect Left_Drawer_Area_Rect;

    [Space (10f)]
    public GameObject Rulebook_Small;
    public GameObject Rulebook_Large;
    public Drag_Drop Drag_Drop;

    private bool OnTable;
    private bool Change_Check;

    private float Half_Width;
    private float Half_Height;
    private Vector3 Origin_Pos;

    private void Awake()
    {
        Left_Drawer_Area = GameObject.Find("Left_Drawer_Area");

        Big_Border_Transform = GameObject.Find("Big_Border").GetComponent<RectTransform>();

        OnTable = false;
        Change_Check = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Left_Drawer_Area_Rect = Left_Drawer_Area.GetComponent<Rect_Area>().Rect;

        Guidebook_Transform = GetComponent<RectTransform>();

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
            //PageCount_Text.text = $"{PageCount}" + "Page"; 임시 주석처리

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
        Rulebook_Small.SetActive(true);
        Rulebook_Large.SetActive(false);
    }

    private void Guidebook_Not_On_Table()
    {
        Rulebook_Small.SetActive(false);
        Rulebook_Large.SetActive(true);
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
}