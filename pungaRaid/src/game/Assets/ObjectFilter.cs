using UnityEngine;
using System.Collections;
using System;

public class ObjectFilter : MonoBehaviour {

    [Serializable]
    public class Filter
    {
        public MoodsManager.moods mood;        
        public int seccionalID;
        public int forceToLaneID;
    }
    public Filter[] filters;


    public bool CanBeAdded(MoodsManager.moods mood, int landID)
    {
        if(filters.Length == 0)
            return true;

        int currentSeccionalID = Data.Instance.areasManager.seccionalActiveID;
        // Debug.Log(mood + "currentSeccionalID: " + currentSeccionalID);

        foreach (Filter f in filters)
        {
            if (f.forceToLaneID == 0 || f.forceToLaneID == landID)
            {
              //  Debug.Log(name + " este objeto se agrega: " + f.mood + "   de seccional: " + f.seccionalID);
                if (f.mood == mood && (f.seccionalID == 0 || f.seccionalID == currentSeccionalID))
                {
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
