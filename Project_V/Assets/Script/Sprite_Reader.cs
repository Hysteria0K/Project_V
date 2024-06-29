using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class Sprite_Reader : MonoBehaviour
{
    public Sprite testc;
    private void Awake()
    {
        Read_Sprite();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Read_Sprite()
    {
        testc = Load_Sprite(testc, nameof(testc));
    }

    private Sprite Load_Sprite(Sprite img, string nameofimg)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/resource/imagefiles/" + nameofimg + ".png"));

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
