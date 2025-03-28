using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Reflection;
using System;

public class Letter : MonoBehaviour, IEndDragHandler, IPointerDownHandler
{
    private RectTransform Table_Area_Transform;
    private RectTransform Letter_Area_Transform;
    private RectTransform PostBox_Area_Transform;
    private RectTransform Big_Border_Transform;
    private JsonReader JsonReader;
    private RectTransform Telephone_Saver;
    private GameManager GameManager;
    private Sprite_Reader SpriteReader;

    private GameObject Table_Area;
    private GameObject Letter_Area;
    private GameObject PostBox_Area;

    private RectTransform Letter_Transform;

    private Rect Table_Rect;
    private Rect PostBox_Rect;
    private Rect Letter_Rect;

    public GameObject Letter_Small;
    public GameObject Letter_Large;
    public GameObject Telephone_Saved;
    public Drag_Drop Drag_Drop;
    public Image poststamp_image;

    [SerializeField] private bool OnTable;
    [SerializeField] private bool Change_Check;

    private float Half_Width;
    private float Half_Height;

    public int Spawn_Number;
    [SerializeField]private bool Spawned;

    private bool Spawn_Larger;

    [Header("Letter info")]
    [SerializeField] public int FirstName;
    [SerializeField] public int LastName;
    [SerializeField] public int Rank;
    [SerializeField] public int Regiment;
    [SerializeField] public int Battalion;
    [SerializeField] public int APO;
    [SerializeField] public int Force;
    [SerializeField] public int PostStamp;

    [Header("Text")]
    public TextMeshProUGUI Rank_Name_text;
    public TextMeshProUGUI Regiment_Battalion_text;
    public TextMeshProUGUI APO_text;
    public TextMeshProUGUI Force_text;

    [Header("Stamp")]
    public int Stamp_Value; 
    public bool Is_Stamp;
    public bool Is_Duplicated;
    public bool Valid;
    private string Reason;
    public bool Problem = false;

    [Header("Control")]
    public float Spawn_Y;
    public float Move_Speed;

    private void Awake()
    {
        Table_Area = GameObject.Find("Table_Area");
        Letter_Area = GameObject.Find("Letter_Area");
        //PostBox_Area = GameObject.Find("PostBox_Area");

        Table_Area_Transform = Table_Area.GetComponent<RectTransform>();
        Letter_Area_Transform = Letter_Area.GetComponent<RectTransform>();
        //PostBox_Area_Transform = PostBox_Area.GetComponent<RectTransform>();

        Big_Border_Transform = GameObject.Find("Big_Border").GetComponent<RectTransform>();
        JsonReader = GameObject.Find("JsonReader").GetComponent<JsonReader>();
        Telephone_Saver = GameObject.Find("Telephone_Saver").GetComponent<RectTransform>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpriteReader = GameObject.Find("Sprite_Reader").GetComponent<Sprite_Reader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Letter_Text();

        Letter_Transform = GetComponent<RectTransform>();

        OnTable = false;
        Change_Check = false;

        Table_Rect = Table_Area.GetComponent<Rect_Area>().Rect;
        Letter_Rect = Letter_Area.GetComponent<Rect_Area>().Rect;
        //PostBox_Rect = PostBox_Area.GetComponent<Rect_Area>().Rect;

        Stamp_Value = 0;

        Half_Width = Letter_Transform.rect.width / 2;
        Half_Height = Letter_Transform.rect.height / 2;

        Is_Stamp = false;
        Is_Duplicated = false;
        Valid = false;

        Spawned = true;

        Spawn_Y = JsonReader.Settings.settings[0].L_Spawn_Y;

        Move_Speed = JsonReader.Settings.settings[0].L_Move_Speed;

        Spawn_Larger = false;

        StartCoroutine(Spawn_Move());

        SpriteReader.LoadSprite(poststamp_image, JsonReader.PostStamp.poststamp[PostStamp].Sprite);
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawned != true)
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
                    Letter_Transform.position = Input.mousePosition;
                    Drag_Drop.Mouse_Center = true;
                }

                Change_Check = false;
            }
        }
    }

    private void Letter_Collider()
    {
        if (this.transform.parent != PostBox_Area_Transform)
        {
            if (Drag_Drop.Is_Drag == true && Input.mousePosition.y <= Letter_Rect.yMin)
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
                if (OnTable == true && Drag_Drop.Is_Drag == true && Input.mousePosition.y > Table_Rect.yMax)
                {
                    OnTable = false;
                    Change_Check = true;
                    this.transform.SetParent(Letter_Area_Transform);
                }
            }
            /*
            if (Drag_Drop.Is_Drag == true && Input.mousePosition.x >= PostBox_Rect.xMin && Input.mousePosition.y >= Table_Rect.yMax)
            {
                OnTable = false;
                Change_Check = true;
                this.transform.SetParent(PostBox_Area_Transform);
            }*/

            Move_Limit(Letter_Transform, Big_Border_Transform);
        }

        /*else
        {
            if (Drag_Drop.Is_Drag == true && Input.mousePosition.y < PostBox_Rect.yMin)
            {
                OnTable = true;
                Change_Check = true;
                this.transform.SetParent(Table_Area_Transform);
            }

            Move_Limit(Letter_Transform, PostBox_Area_Transform);
        }*/
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

    private void Letter_Text()
    {
        Rank_Name_text.text = JsonReader.Rank.militaryrank[Rank].Rank + ", " + 
                              JsonReader.NameList.namelist[FirstName].FirstName + " " +
                              JsonReader.NameList.namelist[LastName].LastName;
        Regiment_Battalion_text.text = JsonReader.ArmyUnit.armyunit[Battalion].Battalion + ", " +
                                       JsonReader.ArmyUnit.armyunit[Regiment].Regiment;
        //APO_text.text = "APO " + JsonReader.ArmyUnit.armyunit[APO].APO.TrimEnd('.', '0');
        //Force_text.text = JsonReader.ArmyUnit.armyunit[Force].Forces;
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

    void IEndDragHandler.OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (this.transform.parent == Letter_Area_Transform) //Prototype = PostBox_Area_Transform
        {
            Check_Valid();

            if (Valid == false)
            {
                Telephone_Saved.GetComponent<Telephone_Saved>().Reason = Reason;
                Instantiate(Telephone_Saved, Telephone_Saver);
            }

            else
            {
                GameManager.Score++;
                GameManager.Score_Update();
            }

            GameManager.Letter_Count--;
            Destroy(this.gameObject);
        }
    }

    private void Check_Valid()
    {
        // 정상적인 편지 분류
        if (string.Format("{0:D3}", Stamp_Value) == JsonReader.ArmyUnit.armyunit[APO].APO.TrimEnd('.', '0') && Is_Duplicated == false)
        {
            Valid = true;
            Debug.Log("문제없음");
        }

        else 
        {
            Valid = false;
            Reason = "Stamp";
            Debug.Log("도장 부적합");
        }

        // 비정상적인 편지 분류
        if (Problem == true && Stamp_Value == 9999 && Is_Duplicated == false)
        {
            Valid = true;
            Debug.Log("문제있는편지 분류 성공");
        }

        if (Problem == true && Stamp_Value != 9999)
        {
            Valid = false;
            Reason = "Stamp";
            Debug.Log("도장 부적합");
        }

    }
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Spawned == true)
        {
            Spawned = false;
        }
    }

    IEnumerator Spawn_Move()
    {
        while(this.transform.position.y >= Letter_Area_Transform.position.y + Spawn_Y && Spawned == true)
        {
            yield return new WaitForSeconds(0.01f);
            this.transform.position -= new Vector3(0, Move_Speed, 0);

            if (Spawn_Larger == false && this.transform.position.y + Letter_Large.GetComponent<RectTransform>().rect.height/2 <= Letter_Rect.yMin)
            {
                Letter_On_Table();
                Spawn_Larger = true;
            }
        }

        Spawned = false;
    }

}
