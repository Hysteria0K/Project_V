using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Scene_Manager : MonoBehaviour
{
    public GameObject Save_UI;

    [Header("Control")]
    [SerializeField] private float Fade_Delay = 0.02f;
    [SerializeField] private float Fade_Value = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load_Prologue()
    {
        Day_Saver.instance.Next_Dialogue_ID = "1_Begin";
        Day_Saver.instance.WriteLetter_ID = "Test";
        // 이 부분은 일단 하드코딩 해둠

        Day_Saver.instance.Day = 1;
        Day_Saver.instance.Next_Scene_Name = "Play";
        Day_Saver.instance.Current_Scene_Name = "Dialogue";
        Day_Saver.instance.Saved_Dialogue_Index = 0;

        SceneManager.LoadScene("Dialogue");
    }

    public void Save_UI_Enable()
    {
        Save_UI.SetActive(true);
        StartCoroutine(Fade_In_UI(Save_UI));
    }

    public void Save_UI_Disable()
    {
        StartCoroutine(Fade_Out_UI(Save_UI));
    }

    IEnumerator Fade_In_UI(GameObject UI)
    {
        float temp = 0.0f;
        UI.GetComponent<CanvasGroup>().alpha = temp;

        while (true)
        {
            if (temp >= 1.0f)
            {
                UI.GetComponent<Save_UI>().UI_Ready = true;
                break;
            }
            temp += Fade_Value;
            UI.GetComponent<CanvasGroup>().alpha = temp;

            yield return new WaitForSeconds(Fade_Delay);
        }
    }

    IEnumerator Fade_Out_UI(GameObject UI)
    {
        float temp = 1.0f;
        UI.GetComponent<CanvasGroup>().alpha = temp;
        UI.GetComponent<Save_UI>().UI_Ready = false;

        while (true)
        {
            if (temp <= 0.0f)
            {
                UI.SetActive(false);
                break;
            }
            temp -= Fade_Value;
            UI.GetComponent<CanvasGroup>().alpha = temp;

            yield return new WaitForSeconds(Fade_Delay);
        }
    }
}
