using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Letter_Count;
    public int Score;
    [SerializeField] private bool Spawn_Check;

    public GameObject Letter;
    public RectTransform Letter_Area;
    public RectTransform Table_Area;
    public TextMeshProUGUI Timer_Text;
    public TextMeshProUGUI Score_Text;
    public Telephone_Saver Telephone_Saver;
    public GameObject Game_End;
    public TextMeshProUGUI Game_End_Day_Text;
    public TextMeshProUGUI Game_End_Score;
    public Letter_Spawner Letter_Spawner;
    public JsonReader JsonReader;

    public float Current_Time;

    public int Min;
    public int Sec;

    [Header("Control")]
    [SerializeField] private int Letter_Count_Max;
    public float Limit_Time;


    private void Awake()
    {
        Letter_Count = 0;
        Spawn_Check = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Letter_Count_Max = JsonReader.GameLevel_Dictionary[this.GetComponent<GameLevel>().Day].Letter_Count_Max;
        Limit_Time = JsonReader.GameLevel_Dictionary[this.GetComponent<GameLevel>().Day].Limit_Time;

        Current_Time = Limit_Time;

        Score_Update();
    }

    // Update is called once per frame
    void Update()
    {
        Current_Time -= Time.deltaTime;

        Min = (int)Current_Time / 60;
        Sec = (int)Current_Time % 60;

        Timer_Text.text = "Time : " + Min.ToString() + ":" + string.Format("{0:D2}", Sec);

        if (Current_Time <= 0)
        {
            EndGame();
        }
    }

    private void LateUpdate()
    {
        if (Letter_Count <= 0 && Spawn_Check == false && Telephone_Saver.IsLocked == false && Telephone_Saver.transform.childCount == 0)
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
            Letter_Spawner.Generate_Complete = false;
            Letter_Spawner.Spawn_Letter();
            while (!Letter_Spawner.Generate_Complete) { } // 생성이 완료될 때까지 대기
            Instantiate(Letter, Letter_Area.position, new Quaternion(0,0,0,0) , Table_Area); //Prototype = Letter_Area
        }

        Spawn_Check = false;
    }

    public void Score_Update()
    {
        Score_Text.text = "Score : " + Score.ToString();
    }

    private void EndGame()
    {
        Game_End.SetActive(true);
        Game_End_Day_Text.text = this.GetComponent<GameLevel>().Day.ToString() + "일차 종료!";
        Game_End_Score.text = "Score : " + Score.ToString();
        Score_Text.gameObject.SetActive(false);
        Timer_Text.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Next_Scene()
    {
        if (Day_Saver.instance != null)
        {
            Day_Saver.instance.Next_Dialogue_ID = JsonReader.GameLevel_Dictionary[this.GetComponent<GameLevel>().Day].End_Story_Id;
            Day_Saver.instance.Next_Scene_Name = "WriteLetter";
            Day_Saver.instance.WriteLetter_ID = JsonReader.GameLevel_Dictionary[this.GetComponent<GameLevel>().Day].Letter_Id;
        }
        Day_Saver.instance.Current_Scene_Name = "Dialogue";
        SceneManager.LoadScene("Dialogue");
    }
}
