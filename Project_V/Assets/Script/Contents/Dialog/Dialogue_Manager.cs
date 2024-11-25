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
using Unity.VisualScripting;
using System.Globalization;
using System.Xml;

public class Dialogue_Manager : MonoBehaviour
{
    public TextMeshProUGUI Dialogue_Text;
    public TextMeshProUGUI Dialogue_Name;
    public GameObject Dialogue_Sprite1;
    public GameObject Dialogue_Sprite2;
    public GameObject Dialogue_Sprite3;
    public Image Background;
    public RectTransform Dialogue_Mask;

    public Dialogue_JsonReader JsonReader;
    public Sprite_Reader SpriteReader;

    public Image Next_Talk;
    public GameObject Main_Talk_Chr;

    [Header("Value")]
    [SerializeField] private string SceneName;
    [SerializeField] public int Index;
    [SerializeField] private int MaxIndex = 0;
    [SerializeField] private bool Text_End;
    [SerializeField] private float Next_Talk_a = 1;
    [SerializeField] private bool Next_Talk_Full = true;
    [SerializeField] private bool Is_Auto = false;
    [SerializeField] private float Skip_Timer;
    private Vector2 Text_Origin_Pos;

    [Header("Control")]
    [SerializeField] private float Text_delay;
    [SerializeField] private float Next_Talk_Speed = 1f;
    [SerializeField] private float Auto_Delay = 1.5f;
    [SerializeField] private float Fade_Delay = 0.01f;
    [SerializeField] private float Wait_Delay = 1.0f;
    [SerializeField] private float Text_Speed = 10f; //���� ������ �������� �ӵ�

    [Space(15f)]
    public string Next_Scene_Name;
    public string Dialogue_Id;

    // Start is called before the first frame update
    private void Awake()
    {
        Dialogue_Id = Day_Saver.instance.Next_Dialogue_ID;
        Next_Scene_Name = Day_Saver.instance.Next_Scene_Name;
        Index = Day_Saver.instance.Saved_Dialogue_Index;

        Text_Origin_Pos = new Vector2(Screen.width / 2, Dialogue_Text.rectTransform.position.y);
        Debug.Log(Text_Origin_Pos);
    }

    void Start()
    {
        //PreLoad_Sprite();
        Next_Dialogue(Index, JsonReader.Dialogue_Dictionary[Dialogue_Id]);
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
        if (Json[index].BG != "")
        {
            SpriteReader.LoadSprite(Background, Json[index].BG);
        }

        SpriteControl(Dialogue_Sprite1, Json[index].Sprite1, Json[index].Pos1);
        SpriteControl(Dialogue_Sprite2, Json[index].Sprite2, Json[index].Pos2);
        SpriteControl(Dialogue_Sprite3, Json[index].Sprite3, Json[index].Pos3);

        switch (Json[index].Type)
        {
            case "main_talk":
                {
                    Main_Talk_Chr.SetActive(true);
                    Main_Talk_Chr.GetComponent<Image>().sprite = GetSprite_From_Name(Json[index].MainTalk_Sprite);
                    StartCoroutine(Dialogue_Output(Text_delay, Json[index].Text));
                    Dialogue_Name.text = Json[index].Name;
                    break;
                }

            case "talk":
                {
                    Main_Talk_Chr.SetActive(false);
                    StartCoroutine(Dialogue_Output(Text_delay, Json[index].Text));
                    Dialogue_Name.text = Json[index].Name;
                    break;
                }
            case "order":
                {
                    switch (Json[index].Name)
                    {
                        case "fade_out":
                            {
                                switch (Json[index].Text)
                                {
                                    case "background":
                                        {
                                            StartCoroutine(Fade(Background, false));
                                            break;
                                        }
                                }
                                break;
                            }
                        case "fade_in":
                            {
                                switch (Json[index].Text)
                                {
                                    case "background":
                                        {
                                            StartCoroutine(Fade(Background, true));
                                            break;
                                        }
                                }
                                break;
                            }
                    }
                    break;
                }
            default: break;
        }
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
        Vector2 Size = new Vector2(0, Dialogue_Mask.sizeDelta.y);
        Dialogue_Mask.sizeDelta = Size;

        Dialogue_Text.text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(Dialogue_Text.rectTransform);
        float Dialogue_Size = Dialogue_Text.rectTransform.sizeDelta.x;

        Dialogue_Mask.position = new Vector2(Text_Origin_Pos.x - Dialogue_Size / 2, Text_Origin_Pos.y);
        Dialogue_Text.rectTransform.position = Text_Origin_Pos;

        StartCoroutine(Text_Alpha());

        while (Size.x <= Dialogue_Size)
        {
            Size.x += Text_Speed;
            Dialogue_Mask.sizeDelta = Size;

            yield return new WaitForSeconds(d);
        }

        Size.x += Text_Speed;
        Dialogue_Mask.sizeDelta = Size;
        Text_End = true;
        Skip_Timer = 0.0f;
    }

    IEnumerator Text_Alpha()
    {
        Dialogue_Text.ForceMeshUpdate();
        TMP_TextInfo textInfo = Dialogue_Text.textInfo;

        Dialogue_Text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        int Text_Index = 0;

        while (Text_Index < Dialogue_Text.text.Length)
        {
            StartCoroutine(ChangeAlphaSequentially(textInfo, Dialogue_Text, Text_Index));
            Text_Index++;
            yield return new WaitForSeconds(0.08f);
        }
    }

    IEnumerator ChangeAlphaSequentially(TMP_TextInfo textInfo, TMP_Text tmpText, int Index)
    {
        if (!textInfo.characterInfo[Index].isVisible)
            yield break;

        float alpha = 0.0f;

        // 글자의 Vertex 색상 변경
        int vertexIndex = textInfo.characterInfo[Index].vertexIndex;
        int materialIndex = textInfo.characterInfo[Index].materialReferenceIndex;

        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

        while (alpha <= 1.0f)
        {
            alpha += 0.05f;
            // 기존 색상의 알파값만 변경
            byte newAlpha = (byte)(255 * alpha);
            for (int j = 0; j < 4; j++) // 글자의 4개 정점에 알파값 적용
            {
                vertexColors[vertexIndex + j].a = newAlpha;
            }

            // **텍스트 메쉬 갱신**
            tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            // 다음 알파값으로 넘어가기 전 대기
            yield return new WaitForSeconds(0.01f);
        }
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
            Text_End = false;
        }
    }

    public void Auto_Change()
    {
        if (Is_Auto) Is_Auto = false;
        else Is_Auto = true;
    }

    public void Next_Scene()
    {
        Day_Saver.instance.Saved_Dialogue_Index = 0;
        Day_Saver.instance.Current_Scene_Name = Next_Scene_Name;
        SceneManager.LoadScene(Next_Scene_Name);
    }

    IEnumerator Fade(Image target, bool fade_in)
    {
        float temp;
        Text_End = false;

        if (fade_in)
        {
            temp = 0;
        }

        else
        {
            temp = 1;
        }

        target.color = new Color(temp, temp, temp);

        while (true)
        {
            yield return new WaitForSeconds(Fade_Delay);

            if (fade_in)
            {
                temp += Fade_Delay;
                target.color = new Color(temp, temp, temp);

                if (temp >= 1)
                {
                    temp = 1;
                    target.color = new Color(temp, temp, temp);
                    Text_End = true;
                    Next();
                    break;
                }
            }

            else
            {
                temp -= Fade_Delay;
                target.color = new Color(temp, temp, temp);

                if (temp <= 0)
                {
                    temp = 0;
                    target.color = new Color(temp, temp, temp);

                    yield return new WaitForSeconds(Wait_Delay); // �ϴ� ����������� ������ ����

                    Text_End = true;
                    Next();
                    break;
                }
            }
        }
    }

    private void PreLoad_Sprite()
    {
        for (int i = 0; i <= MaxIndex; i++)
        {
            if (JsonReader.Dialogue_Dictionary[Dialogue_Id][i].MainTalk_Sprite != "")
            {
                SpriteReader.LoadSprite(Main_Talk_Chr.GetComponent<Image>(), JsonReader.Dialogue_Dictionary[Dialogue_Id][i].MainTalk_Sprite);
            }
        }

    }
}