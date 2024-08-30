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
    public bool Is_Open;
    public string Selected_Character;
    public Image Character_Sprite;

    // Start is called before the first frame update
    void Start()
    {
        Is_Open = false;
        SpriteReader.LoadSprite(Character_Sprite, JsonReader.Character_Dictionary[Selected_Character].Standing_Sprite);
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
}
