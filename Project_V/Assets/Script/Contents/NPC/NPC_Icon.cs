using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPC_Icon : MonoBehaviour, IPointerDownHandler
{
    public NPC_Select Npc_Select;
    public Sprite_Reader SpriteReader;
    public JsonReader JsonReader;

    [Header("Character")]
    public string Character_Name;

    [Header("���߿����̺굥���ͷλ���")]
    public bool Available;

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.root.name == "PreLoad")
        {
            if (Npc_Select.Selected_Character == Character_Name || Available == false)
            {
                SpriteReader.LoadSprite(this.GetComponent<Image>(), JsonReader.Character_Dictionary[Character_Name].Face_Sprite_Mono);
                this.transform.root.GetComponent<NPC_Image_PreLoad>().Loaded_Count++;
            }

            else
            {
                SpriteReader.LoadSprite(this.GetComponent<Image>(), JsonReader.Character_Dictionary[Character_Name].Face_Sprite);
                this.transform.root.GetComponent<NPC_Image_PreLoad>().Loaded_Count++;
            }
        }

        // ���߿� ���� Unload���� ����
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (Npc_Select.Selected_Character != Character_Name && Npc_Select.Character_Ready == true)
        {
            Npc_Select.Temp_Char = Npc_Select.Selected_Character;
            Npc_Select.Selected_Character = Character_Name;
            Npc_Select.Change_Standing_Image();
            SpriteReader.LoadSprite(this.GetComponent<Image>(), JsonReader.Character_Dictionary[Character_Name].Face_Sprite_Mono);
            Npc_Select.Change_Icon_Image();
        }
    }
}
