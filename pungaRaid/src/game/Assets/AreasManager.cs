using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class AreasManager : MonoBehaviour {

    public areasType areasToAdd;
    public enum areasType
    {
        SECCIONAL_ID,
        GENERIC
    }
    public List<MoodAreas> moodAreas;
    public List<AreaSet> genericAreas;
    public List<AreaSet> activeAreaSet;

    public int seccionalActiveID;

    /// del area de seccionales avctivas:
    public int seccionalActiveIDArray;

    public int activeAreaID;

    [Serializable]
    public class MoodAreas
    {
        public List<AreaSet> areaSets;
        public int seccionalID;
    }
	void Start () {
        Events.OnLoadCurrentAreas += OnLoadCurrentAreas;
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.OnLoadCurrentAreas -= OnLoadCurrentAreas;
        Events.OnHeroDie -= OnHeroDie;
    }
    void OnHeroDie()
    {
        seccionalActiveID = Data.Instance.moodsManager.currentSeccionalID;
        seccionalActiveIDArray = 0;
        areasToAdd = areasType.SECCIONAL_ID;
        activeAreaID = 0;
    }
    public AreaSet GetNextSeccionalAreaSet()
    {
        if (moodAreas.Count > seccionalActiveIDArray + 1)
        {
            activeAreaID = 0;
            seccionalActiveIDArray++;
            seccionalActiveID = GetCurrentArea().seccionalID;
            //print("________________ moodAreas.Count : " + moodAreas.Count + " seccionalActiveIDArray:  " + seccionalActiveIDArray + "   GetNextSeccionalAreaSet " + seccionalActiveIDArray);
            areaSet = GetCurrentArea().areaSets[activeAreaID];
            areasToAdd = areasType.SECCIONAL_ID;
        }
        //else
        //    return GetActiveAreaSet();

      //  AreaSet areaSet = GetCurrentArea().areaSets[activeAreaID];        
        return areaSet;
    }
    AreaSet areaSet;
    public AreaSet GetActiveAreaSet()
    {
        if (areasToAdd == areasType.SECCIONAL_ID && activeAreaID == moodAreas[seccionalActiveIDArray].areaSets.Count)
        {
          //  print("GO TO GENERICS " + activeAreaID + " seccionalActiveIDArray: " + seccionalActiveIDArray);
            areasToAdd = areasType.GENERIC;
            activeAreaID = 0;
        }
        if (areasToAdd == areasType.GENERIC)
            areaSet = genericAreas[activeAreaID];
        else
            areaSet = GetCurrentArea().areaSets[activeAreaID];

        //print(areaSet + " areaSet name: " + areaSet.name + "   - " + areasToAdd + "   activeAreaID: " + activeAreaID + " de (Count): " + GetCurrentArea().areaSets.Count);
        activeAreaID++;

        return areaSet;
    }
    public MoodAreas GetCurrentArea()
    {
        return moodAreas[seccionalActiveIDArray];
    }
    void OnLoadCurrentAreas()
    {
        genericAreas.Clear();
        foreach (MoodAreas moodArea in moodAreas)
            moodArea.areaSets.Clear();

        moodAreas.Clear();

        int moodID = Data.Instance.moodsManager.GetCurrentMoodID();
        seccionalActiveID = Data.Instance.moodsManager.GetCurrentSeccional().id;

        AddAreasToSeccional(moodID, seccionalActiveID);
        AddOtherUnlockedAreas(moodID, seccionalActiveID);
        AddGenericAreas(moodID);       

        RandomizeAreaSetsByPriority();
	}
    void AddAreasToSeccional(int moodID, int seccionalID)
    {
        
        MoodAreas moodArea = new MoodAreas();
        moodArea.seccionalID = seccionalID;
        moodArea.areaSets = new List<AreaSet>();

        GameObject[] thisAreaSets = Resources.LoadAll<GameObject>("areas/" + moodID + "_" + seccionalID);
        foreach (GameObject go in thisAreaSets)
        {
            AreaSet thisAreaSet = go.GetComponent<AreaSet>() as AreaSet;
            if (thisAreaSet) moodArea.areaSets.Add(thisAreaSet);
        }
        moodAreas.Add(moodArea);
    }
    void AddOtherUnlockedAreas(int moodID, int seccionalID)
    {
        MoodAreas moodArea = new MoodAreas();
        moodArea.areaSets = new List<AreaSet>();

        foreach (Seccional seccional in Data.Instance.moodsManager.data.data[moodID - 1].seccional )
            if (seccional.id != seccionalID &&  seccional.unlocked)
                AddAreasToSeccional(moodID, seccional.id);
    }
    void AddGenericAreas(int moodID)
    {
        foreach (GameObject go in Resources.LoadAll<GameObject>("areas/0_genericas"))
        {
            AreaSet thisAreaSet = go.GetComponent<AreaSet>() as AreaSet;
            if (thisAreaSet) genericAreas.Add(thisAreaSet);
        }
    }
    public void RandomizeAreaSetsByPriority()
    {
        foreach(MoodAreas moodArea in moodAreas)
        {
            moodArea.areaSets = Randomize(moodArea.areaSets);
            moodArea.areaSets = moodArea.areaSets.OrderBy(x => x.competitionsPriority).ToList();
            moodArea.areaSets.Reverse();
        }
        genericAreas = genericAreas.OrderBy(x => x.competitionsPriority).ToList();
        genericAreas.Reverse();
    }
    private List<AreaSet> Randomize(List<AreaSet> toRandom)
    {
        for (int i = 0; i < toRandom.Count; i++)
        {
            AreaSet temp = toRandom[i];
            int randomIndex = UnityEngine.Random.Range(i, toRandom.Count);
            toRandom[i] = toRandom[randomIndex];
            toRandom[randomIndex] = temp;
        }
        return toRandom;
    }
}
