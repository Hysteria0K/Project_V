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

public class Dialogue_Manager_New : MonoBehaviour
{
    public TextMeshProUGUI Dialogue_Text;
    public TextMeshProUGUI Dialogue_Name;
    public GameObject Dialogue_Sprite1;
    public GameObject Dialogue_Sprite2;
    public GameObject Dialogue_Sprite3;
    public Image Background;
    public RectTransform Dialogue_Mask;

    public Dialogue_New_JsonReader JsonReader;
    public Sprite_Reader SpriteReader;

    public Image Next_Talk;
    public GameObject Main_Talk_Chr;

    [Header("Value")]
    [SerializeField] private string SceneName;
    [SerializeField] public int Index;
    [SerializeField] private int MaxIndex = 0;
    [SerializeField] private bool Text_End = true;
    [SerializeField] private float Next_Talk_a = 1;
    [SerializeField] private bool Next_Talk_Full = true;
    [SerializeField] private bool Is_Auto = false;
    [SerializeField] private float Skip_Timer;
    private Vector2 Text_Origin_Pos;

    private Coroutine AutoZoom;

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
    }

    void Start()
    {
        //PreLoad_Sprite();
        Next_Dialogue(Index, JsonReader.Dialogue_Dictionary[Dialogue_Id]);
        MaxIndex = JsonReader.Dialogue_Dictionary[Dialogue_Id].Count - 1;
    }

    void Update()
    {
        if (Text_End == true && JsonReader.Dialogue_Dictionary[Dialogue_Id][Index].Set != true) // 넘기기가 True 이고 텍스트 출력이 끝나면 깜빡이기
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

    private void Next_Dialogue(int index, Dictionary<int, Dialogue_New_JsonReader.Dialogue_Attributes> Json)
    {
        switch (Json[index].Type)
        {
            case "talk":
                {
                    if (Json[index].MainTalk_Sprite == "")
                    {
                        Main_Talk_Chr.SetActive(false);
                        StartCoroutine(Dialogue_Output(Text_delay, Json[index].Text));
                        Dialogue_Name.text = Json[index].Name;
                        break;
                    }
                    else
                    {
                        Main_Talk_Chr.SetActive(true);
                        Main_Talk_Chr.GetComponent<Image>().sprite = GetSprite_From_Name(Json[index].MainTalk_Sprite);
                        StartCoroutine(Dialogue_Output(Text_delay, Json[index].Text));
                        Dialogue_Name.text = Json[index].Name;
                        break;
                    }
                }
            case "order": // 케이스 이름을 테이블에 작성하면 작동함
                {
                    Text_End = true;

                    switch (Json[index].Cmd)
                    {
                        case "fade_out": // 배경 페이드 아웃
                            {
                                StartCoroutine(Fade(Background, false));
                                break;
                            }
                        case "fade_in": // 배경 페이드 인
                            {
                                SpriteReader.LoadSprite(Background, Json[index].Cmd_Target);
                                StartCoroutine(Fade(Background, true));
                                break;
                            }
                        case "auto_zoom_start":
                            {
                                AutoZoom = StartCoroutine(Auto_Zoom()); 
                                break;
                            }
                        case "auto_zoom_end":
                            {
                                StopCoroutine(AutoZoom); // 일단 자연스럽게 보이는게 중요한거 같아서 스케일 1.0으로 초기화 안 시켰음
                                break;
                            }
                        case "chr1_image_change": //캐릭터1 이미지 변경, Cmd_Target이 변경 이미지
                            {
                                Image_Change(Dialogue_Sprite1, Json[index].Cmd_Target);
                                break;
                            }
                        case "chr2_image_change": //캐릭터2 이미지 변경, Cmd_Target이 변경 이미지
                            {
                                Image_Change(Dialogue_Sprite2, Json[index].Cmd_Target);
                                break;
                            }
                        case "chr1_move": //캐릭터1 이동
                            {
                                Chr_Move(Dialogue_Sprite1, Json[index].Move, new Vector2(Json[index].Move_X, Json[index].Move_Y), Json[Index].Move_Spd);
                                break;
                            }
                        case "chr2_move": //캐릭터2 이동
                            {
                                Chr_Move(Dialogue_Sprite2, Json[index].Move, new Vector2(Json[index].Move_X, Json[index].Move_Y), Json[Index].Move_Spd);
                                break;
                            }
                    }
                    break;
                }
            default: break;
        }

        if (Json[index].Set) // Set가 True일 경우 테이블 바로 아래열도 동시에 처리함. 여러 명령을 해야할 때 True로 여러개 엮으면 됨. 대화와 동시에 할 경우 order가 대화보다 위에 있어야함
        {
            Index++;
            Next_Dialogue(Index, JsonReader.Dialogue_Dictionary[Dialogue_Id]);
        }
    }

    private void Image_Change(GameObject obj, string target) // 캐릭터 이미지 변경 용 함수
    {
        if (obj.activeInHierarchy == false) obj.transform.parent.gameObject.SetActive(true);
        obj.GetComponent<Image>().sprite = GetSprite_From_Name(target);
    }

    private void Chr_Move(GameObject obj, bool Move_Check, Vector2 Move_Target, float Move_Spd) // 캐릭터 이미지 이동
    {
        if (Move_Check == false) // 텔레포트
        {
            obj.GetComponent<RectTransform>().anchoredPosition = Move_Target;
        }

        else // 부드럽게 이동
        {
            StartCoroutine(Move(obj, Move_Target, Move_Spd));
        }
    }

    private Sprite GetSprite_From_Name(string classname) // SpriteReader에 미리 로드 되어있는 이미지 불러오기
    {
        Type spriteReaderType = SpriteReader.GetType();

        FieldInfo fieldInfo = spriteReaderType.GetField(classname);

        return fieldInfo.GetValue(SpriteReader) as Sprite;
    }

    #region 텍스트 출력 관련 코루틴 모음
    IEnumerator Dialogue_Output(float d, string text)
    {
        Text_End = false;

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

    #endregion

    IEnumerator Auto_Zoom() // 배경 확대/축소 효과
    {
        bool zoom = true;
        float scale = 1.0f;
        float MaxScale = 1.05f;

        while(true)
        {
            if (scale >= MaxScale) zoom = false;
            if (zoom == false && scale <= 1.0f ) zoom = true;

            if (zoom == true) scale += 0.0005f;
            else scale -= 0.0005f;

            Background.transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForSeconds(Fade_Delay);
        }
    }
    private void Next_Talk_Control() // 버튼 깜빡이는 연출
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

    public void Next() // 다음 대화
    {
        if (Index == MaxIndex)
        {
            Next_Scene();
        }

        if (Index <= MaxIndex && Text_End == true && JsonReader.Dialogue_Dictionary[Dialogue_Id][Index].Set != true)
        {
            Index++;
            Next_Dialogue(Index, JsonReader.Dialogue_Dictionary[Dialogue_Id]);
            Next_Talk_a = 1;
            Next_Talk.color = new Color(Next_Talk.color.r, Next_Talk.color.g, Next_Talk.color.b, Next_Talk_a);
        }
    }

    public void Auto_Change() // 오토 버튼
    {
        if (Is_Auto) Is_Auto = false;
        else Is_Auto = true;
    }

    public void Next_Scene() // 다음 씬으로 , 스킵 버튼
    {
        Day_Saver.instance.Saved_Dialogue_Index = 0;
        Day_Saver.instance.Current_Scene_Name = Next_Scene_Name;
        SceneManager.LoadScene(Next_Scene_Name);
    }

    IEnumerator Fade(Image target, bool fade_in) // 페이드 인 , 아웃
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
                    //Next(); // Next 활성화시 페이드 종료되면 바로 다음 인덱스
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
                    //Next(); // Next 활성화시 페이드 종료되면 바로 다음 인덱스
                    break;
                }
            }
        }
    }

    IEnumerator Move(GameObject obj, Vector2 target, float speed) // 서서히 움직이기 (움직임이 끝날때까지 Text_End 비활성화)
    {
        while (true)
        {
            obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(obj.GetComponent<RectTransform>().anchoredPosition, target, speed * Time.deltaTime);
            Text_End = false;

            yield return new WaitForSeconds(0.01f);

            if (Vector2.Distance(obj.GetComponent<RectTransform>().anchoredPosition, target) < 0.1f)
            {
                obj.GetComponent<RectTransform>().anchoredPosition = target;
                Text_End = true;
                break;
            }
        }
    }
    private void PreLoad_Sprite() // 스프라이트 미리 불러오기
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