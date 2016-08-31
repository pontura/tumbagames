using UnityEngine;
using System.Collections;
using System;

public class ObjectFilter : MonoBehaviour {

    [Serializable]
    public class Filter
    {
        public MoodsManager.moods mood;
        public int forceToLaneID;
    }
    public Filter[] filters;

    public bool CanBeAdded(MoodsManager.moods mood, int landID)
    {
        if(filters.Length == 0)
            return true;

       // print(mood);

        foreach (Filter f in filters)
        {
            if (f.forceToLaneID == 0 || f.forceToLaneID == landID)
            {
                if (f.mood == mood)
                {
                   // print("este objeti: " + f.mood);
                    return true;
                }
            }
        }
        foreach (Filter f in filters)
        {
            if (f.mood == MoodsManager.moods.GENERIC)
                return true;
        }
        return false;
    }

}
