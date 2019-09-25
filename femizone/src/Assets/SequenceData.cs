using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SequenceData : MonoBehaviour
{
    public List<Item> all;

    [Serializable]
    public class Item
    {
        public string name;
        public int id;
        public int max;
    }
    public void AddIfNotExists(string name, int max)
    {
        foreach (Item sd in all)
        {
            if (sd.name == name)
                return;
        }
        Item item = new Item();
        item.name = name;
        item.max = max;
        all.Add(item);
    }
    public int GetIdFor(string name)
    {
        foreach (Item sd in all)
        {
            if (sd.name == name)
            {
                sd.id++;
                if (sd.id >= sd.max)
                    sd.id = 0;

                return sd.id;
            }            
        }
        return 0;
    }
}
