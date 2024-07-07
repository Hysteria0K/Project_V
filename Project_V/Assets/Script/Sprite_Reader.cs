using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class Sprite_Reader : MonoBehaviour
{
    [Header("Dummy")]
    #region 더미 리소스
    public Sprite testc;
    public Sprite charlotte;
    public Sprite leni;

    public Sprite Dumystamp_1;
    public Sprite Dumystamp_2;
    public Sprite Imvalidstamp;

    public Sprite D_308801;
    public Sprite D_308001;
    #endregion 더미 리소스

    private void Awake()
    {
        //Read_Sprite();
    }

    /* 동적할당
    private void Read_Sprite()
    {
        testc = Load_Sprite(testc, nameof(testc));
        charlotte = Load_Sprite(charlotte, nameof(charlotte));
        leni = Load_Sprite(leni, nameof(leni));
    }

    private Sprite Load_Sprite(Sprite img, string nameofimg)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/resource/imagefiles/" + nameofimg + ".png"));

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
    */
}
