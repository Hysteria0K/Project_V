using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    public RectTransform Table_Area_Transform;

    private RectTransform Letter_Transform;

    private Rect rect1;
    private Rect rect2;

    public GameObject Letter_Small;
    public GameObject Letter_Large;

    [SerializeField]
    private bool OnTable;
    private bool Change_Check;

    private float OriginWidth;
    private float OriginHeight;
    // Start is called before the first frame update
    void Start()
    {
        Letter_Transform = GetComponent<RectTransform>();

        OnTable = false;
        Change_Check = false;

        OriginWidth = Letter_Transform.rect.width;
        OriginHeight= Letter_Transform.rect.height;
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
        rect1 = new Rect(Table_Area_Transform.position.x - Table_Area_Transform.rect.width / 2,
                        Table_Area_Transform.position.y - Table_Area_Transform.rect.height / 2,
                        Table_Area_Transform.rect.width, Table_Area_Transform.rect.height);
        rect2 = new Rect(Letter_Transform.position.x - OriginWidth / 2,
                         Letter_Transform.position.y - OriginHeight / 2,
                         OriginWidth, OriginHeight);

        if (rect1.Overlaps(rect2))
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
}
