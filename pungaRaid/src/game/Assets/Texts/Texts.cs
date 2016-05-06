using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

public class Texts :MonoBehaviour {

    public List<string> tutorial;
    public TextsRuleta ruleta;
    public TextsMoods moods;

    string json_tutorials_Url = "texts/tutorial";
    string json_ruleta_Url = "texts/ruleta";
    string json_moods_Url = "texts/moods";

    void Start()
    {
        Encoding utf8 = Encoding.UTF8;

        TextAsset file;
        
        file = Resources.Load(json_tutorials_Url) as TextAsset;
        fillArray(tutorial, SimpleJSON.JSON.Parse(file.text)["texts"]);

        file = Resources.Load(json_ruleta_Url) as TextAsset;
        ruleta.Init( SimpleJSON.JSON.Parse(file.text)["texts"]);

        file = Resources.Load(json_moods_Url) as TextAsset;
        moods.Init(SimpleJSON.JSON.Parse(file.text)["texts"]);
    }
    public void LoadDataMinigames(string json_data)
    {
        var Json = SimpleJSON.JSON.Parse(json_data);
    }
    private void fillArray(List<string> arr, JSONNode content)
    {
        for (int a = 0; a < content.Count; a++)
        {
            string text = content[a]["text"];
            text = ReplaceUserName(text);
            arr.Add(text);
        }
    }
    public string GetRandomText(List<string> arr)
    {
        return arr[ UnityEngine.Random.Range(0,arr.Count) ];
    }
    public string ReplaceUserName(string field)
    {
        //if (Data.Instance.userData.username != "")
        //    return field.Replace("[username]", Data.Instance.userData.username);
        //else 
            return field;
    }
    public string GetFilteredText(List<string> arr, int id)
    {
        return ReplaceUserName(arr[id]);
    }

}
