using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Select : MonoBehaviour
{
    public JsonReader JsonReader;
    public Sprite_Reader SpriteReader;
    public GameObject Select_Closed;
    public GameObject Select_Open;
    public Image Character_Sprite;
    public GameObject Char_Icon;

    public bool Is_Open;
    public string Selected_Character;
    public string Temp_Char;

    // Start is called before the first frame update
    void Start()
    {
        Is_Open = false;
        Change_Standing_Image();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change_UI()
    {
        if (Is_Open == false)
        {
            Select_Closed.SetActive(false);
            Select_Open.SetActive(true);
            Is_Open = true;
        }

        else
        {
            Select_Closed.SetActive(true);
            Select_Open.SetActive(false);
            Is_Open = false;
        }
    }

    public void Change_Standing_Image()
    {
        SpriteReader.LoadSprite(Character_Sprite, JsonReader.Character_Dictionary[Selected_Character].Standing_Sprite);
    }

    public void Change_Icon_Image()
    {
        for (int i = 0; i < Char_Icon.transform.childCount; i++)
        {
            if (Char_Icon.transform.GetChild(i).GetComponent<NPC_Icon>().Character_Name == Temp_Char)
            {
                SpriteReader.LoadSprite(Char_Icon.transform.GetChild(i).GetComponent<Image>(), JsonReader.Character_Dictionary[Temp_Char].Face_Sprite);
                break;
            }
        }
        Temp_Char = "";
    }
}
