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

    public GameObject Letters;

    private RectTransform Stamp_Transform;

    private int Move_Count = 0;
    [SerializeField] private bool Is_Ready;
    // Start is called before the first frame update
    void Start()
    {
        Stamp_Transform = GetComponent<RectTransform>();
        Is_Ready = true;
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
        Instantiate(Stamp, Stamp_Transform.position, Stamp_Transform.rotation, FindLetter(Letters).transform.GetChild(1).transform);

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
            if (child.GetComponent<Letter>().Stamp_Ready == true)
            {
                return child;
            }
        }
        return null;
    }
}
