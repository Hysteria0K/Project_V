using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulebookPage : MonoBehaviour
{
    public TextMeshProUGUI txt_title;
    public Transform Vertical_Layout_Group;

    void Start()
    {
        if (Vertical_Layout_Group != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Vertical_Layout_Group.GetComponent<RectTransform>());
        }
    }
}
