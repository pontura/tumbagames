using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SimpleJSON;

[Serializable]
public class TextsMoods {

    public List<Data> data;

    [Serializable]
    public class Data
    {
        public int id;
        public string title;
        public string text;
    }

    public void Init(JSONNode content)
    {
        for (int a = 0; a < content.Count; a++)
        {
            Data dataNew = new Data();
            dataNew.id = int.Parse(content[a]["id"]);
            dataNew.title = content[a]["title"];
            dataNew.text = content[a]["text"];
            data.Add(dataNew);
        }
    }
    public Data GetDataById(int id)
    {
        foreach (Data d in data)
        {
            if (d.id == id)
                return d;
        }
        return null;
    }
}
