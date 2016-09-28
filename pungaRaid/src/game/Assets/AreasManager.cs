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
    public int activeAreaID;

    [Serializable]
    public class MoodAreas
    {
        public List<AreaSet> areaSets;
    }
	void Start () {
        Events.OnLoadCurrentAreas += OnLoadCurrentAreas;
    }
    public AreaSet GetActiveAreaSet()
    {
        if (areasToAdd == areasType.SECCIONAL_ID && activeAreaID == moodAreas[seccionalActiveID].areaSets.Count)
        {
            areasToAdd = areasType.GENERIC;
            activeAreaID = 0;
        }
        AreaSet areaSet = null;
        if (areasToAdd == areasType.GENERIC)
            areaSet = genericAreas[activeAreaID];
        else
            areaSet = GetCurrentArea().areaSets[activeAreaID];

        activeAreaID++;

        return areaSet;
    }
    public MoodAreas GetCurrentArea()
    {
        return moodAreas[seccionalActiveID];
    }
    void OnLoadCurrentAreas()
    {
        genericAreas.Clear();
        foreach (MoodAreas moodArea in moodAreas)
            moodArea.areaSets.Clear();

        int moodID = Data.Instance.moodsManager.GetCurrentMoodID();
        int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional().id;

        AddAreasToSeccional(moodID, seccionalID);
        AddOtherUnlockedAreas(moodID, seccionalID);
        AddGenericAreas(moodID);       

        RandomizeAreaSetsByPriority();
	}
    void AddAreasToSeccional(int moodID, int seccionalID)
    {
        MoodAreas moodArea = new MoodAreas();
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
