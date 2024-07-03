using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone_Saver : MonoBehaviour
{
    public GameObject Telephone;
    public RectTransform Canvas;

    public bool IsLocked;

    // Start is called before the first frame update
    void Start()
    {
        IsLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocked == false && this.transform.childCount != 0)
        {
            Spawn_Telephone();
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    public void Spawn_Telephone()
    {
        Telephone.GetComponent<Telephone>().Reason = this.transform.GetChild(0).GetComponent<Telephone_Saved>().Reason;
        Instantiate(Telephone, new Vector3(104, 801, 0), new Quaternion(0, 0, 0, 0), Canvas);
        IsLocked = true;
    }
}
