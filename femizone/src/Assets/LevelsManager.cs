using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

    EnemiesManager enemiesManager;
	BackgroundsManager backgroundsManager;
	Levels levels;
    public Level[] all;
	int levelsWidth;
	Vector3 offset;

	void Start () {
		levels = GetComponent<Levels> ();
        enemiesManager = GetComponent<EnemiesManager>();
		backgroundsManager = GetComponent<BackgroundsManager> ();
		levelsWidth = Data.Instance.settings.LevelsWidth;
    }
	public void AddNewLevel (int id) {
		offset = new Vector3 (id * levelsWidth, 0, 0);
		Load(all[id]);
	}
	void Load (Level level) {
		
		foreach(SceneObjectData data in level.GetComponentsInChildren<SceneObjectData>())
        {
			data.pos = data.transform.position + offset;
            enemiesManager.InstantiateSceneOject(data);
        }
		backgroundsManager.AddBackground (level.background, offset);

	}
}
