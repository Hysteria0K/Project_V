using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Number_Stamp : MonoBehaviour
{ 
    public Number_Button Button_100;
    public Number_Button Button_10;
    public Number_Button Button_1;

    public int Stamp_Value = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load_Value()
    {
        Stamp_Value = Button_100.Number * 100 + Button_10.Number * 10 + Button_1.Number;
    }
}
