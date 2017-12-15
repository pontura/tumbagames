using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
	public states state;
	public enum states
	{
		FIGHTING,
		LEVEL_CLEAR
	}
	static World mInstance = null;
	public Camera camera;
	public EnemiesManager enemiesManager;
	public HeroesManager heroesManager;
	public WorldCamera worldCamera;
	public Levels levels;

	public static World Instance
	{
		get
		{
			return mInstance;
		}
	}

	void Awake()
	{
		if (!mInstance)
			mInstance = this;

		heroesManager = GetComponent<HeroesManager>();
		enemiesManager = GetComponent<EnemiesManager>();
		levels = GetComponent<Levels> ();
	}
	public int newXLimit;
	public void LevelClear()
	{
		state = states.LEVEL_CLEAR;
		newXLimit = (levels.activeLevelID * Data.Instance.settings.LevelsWidth);
	}
	void Update()
	{
		if (state == states.FIGHTING)
			return;
		
		if (state == states.LEVEL_CLEAR) {
			print(heroesManager.GetMostAdvancedPosition ());
			float pos = heroesManager.GetMostAdvancedPosition ();
			//print (pos);
			if (worldCamera.transform.position.x >= newXLimit) {
				pos = newXLimit;
				state = states.FIGHTING;
			} else if(pos>10){
				worldCamera.UpdatePosition (1);
			}
		}
	}
}
