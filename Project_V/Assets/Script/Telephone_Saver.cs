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
        Delay_Time = JsonReader.Settings.settings.Delay_Time;
        IsLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocked == false && this.transform.childCount != 0)
        {
            StartCoroutine(Delay());
            IsLocked = true;
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
