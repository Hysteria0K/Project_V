using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rect_Area : MonoBehaviour
{

    public Rect Rect;
    private RectTransform this_Transform;

    private void Awake()
    {
        this_Transform = this.GetComponent<RectTransform>();

        Rect = new Rect(this_Transform.position.x - this_Transform.rect.width / 2,
        this_Transform.position.y - this_Transform.rect.height / 2,
        this_Transform.rect.width, this_Transform.rect.height);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
