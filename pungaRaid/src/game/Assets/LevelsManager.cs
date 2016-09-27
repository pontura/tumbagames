﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LevelsManager : MonoBehaviour {

    //[Serializable]
    //public class Group
    //{
    //    public string name;
    //    public int distance;
    //    public Level[] levels;
    //}
    private float startingGroupDistance;
    public Lanes lanes;
    public Level StartingLevel;

   // public Group[] groups;

    public Level activeLevel;
    private int nextLevelDistance;
    private int offset = 40;
   
    public void Init()
    {
        //  activeLevel = StartingLevel;
        // nextLevelDistance = StartingLevel.length;
        // activeGroupId = -1;
       // Invoke("Continue", 0.2f);
        CheckForNewLevel(0);
    }
    public void Continue()
    {
        //  activeLevel = StartingLevel;
        // nextLevelDistance = StartingLevel.length;
        // activeGroupId = -1;
        
    }
    public void CheckForNewLevel(float distance)
    {
        distance += offset;    

        if (distance < nextLevelDistance) return;

        AreaSet areaSet = Data.Instance.areasManager.GetActiveAreaSet();

        if ((int)distance > (int)areaSet.distance)
        {
            startingGroupDistance += distance;
           // print("_ cambia grupo " + activeGroupId + " startingGroupDistance: " + startingGroupDistance + " distanc: " + distance);
        }

        int rand = UnityEngine.Random.Range(0, areaSet.levels.Length);
        activeLevel = areaSet.levels[rand];

       // print("nextLevelDistance " + nextLevelDistance + " distance " + distance + " activeGroupId: " + activeGroupId + "  GROUP: " + groups[activeGroupId].name + " activeLevel.length " + activeLevel.length + "  activeLevel.NAME " + activeLevel.name);
        LoadLevelAssets(nextLevelDistance);
        
        nextLevelDistance += activeLevel.length;
        
    }
    private void LoadLevelAssets(int nextLevelDistance)
    {
        //print("nextLevelDistance" + nextLevelDistance);
        //print("activeLevel" + activeLevel);
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
