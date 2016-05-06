using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using SimpleJSON;

[Serializable]
public class TextsRuleta {

    public List<Data> data;

    [Serializable]
    public class Data
    {
        public int zone;
        public int item;
        public string text;
    }

    public void Init(JSONNode content)
    {
        for (int a = 0; a < content.Count; a++)
        {
            Data dataNew = new Data();
            dataNew.zone = int.Parse(content[a]["zone"]);
            dataNew.item = int.Parse(content[a]["item"]);
            dataNew.text = content[a]["text"];
            data.Add(dataNew);
        }
    }
    public Data GetDataById(int id)
    {
        return data[id];
    }
}
