﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AreasManager : MonoBehaviour {

    public List<AreaSet> areaSets;

	void Start () {
        Events.OnLoadCurrentAreas += OnLoadCurrentAreas;
    }
    void OnLoadCurrentAreas()
    {
        areaSets.Clear();
        int moodID = Data.Instance.moodsManager.GetCurrentMoodID();
        int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional().id;

        GameObject[] thisAreaSets = Resources.LoadAll<GameObject>("areas/" + moodID + "_" + seccionalID);
        foreach (GameObject go in thisAreaSets)
        {
            AreaSet thisAreaSet = go.GetComponent<AreaSet>() as AreaSet;
            if (thisAreaSet) areaSets.Add(thisAreaSet);
        }
        foreach (GameObject go in Resources.LoadAll<GameObject>("areas/" + moodID))
        {
            AreaSet thisAreaSet = go.GetComponent<AreaSet>() as AreaSet;
            if (thisAreaSet) areaSets.Add(thisAreaSet);
        }
        foreach (GameObject go in Resources.LoadAll<GameObject>("areas/0"))
        {
            AreaSet thisAreaSet = go.GetComponent<AreaSet>() as AreaSet;
            if (thisAreaSet) areaSets.Add(thisAreaSet);
        }

        RandomizeAreaSetsByPriority();
	}
    public void RandomizeAreaSetsByPriority()
    {
        areaSets = Randomize(areaSets);
        areaSets = areaSets.OrderBy(x => x.competitionsPriority).ToList();
        areaSets.Reverse();
    }
    private List<AreaSet> Randomize(List<AreaSet> toRandom)
    {
        for (int i = 0; i < toRandom.Count; i++)
        {
            AreaSet temp = toRandom[i];
            int randomIndex = Random.Range(i, toRandom.Count);
            toRandom[i] = toRandom[randomIndex];
            toRandom[randomIndex] = temp;
        }
        return toRandom;
    }
}
