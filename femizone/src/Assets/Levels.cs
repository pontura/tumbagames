using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour {

    public LevelsData levelsData;
	public int activeLevelID;

	void Start () {
        levelsData = GetComponent<LevelsData> ();
        levelsData.Init();

        AddNewLevel();
    }
	public void StageClear()
	{		
		activeLevelID++;
		AddNewLevel ();
		World.Instance.LevelClear ();
		Events.OnStageClear ();
	}
	void AddNewLevel()
	{
        levelsData.AddNewLevel (activeLevelID);
	}
}
