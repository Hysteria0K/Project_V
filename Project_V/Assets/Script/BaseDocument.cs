using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseDocument : MonoBehaviour
{
    [SerializeField] private RectTransform Table_Area_Transform;
    [SerializeField] private RectTransform Letter_Area_Transform;
    [SerializeField] private RectTransform Big_Border_Transform;

    private RectTransform BaseDocument_Transform;

    private Rect Table;
    private Rect Letter_Area;

    public GameObject BaseDocument_Small;
    public GameObject BaseDocument_Large;
    public Drag_Drop Drag_Drop;

    private bool OnTable;
    private bool Change_Check;

    private float Half_Width;
    private float Half_Height;

    private void Awake()
    {
        Table_Area_Transform = GameObject.Find("Table_Area").GetComponent<RectTransform>();
        Letter_Area_Transform = GameObject.Find("Letter_Area").GetComponent<RectTransform>();
        Big_Border_Transform = GameObject.Find("Big_Border").GetComponent<RectTransform>();

        OnTable = false;
        Change_Check = false;

        Table = new Rect(Table_Area_Transform.position.x - Table_Area_Transform.rect.width / 2,
                Table_Area_Transform.position.y - Table_Area_Transform.rect.height / 2,
                Table_Area_Transform.rect.width, Table_Area_Transform.rect.height);

        Letter_Area = new Rect(Letter_Area_Transform.position.x - Letter_Area_Transform.rect.width / 2,
                       Letter_Area_Transform.position.y - Letter_Area_Transform.rect.height / 2,
                       Letter_Area_Transform.rect.width, Letter_Area_Transform.rect.height);

    }

    // Start is called before the first frame update
    void Start()
    {
        BaseDocument_Transform = GetComponent<RectTransform>();


        Half_Width = BaseDocument_Transform.rect.width / 2;
        Half_Height = BaseDocument_Transform.rect.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        BaseDocument_Collider();

        if (Change_Check)
        {
            if (OnTable == true)
            {
                BaseDocument_On_Table();
            }

            else
            {
                BaseDocument_Not_On_Table();
                BaseDocument_Transform.position = Input.mousePosition;
                Drag_Drop.Mouse_Center = true;
            }

            Change_Check = false;
        }

    }

    private void BaseDocument_Collider()
    {
        if (Drag_Drop.Is_Drag == true && Input.mousePosition.x >= Letter_Area.xMax)
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
            if (OnTable == true && Drag_Drop.Is_Drag == true && Input.mousePosition.x < Table.xMin)
            {
                OnTable = false;
                Change_Check = true;
                this.transform.SetParent(Letter_Area_Transform);
            }
        }

        Move_Limit(BaseDocument_Transform, Big_Border_Transform);
    }

    private void BaseDocument_On_Table()
    {
        BaseDocument_Small.SetActive(false);
        BaseDocument_Large.SetActive(true);
    }

    private void BaseDocument_Not_On_Table()
    {
        BaseDocument_Small.SetActive(true);
        BaseDocument_Large.SetActive(false);
    }

    void Move_Limit(RectTransform Move, RectTransform Limit)
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
