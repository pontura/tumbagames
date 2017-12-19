using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {

    EnemiesManager enemiesManager;
	PowerupsManager powerupsManager;

	BackgroundsManager backgroundsManager;
	Levels levels;
    public Level[] all;
	int levelsWidth;
	Vector3 offset;

	void Start () {
		levels = GetComponent<Levels> ();
        enemiesManager = GetComponent<EnemiesManager>();
		backgroundsManager = GetComponent<BackgroundsManager> ();
		powerupsManager = GetComponent<PowerupsManager> ();
		levelsWidth = Data.Instance.settings.LevelsWidth;
    }
	public void AddNewLevel (int id) {
		offset = new Vector3 (id * levelsWidth, 0, 0);
		Load(all[id]);
	}
	void Load (Level level) {

		SceneObjectData[] allSceneObjectData = level.GetComponentsInChildren<SceneObjectData> ();
		foreach(SceneObjectData data in allSceneObjectData)
        {
			data.pos = data.transform.position + offset;

			if (data.type == SceneObjectData.types.ENERGY)
				powerupsManager.InstantiateSceneOject(data);
			else
				enemiesManager.InstantiateSceneOject(data);
        }

		backgroundsManager.AddBackground (level.background, offset);

		if (enemiesManager.all.Count == 0)
			World.Instance.levels.StageClear ();

	}
	void StageClearDelay()
	{
		World.Instance.levels.StageClear ();
	}
}
