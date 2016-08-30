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
        public List<Seccional> seccional;
    }

    public void Init(JSONNode content)
    {
        for (int a = 0; a < content.Count; a++)
        {
            Data dataNew = new Data();
            dataNew.id = int.Parse(content[a]["id"]);
            dataNew.title = content[a]["title"];
            int seccionalID = 0;
            dataNew.seccional = new List<Seccional>();
            for (int b = 0; b < content[a]["seccionales"].Count; b++ )
            {
                Seccional seccional = new Seccional();
                seccional.id = seccionalID;
                seccional.name = content[a]["seccionales"][b]["text"];
                seccional.price = int.Parse(content[a]["seccionales"][b]["price"]);

                if (PlayerPrefs.GetInt("mood" + a + b) > 0 || (a==0 && b==0) )
                    seccional.unlocked = true;

                seccionalID++;
                dataNew.seccional.Add(seccional);
            }
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
