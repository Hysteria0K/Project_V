using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Reflection;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System;
using TMPro;

public class Dialogue_Manager : MonoBehaviour
{
    public TextMeshProUGUI Dialogue_Text;
    public TextMeshProUGUI Dialogue_Name;
    public GameObject Dialogue_Sprite1;
    public GameObject Dialogue_Sprite2;
    public GameObject Dialogue_Sprite3;

    public Dialogue_JsonReader JsonReader;
    public Sprite_Reader SpriteReader;

    public Image Next_Talk;

    [Header("Value")]
    [SerializeField] private string SceneName;
    [SerializeField] private int Index = 0;
    [SerializeField] private int MaxIndex = 0;
    [SerializeField] private bool Text_End;
    [SerializeField] private float Next_Talk_a = 1;
    [SerializeField] private bool Next_Talk_Full = true;
    [SerializeField] private bool Is_Auto = false;
    [SerializeField] private float Skip_Timer;

    [Header("Control")]
    [SerializeField] private float Text_delay;
    [SerializeField] private float Next_Talk_Speed = 1f;
    [SerializeField] private float Auto_Delay = 1.5f;

    [Space(15f)]
    public string Next_Scene_Name;
    public string Dialogue_Id;

    // Start is called before the first frame update
    private void Awake()
    {
        Dialogue_Id = Day_Saver.instance.Next_Dialogue_ID;
        Next_Scene_Name = Day_Saver.instance.Next_Scene_Name;
    }

    void Start()
    {
        Next_Dialogue(0, JsonReader.Dialogue_Dictionary[Dialogue_Id]);
        MaxIndex = JsonReader.Dialogue_Dictionary[Dialogue_Id].Count - 1;
    }

    void Update()
    {
        if (Text_End == true)
        {
            Next_Talk_Control();
        }

        if (Is_Auto == true && Text_End == true)
        {
            Skip_Timer += Time.deltaTime;

            if (Skip_Timer >= Auto_Delay)
            {
                Next();
            }
        }
    }

    private void Next_Dialogue(int index, Dictionary<int, Dialogue_JsonReader.Dialogue_Attributes> Json)
    {
        StartCoroutine(Dialogue_Output(Text_delay, Json[index].Text));
        Dialogue_Name.text = Json[index].Name;

        SpriteControl(Dialogue_Sprite1, Json[index].Sprite1, Json[index].Pos1);
        SpriteControl(Dialogue_Sprite2, Json[index].Sprite2, Json[index].Pos2);
        SpriteControl(Dialogue_Sprite3, Json[index].Sprite3, Json[index].Pos3);
    }

    private void SpriteControl(GameObject Sprite, string Json_Sprite, int Json_Sprite_Pos)
    {
        if (Json_Sprite != "")
        {
            Sprite.SetActive(true);
            Sprite.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, Json_Sprite_Pos);
            Sprite.GetComponent<Image>().sprite = GetSprite_From_Name(Json_Sprite);
        }

        else
        {
            Sprite.SetActive(false);
        }
    }

    private Sprite GetSprite_From_Name(string classname)
    {
        Type spriteReaderType = SpriteReader.GetType();

        FieldInfo fieldInfo = spriteReaderType.GetField(classname);

        return fieldInfo.GetValue(SpriteReader) as Sprite;
    }

    IEnumerator Dialogue_Output(float d, string text)
    {
        int count = 0;

        Dialogue_Text.text = "";

        Text_End = false;

        while (count != text.Length)
        {
            if (count < text.Length)
            {
                Dialogue_Text.text += text[count].ToString();
                count++;
            }

            yield return new WaitForSeconds(d);
        }

        Text_End = true;
        Skip_Timer = 0.0f;
    }

    private void Next_Talk_Control()
    {
        if (Next_Talk_a >= 1)
        {
            Next_Talk_Full = true;
        }

        else if (Next_Talk_a <= 0)
        {
            Next_Talk_Full = false;
        }

        if (Next_Talk_Full == true)
        {
            Next_Talk_a -= Time.deltaTime * Next_Talk_Speed;
            Next_Talk.color = new Color(Next_Talk.color.r, Next_Talk.color.g, Next_Talk.color.b, Next_Talk_a);
        }

        if (Next_Talk_Full == false)
        {
            Next_Talk_a += Time.deltaTime * Next_Talk_Speed;
            Next_Talk.color = new Color(Next_Talk.color.r, Next_Talk.color.g, Next_Talk.color.b, Next_Talk_a);
        }
    }

    public void Next()
    {
        if (Text_End == true)
        {
            Index++;
        }

        if (Index > MaxIndex)
        {
            Next_Scene();
            Text_End = false;
        }

        if (Index <= MaxIndex && Text_End == true)
        {
            Next_Dialogue(Index, JsonReader.Dialogue_Dictionary[Dialogue_Id]);
            Next_Talk_a = 1;
            Next_Talk.color = new Color(Next_Talk.color.r, Next_Talk.color.g, Next_Talk.color.b, Next_Talk_a);
        }
    }

    public void Auto_Change()
    {
        if (Is_Auto) Is_Auto = false;
        else Is_Auto = true;
    }

    public void Next_Scene()
    {
        SceneManager.LoadScene(Next_Scene_Name);
    }
}
