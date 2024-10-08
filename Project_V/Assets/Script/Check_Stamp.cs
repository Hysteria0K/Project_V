using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;

public class Check_Stamp : MonoBehaviour, IEndDragHandler
{
    public GameObject Stamp;
    public GameObject Table_Area;
    public Stamp_Bar_Edge Stamp_Bar_Edge;
    public Transform Drawer_Right;
    public GameObject Drawer_Right_BG;
    public Transform Front_Table_Area;

    public RectTransform Big_Border;

    private Vector3 Origin_Pos;

    private RectTransform Stamp_Transform;

    private RectTransform Letter_Large_Transform;

    private Drag_Drop Drag_Drop;

    private Rect Letter_Large_Rect;
    [SerializeField] private Rect Stamp_Rect;

    private bool Is_Ready;
    private bool OnTable;

    [Header("Setting")]
    public int Stamp_Value;

    [Header("Coroutine Values")]
    [SerializeField] private float Stamp_Down;
    [SerializeField] private Vector3 Stamp_Down_Position;
    [SerializeField] private Vector3 Stamp_Up_Position;

    private Vector3 Instance_Pos;
    private bool Stamp_Perfect;
    // Start is called before the first frame update
    void Start()
    {
        Stamp_Transform = Stamp.GetComponent<RectTransform>();
        Drag_Drop = this.GetComponent<Drag_Drop>();
        Is_Ready = true;
        OnTable = false;

        Stamp_Down = 50;

        /*Stamp_Rect = new Rect(this.transform.position.x - Stamp_Bar_Edge.X_Position_Saved - Stamp.GetComponent<RectTransform>().rect.width / 2,
        this.transform.position.y - this.GetComponent<RectTransform>().rect.height / 2 - Stamp_Down,
        Stamp.GetComponent<RectTransform>().rect.width, Stamp.GetComponent<RectTransform>().rect.height);*/

        Stamp_Rect = new Rect(this.transform.position.x - Stamp.GetComponent<RectTransform>().rect.width / 2,
        this.transform.position.y - this.GetComponent<RectTransform>().rect.height / 2 - Stamp_Down,
        Stamp.GetComponent<RectTransform>().rect.width, Stamp.GetComponent<RectTransform>().rect.height);

        /*Stamp_Down_Position = new Vector3(this.transform.position.x - Stamp_Bar_Edge.X_Position_Saved, this.transform.position.y - Stamp_Down, this.transform.position.z);
        Stamp_Up_Position = new Vector3(this.transform.position.x - Stamp_Bar_Edge.X_Position_Saved, this.transform.position.y, this.transform.position.z);*/

        Stamp_Down_Position = new Vector3(this.transform.position.x, this.transform.position.y - Stamp_Down, this.transform.position.z);
        Stamp_Up_Position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        Stamp_Perfect = false;

        Origin_Pos = this.transform.position;
    }

    void Update()
    {
        Stamp_Collider();
    }

    #region 도장 찍기 함수
    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Stamp_Rect = new Rect(this.transform.position.x - Stamp.GetComponent<RectTransform>().rect.width / 2,
        this.transform.position.y - this.GetComponent<RectTransform>().rect.height / 2 - Stamp_Down,
        Stamp.GetComponent<RectTransform>().rect.width, Stamp.GetComponent<RectTransform>().rect.height);

        Stamp_Down_Position = new Vector3(this.transform.position.x, this.transform.position.y - Stamp_Down, this.transform.position.z);
        Stamp_Up_Position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        if (Drag_Drop.Is_Drag == false && Input.mousePosition.y > Front_Table_Area.GetComponent<Rect_Area>().Rect.yMax)
        {
            OnTable = false;
            this.transform.SetParent(Drawer_Right);
            this.transform.position = Origin_Pos;
        }
    }

    IEnumerator StampDown()
    {
        if (this.GetComponent<Number_Stamp>() != null)
        {
            Stamp_Value = this.GetComponent<Number_Stamp>().Stamp_Value;
            this.GetComponent<Number_Stamp>().Button_100.GetComponent<Button>().interactable = false;
            this.GetComponent<Number_Stamp>().Button_10.GetComponent<Button>().interactable = false;
            this.GetComponent<Number_Stamp>().Button_1.GetComponent<Button>().interactable = false;
        }

        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            if (this.transform.position == Stamp_Down_Position)
            {
                StartCoroutine(StampUp());
                break;
            }
            this.transform.position -= new Vector3(0, Stamp_Down / 10, 0);
        }
    }

    IEnumerator StampUp()
    {
        GameObject Target = FindLetter(Table_Area);
        if (Target != null)
        {
            Instance_Pos = new Vector3(this.transform.position.x, this.transform.position.y - (this.GetComponent<RectTransform>().rect.height - Stamp_Transform.rect.height) / 2, this.transform.position.z);

            if (Stamp.GetComponent<TextMeshProUGUI>() != false)
            {
                Stamp.GetComponent<TextMeshProUGUI>().text = string.Format("{0:D3}", Stamp_Value);
            }

            Instantiate(Stamp, Instance_Pos, this.transform.rotation, Target.transform.GetChild(1).transform);

            if (Target.GetComponent<Letter>().Is_Stamp == false && Stamp_Perfect == true)
            {
                Target.GetComponent<Letter>().Stamp_Value = this.Stamp_Value;
                Target.GetComponent<Letter>().Is_Stamp = true;
            }

            else
            {
                Target.GetComponent<Letter>().Is_Duplicated = true;
            }
        }

        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            if (this.transform.position == Stamp_Up_Position)
            {
                if (this.GetComponent<Number_Stamp>() != null)
                {
                    this.GetComponent<Number_Stamp>().Button_100.GetComponent<Button>().interactable = true;
                    this.GetComponent<Number_Stamp>().Button_10.GetComponent<Button>().interactable = true;
                    this.GetComponent<Number_Stamp>().Button_1.GetComponent<Button>().interactable = true;
                }

                Is_Ready = true;
                Stamp_Bar_Edge.Click_On = true;
                this.GetComponent<Drag_Drop>().enabled = true;
                break;
            }
            this.transform.position += new Vector3(0, Stamp_Down / 10, 0);
        }

        Stamp_Perfect = false;
    }

    #endregion 도장 찍기 함수

    private GameObject FindLetter(GameObject Parent)
    {
        int Child_Count = Parent.transform.childCount;

        for (int i = Child_Count - 1; i >= 0; i--)
        {
            GameObject child = Parent.transform.GetChild(i).gameObject;
            Letter Target = child.GetComponent<Letter>();

            if (Target != null)
            {
                if (Find_Letter_Large(child))
                {
                    return child;
                }
            }
        }
        return null;
    }

    private bool Find_Letter_Large(GameObject Letter)
    {
        int Child_Count = Letter.transform.childCount;

        GameObject Letter_Large = Letter.transform.GetChild(Child_Count - 1).gameObject;

        Letter_Large_Transform = Letter_Large.GetComponent<RectTransform>();

        Letter_Large_Rect = new Rect(Letter_Large_Transform.position.x - Letter_Large_Transform.rect.width / 2,
                                    Letter_Large_Transform.position.y - Letter_Large_Transform.rect.height / 2,
                                    Letter_Large_Transform.rect.width, Letter_Large_Transform.rect.height);

        if (IsRectContained(Stamp_Rect, Letter_Large_Rect))
        {
            if (IsRectContained_Perfect(Stamp_Rect, Letter_Large_Rect) != false)
            {
                Stamp_Perfect = true;
                Debug.Log("잘 찍힘");
            }
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool IsRectContained(Rect StampRect, Rect LargeLetterRect)
    {
        return LargeLetterRect.Contains(new Vector2(StampRect.xMin, StampRect.yMin)) ||
              LargeLetterRect.Contains(new Vector2(StampRect.xMax, StampRect.yMin)) ||
              LargeLetterRect.Contains(new Vector2(StampRect.xMin, StampRect.yMax)) ||
              LargeLetterRect.Contains(new Vector2(StampRect.xMax, StampRect.yMax));
    }

    private bool IsRectContained_Perfect(Rect StampRect, Rect LargeLetterRect)
    {
        return LargeLetterRect.Contains(new Vector2(StampRect.xMin, StampRect.yMin)) &&
              LargeLetterRect.Contains(new Vector2(StampRect.xMax, StampRect.yMin)) &&
              LargeLetterRect.Contains(new Vector2(StampRect.xMin, StampRect.yMax)) &&
              LargeLetterRect.Contains(new Vector2(StampRect.xMax, StampRect.yMax));
    }

    public void Stamp_Work()
    {
        if (Is_Ready == true && OnTable == true)
        {
            StartCoroutine(StampDown());
            Is_Ready = false;
            this.GetComponent<Drag_Drop>().enabled = false;
            //Stamp_Bar_Edge.Click_On = false;
        }
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
        if (rectCorners[0].x < boundingCorners[0].x)
        {
            limitedPosition.x += boundingCorners[0].x - rectCorners[0].x;
        }

        // 오른쪽 경계 제한
        if (rectCorners[2].x > boundingCorners[2].x)
        {
            limitedPosition.x -= rectCorners[2].x - boundingCorners[2].x;
        }

        // 아래쪽 경계 제한
        if (rectCorners[0].y < boundingCorners[0].y )
        {
            limitedPosition.y += boundingCorners[0].y - rectCorners[0].y;
        }

        // 위쪽 경계 제한
        if (rectCorners[1].y > boundingCorners[1].y)
        {
            limitedPosition.y -= rectCorners[1].y - boundingCorners[1].y;
        }

        // 제한된 위치 적용
        Move.position = limitedPosition;
    }
    private void Stamp_Collider()
    {
        if (Drag_Drop.Is_Drag == true && Input.mousePosition.y <= Drawer_Right_BG.GetComponent<Rect_Area>().Rect.yMin)
        {
            if (OnTable == false)
            {
                OnTable = true;
                this.transform.SetParent(Front_Table_Area);
            }
        }

        if (Is_Ready == true)
        {
            Move_Limit(this.GetComponent<RectTransform>(), Big_Border);
        }
    }
}
