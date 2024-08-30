using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Icon : MonoBehaviour
{
    public NPC_Select Npc_Select;
    public Sprite_Reader SpriteReader;
    public JsonReader JsonReader;

    [Header("Character")]
    [SerializeField] private string Character_Name;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
