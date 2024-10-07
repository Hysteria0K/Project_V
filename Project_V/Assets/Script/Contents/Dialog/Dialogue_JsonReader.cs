using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static JsonReader;

public class Dialogue_JsonReader : MonoBehaviour
{
    #region Attributes
    [System.Serializable]
    public class Dialogue_Attributes
    {
        public string Id;
        public int Index;
        public string Name;
        public string Text;
        public string Sprite1;
        public int Pos1;
        public string Sprite2;
        public int Pos2;
        public string Sprite3;
        public int Pos3;
    }


    #endregion

    #region Parse
    public class Dialogue_Parse
    {
        public Dialogue_Attributes[] dialogue;
    }


    #endregion

    public Dialogue_Parse Dialogue;

    public Dictionary<string, Dictionary<int, Dialogue_Attributes>> Dialogue_Dictionary;

    private void Awake()
    {
        Dialogue = JsonUtility.FromJson<Dialogue_Parse>(ReadJson("dialogue"));

        Dialogue_Dictionary = new Dictionary<string, Dictionary<int, Dialogue_Attributes>>();
        Dialogue_To_Dictionary();

        Debug.Log("JsonParse Complete");

    }
    private string ReadJson(string Filename)
    {
#if UNITY_EDITOR
        string JsonText = File.ReadAllText("Assets/StreamingAssets/resource/test/" + Filename + ".json");

        return (JsonText);
#else
        string JsonText = AESUtil.Decrypt(File.ReadAllText(Application.persistentDataPath + "/resource/jsonfiles/" + Filename + ".json"),Json_Patcher.AES_key, Json_Patcher.AES_iv);

        return (JsonText);
#endif
    }

    private void Dialogue_To_Dictionary()
    {
        Dictionary<int, Dialogue_Attributes> Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();

        string Saved_ID = Dialogue.dialogue[0].Id;

        for (int i = 0; i < Dialogue.dialogue.Length; i++)
        {
            if (Saved_ID != Dialogue.dialogue[i].Id)
            {
                //Debug.Log("º¯È¯");
                Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);
                Temp_dictionary = new Dictionary<int, Dialogue_Attributes>();
                Saved_ID = Dialogue.dialogue[i].Id;
            }

            Temp_dictionary.Add(Dialogue.dialogue[i].Index, Dialogue.dialogue[i]);
        }

        Dialogue_Dictionary.Add(Saved_ID, Temp_dictionary);
    }

}
