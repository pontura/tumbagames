using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

    EnemiesManager enemiesManager;
    public Level level1;

	void Start () {
        enemiesManager = GetComponent<EnemiesManager>();
        LoadNewLevel();
    }
	
	void LoadNewLevel () {
		foreach(SceneObjectData data in level1.GetComponentsInChildren<SceneObjectData>())
        {
            data.pos = data.transform.position;
            enemiesManager.InstantiateSceneOject(data);
        }
	}
}
