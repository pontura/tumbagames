﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour {

    private float startingGroupDistance;
    public Lanes lanes;
    public Level StartingLevel;
    public AreaSet MiamiAreaSet;

    public Level activeLevel;
    private int nextLevelDistance;
    private int offset = 40;
    private AreaSet areaSet;
    public bool inMiamiMode;

    private float distanceToNextSeccional = 500;
   
    public void Init()
    {
        NewAreaSet();
        CheckForNewLevel(0);
        Events.OnPowerUp += OnPowerUp;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
    }
    void OnDestroy()
    {
        Events.OnPowerUp -= OnPowerUp;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
    }
    void OnHeroPowerUpOff()
    {
        inMiamiMode = false;
    }
    void OnPowerUp(PowerupManager.types type)
    {
        if (type == PowerupManager.types.RICKYFORT)
            inMiamiMode = true;
    }
    public void NewAreaSet()
    {
        areaSet = Data.Instance.areasManager.GetActiveAreaSet();
        startingGroupDistance += areaSet.distance;
    }
    public void NewSeccionalAreaSet()
    {
        areaSet = Data.Instance.areasManager.GetNextSeccionalAreaSet();
        distanceToNextSeccional += distanceToNextSeccional;
    }
    public void CheckForNewLevel(float distance)
    {
        distance += offset;
        
        if (distance < nextLevelDistance) return;

        if (inMiamiMode)
        {
            areaSet = MiamiAreaSet;
            startingGroupDistance = distance;
        } else
        if (distance > distanceToNextSeccional)
        {
            NewSeccionalAreaSet();
            startingGroupDistance = distance;
        }
        else if (distance > startingGroupDistance)
        {
            //  print("_____________ cambia _____________distance: " + distance + " startingGroupDistance " + startingGroupDistance);
            NewAreaSet();
        }

        activeLevel = areaSet.GetLevel();

     //   Debug.Log("seccionalActiveID: " + Data.Instance.areasManager.seccionalActiveID + " " +  startingGroupDistance + "   areaSet " + areaSet + "  distance " + distance + "  activeLevel: " + activeLevel.name + "    areaSet.distance : " + areaSet.distance + "    activeLevel.length " + activeLevel.length);
        LoadLevelAssets(nextLevelDistance);
        
        nextLevelDistance += activeLevel.length;
        
    }
    private void LoadLevelAssets(int nextLevelDistance)
    {
        Lanes laneData = activeLevel.GetComponent<Lanes>();
        lanes.AddBackground(laneData.vereda, nextLevelDistance, activeLevel.length);

        foreach (Lane lane in laneData.all)
        {
            Transform[] allObjectsInLane = lane.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in allObjectsInLane)
            {
                 EnemySettings settings = new EnemySettings();
                
                settings.speed = 0.05f;
                if(t.transform.localScale.x < 0)
                    settings.speed = -0.05f;

                lanes.AddObjectToLane(t.gameObject.name, lane.id, (int)(nextLevelDistance + t.transform.localPosition.x), settings);
            }
        }        
    }
	public void LoadNext () {
	    
	}
}
