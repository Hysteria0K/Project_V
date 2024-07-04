using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Letter_Count;
    [SerializeField] private int Letter_Count_Max;
    public int Score;
    [SerializeField] private bool Spawn_Check;

    public GameObject Letter;

    public RectTransform Letter_Area;


    private void Awake()
    {
        Letter_Count_Max = 3;
        Letter_Count = 0;
        Spawn_Check = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Letter_Count <= 0 && Spawn_Check == false) 
        {
            StartCoroutine(Spawn_Letter());
            Spawn_Check = true;
        }
    }

    IEnumerator Spawn_Letter()
    {
        while (Letter_Count < Letter_Count_Max)
        {
            yield return new WaitForSeconds(0.5f);
            Letter_Count++;
            Letter.GetComponent<Letter>().Spawn_Number = Letter_Count;
            Instantiate(Letter, Letter_Area.position - new Vector3(0, 700, 0), new Quaternion(0,0,0,0) ,Letter_Area);
        }

        Spawn_Check = false;
    }
}
