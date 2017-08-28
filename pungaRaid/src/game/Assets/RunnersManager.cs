using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnersManager : MonoBehaviour {

	private LevelsManager levelsManager;
	private Character character;
	void Start () {
		Events.OnAddRunner += OnAddRunner;
		levelsManager = GetComponent<LevelsManager> ();
		character = Game.Instance.characterManager.character;
	}
	void OnDestroy () {
		Events.OnAddRunner -= OnAddRunner;
	}
	void OnAddRunner()
	{
		print ("OnAddRunner");
		EnemySettings settings = new EnemySettings();
		settings.speed = -7f;		
		int laneID = Game.Instance.gameManager.characterManager.lanes.laneActiveID;
		int distance = (int)(character.transform.localPosition.x + 10);
		levelsManager.lanes.AddObjectToLane ("Skate", laneID,distance , settings);
	}

}
