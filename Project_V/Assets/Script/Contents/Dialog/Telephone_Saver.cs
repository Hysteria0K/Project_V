using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone_Saver : MonoBehaviour
{
    public JsonReader JsonReader;
    public GameObject Telephone;
    public RectTransform Canvas;

    public bool IsLocked;

    [Header("Control")]
    [SerializeField] private float Delay_Time;

    // Start is called before the first frame update
    void Start()
    {
        Delay_Time = JsonReader.Settings.settings[0].Telephone_Delay;
        IsLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocked == false && this.transform.childCount != 0)
        {
            IsLocked = true;
            StartCoroutine(Delay());
        }
    }

    public void Spawn_Telephone()
    {
        Telephone.GetComponent<Telephone>().Reason = this.transform.GetChild(0).GetComponent<Telephone_Saved>().Reason;
        Instantiate(Telephone, new Vector3(104, 601, 0), new Quaternion(0, 0, 0, 0), Canvas);
        Destroy(this.transform.GetChild(0).gameObject);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(Delay_Time);
        Spawn_Telephone();
    }
}
