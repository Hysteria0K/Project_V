using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using System.Net;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Sprite_Reader : MonoBehaviour
{
    [Header("Dummy")]
    #region 더미 리소스
    public Sprite D_308801;
    public Sprite D_308001;
    #endregion 더미 리소스

    public Dictionary<string, AsyncOperationHandle<Sprite>> Sprite_Dictionary;

    private void Awake()
    {
        Sprite_Dictionary = new Dictionary<string, AsyncOperationHandle<Sprite>>();

        //LoadSprite(GetComponent<Image>(),"Dumystamp_1");
    }


    public void LoadSprite(Image target, string Address)
    {
        if (Sprite_Dictionary.ContainsKey(Address) == false)
        {
            var Load_Handle = Addressables.LoadAssetAsync<Sprite>(Address);
            Load_Handle.Completed += handle => OnSpriteLoaded(handle, Address, target);
        }

        else
        {
            target.sprite = Sprite_Dictionary[Address].Result;
        }
    }

    private void OnSpriteLoaded(AsyncOperationHandle<Sprite> handle, string Address, Image target)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(Address + "로드");
            Sprite_Dictionary.Add(Address, handle);

            target.sprite = Sprite_Dictionary[Address].Result;

        }
        else
        {
            Debug.LogError("Failed");
        }
    }

    public void Release_Sprite(string Address)
    {
        Addressables.Release(Sprite_Dictionary[Address]);
        Sprite_Dictionary.Remove(Address);
    }
}
