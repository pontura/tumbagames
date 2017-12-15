using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour {

    LevelsManager levelsManager;
	public int activeLevelID;

	void Start () {
		levelsManager = GetComponent<LevelsManager> ();
		AddNewLevel();
    }
	public void StageClear()
	{		
		activeLevelID++;
		AddNewLevel ();
		World.Instance.LevelClear ();
	}
	void AddNewLevel()
	{
		levelsManager.AddNewLevel (activeLevelID);
	}
}
