using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
	static World mInstance = null;
	public Camera camera;
	public EnemiesManager enemiesManager;
	public HeroesManager heroesManager;

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
	}
	public List<int> arr;
	void Start()
	{
		Random.InitState (2);
		for (int a = 0; a < 100; a++) {
			arr.Add(a);
		}
		for(int n=0; n<1000; n++)
		{
			int firstNum = arr [0];
			int randomNum = arr [Random.Range(1,arr.Count-1)];
			arr [0] = arr[randomNum];
			arr [randomNum] = firstNum;
		}
	}
}
