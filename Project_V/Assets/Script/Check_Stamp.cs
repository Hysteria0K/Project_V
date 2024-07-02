using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Check_Stamp : MonoBehaviour, IPointerDownHandler
{
    public GameObject Stamp;
    public GameObject Table_Area;
    public Stamp_Bar_Edge Stamp_Bar_Edge;

    private RectTransform Stamp_Transform;

    private RectTransform Letter_Large_Transform;

    private Rect Letter_Large_Rect;
    private Rect Stamp_Rect;

    private int Move_Count = 0;

    [SerializeField] private bool Is_Ready;

    [Header("Setting")]
    [SerializeField] private int Stamp_Value;

    // Start is called before the first frame update
    void Start()
    {
        Stamp_Transform = GetComponent<RectTransform>();
        Is_Ready = true;

        Stamp_Rect = new Rect(Stamp_Transform.position.x - Stamp_Bar_Edge.X_Position_Saved - Stamp_Transform.rect.width / 2,
        Stamp_Transform.position.y - Stamp_Transform.rect.height / 2 - 50 /*내려갔을때 감소하는 값 나중에 하드코딩 없앨때 하면댐*/,
        Stamp_Transform.rect.width, Stamp_Transform.rect.height);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Is_Ready == true)
        {
            StartCoroutine(StampDown());
            Is_Ready = false;
        }
    }

    IEnumerator StampDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            if (Move_Count >= 10)
            {
                Move_Count = 0;
                StartCoroutine(StampUp());
                break;
            }
            Stamp_Transform.position -= new Vector3(0, 5, 0);
            Move_Count++;
        }
    }

    IEnumerator StampUp()
    {
        GameObject Target = FindLetter(Table_Area);
        if (Target != null)
        {
            Instantiate(Stamp, Stamp_Transform.position, Stamp_Transform.rotation, Target.transform.GetChild(1).transform);

            if (Target.GetComponent<Letter>().Stamp_Value == 0 || Target.GetComponent<Letter>().Stamp_Value == this.Stamp_Value)
            {
                Target.GetComponent<Letter>().Stamp_Value = this.Stamp_Value;
            }

            else
            {
                Target.GetComponent<Letter>().Stamp_Value = 4;
            }
        }

        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            if (Move_Count >= 10)
            {
                Move_Count = 0;
                Is_Ready = true;
                break;
            }
            Stamp_Transform.position += new Vector3(0, 5, 0);
            Move_Count++;
        }
    }

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
}
