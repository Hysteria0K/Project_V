using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice_UI : MonoBehaviour
{
    public bool Ready = false;

    [Header("Control")]
    [SerializeField] private float Delay_Time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade_In()
    {
        StartCoroutine(Fade_In_Coroutine());
    }

    public void Fade_Out()
    {
        StartCoroutine(Fade_Out_Coroutine());
    }

    IEnumerator Fade_In_Coroutine()
    {
        while (true)
        {
            this.GetComponent<CanvasGroup>().alpha += 0.1f;

            if (this.GetComponent<CanvasGroup>().alpha >= 1.0f)
            {
                break;
            }

            yield return new WaitForSeconds(Delay_Time);
        }

        Ready = true;
    }

    IEnumerator Fade_Out_Coroutine()
    {
        Ready = false;

        while (true)
        {
            this.GetComponent<CanvasGroup>().alpha -= 0.1f;

            if (this.GetComponent<CanvasGroup>().alpha <= 0.0f)
            {
                break;
            }

            yield return new WaitForSeconds(Delay_Time);
        }

        this.gameObject.SetActive(false);
    }
}
