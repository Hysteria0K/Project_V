using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public bool Character_Ready;

    [Header("Control")]
    [SerializeField] private float Fade_Delay = 0.05f;
    [SerializeField] private float Wait_Delay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Is_Open = false;
        Character_Ready = true;
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

    public void Change_Standing_Image()
    {
        StartCoroutine(Fade_Change());
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

    IEnumerator Fade_Change()
    {
        Character_Ready = false;
        float temp = 1.0f;
        bool check = true;

        while (true)
        {
            yield return new WaitForSeconds(Fade_Delay);

            if (check)
            {
                temp -= Fade_Delay;
                Character_Sprite.color = new Color(255, 255, 255, temp);

                if (temp <= 0)
                {
                    check = false;
                    temp = 0;
                    SpriteReader.LoadSprite(Character_Sprite, JsonReader.Character_Dictionary[Selected_Character].Standing_Sprite);
                    Debug.Log("페이드 아웃");

                    yield return new WaitForSeconds(Wait_Delay);
                }
            }

            else
            {
                temp += Fade_Delay;
                Character_Sprite.color = new Color(255, 255, 255, temp);

                if (temp >= 1)
                {
                    Debug.Log("페이드 인");
                    Character_Ready = true;
                    break;
                }
            }
        }
    }
}
