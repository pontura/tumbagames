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
        public bool unlocked;
        public List<Seccional> seccional;
    }

    public void Init(JSONNode content)
    {
        for (int a = 0; a < content.Count; a++)
        {
            Data dataNew = new Data();
            dataNew.id = int.Parse(content[a]["id"]);
            dataNew.title = content[a]["title"];
            int seccionalID = 1;
            dataNew.seccional = new List<Seccional>();

            if (PlayerPrefs.GetInt("mood" + dataNew.id) > 0 || dataNew.id == 1)
                dataNew.unlocked = true;

            for (int b = 0; b < content[a]["seccionales"].Count; b++ )
            {
                Seccional seccional = new Seccional();
                seccional.id = seccionalID;
                seccional.title = content[a]["seccionales"][b]["title"];
                seccional.name = content[a]["seccionales"][b]["text"];
                seccional.price = int.Parse(content[a]["seccionales"][b]["price"]);
                string unlockPref = "mood_" + (int)(a + 1) + "_" + (int)(b + 1);
                if (PlayerPrefs.GetInt(unlockPref) > 0 || (dataNew.id == 1 && b == 0))
                    seccional.unlocked = true;

                seccionalID++;
                seccional.moodID = dataNew.id;
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
