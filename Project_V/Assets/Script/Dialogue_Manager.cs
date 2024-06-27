using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue_Manager : MonoBehaviour
{
    public Text Dialogue_Text;
    public Text Dialogue_Name;
    public GameObject Dialogue_Sprite1;
    public GameObject Dialogue_Sprite2;
    public GameObject Dialogue_Sprite3;

    public Test_JsonReader JsonReader;

    [SerializeField]
    private string SceneName;
    private int Index = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        SceneName = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        switch(SceneName)
        {
            case "Test_Dialogue":
                {
                    Next_Dialogue(0, JsonReader.Test_Dialogue.Test);
                    break;
                }

            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Index < JsonReader.Test_Dialogue.Test.Length-1)
        {
            Index++;
            Next_Dialogue(Index, JsonReader.Test_Dialogue.Test);
        }
    }

    private void Next_Dialogue(int index, Test_JsonReader.Test_Dialogue_Attributes[] Json)
    {
        Dialogue_Text.text = Json[index].Text;
        Dialogue_Name.text = Json[index].Name;

        SpriteControl(Dialogue_Sprite1, Json[index].Sprite1, Json[index].Pos1, Json[index].Layer1);
        SpriteControl(Dialogue_Sprite2, Json[index].Sprite2, Json[index].Pos2, Json[index].Layer2);
        SpriteControl(Dialogue_Sprite3, Json[index].Sprite3, Json[index].Pos3, Json[index].Layer3);
    }

    private void SpriteControl(GameObject Sprite, string Json_Sprite, int Json_Sprite_Pos, int Json_Sprite_Layer)
    {
        if (Json_Sprite != "")
        {
            Debug.Log(Json_Sprite);
            Sprite.SetActive(true);
            Sprite.transform.position = new Vector3(960 +Json_Sprite_Pos, 540, Json_Sprite_Layer);
        }

        else
        {
            Sprite.SetActive(false);
        }
    }
}
