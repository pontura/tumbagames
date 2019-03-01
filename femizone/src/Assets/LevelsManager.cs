using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsManager : MonoBehaviour {

    EnemiesManager enemiesManager;
	PowerupsManager powerupsManager;
	GenericObjectsManager genericObjectsManager;
	WeaponsManager weaponsManager;
	BackgroundsManager backgroundsManager;

	public List<LevelsByDificulty> levelsByDificulty;
	[Serializable]
	public class LevelsByDificulty
	{
		public List<Level> all;
		public int total;
	}

	Levels levels;
	public List<Level> all;
	int levelsWidth;
	Vector3 offset;

	void Start () {
		
		foreach (LevelsByDificulty l in levelsByDificulty) {
			Shuffle (l.all);
			int id = 0;
			foreach (Level level in l.all) {
				if(id<l.total)
					all.Add (level);
				id++;
			}
		}
		
		levels = GetComponent<Levels> ();
        enemiesManager = GetComponent<EnemiesManager>();
		backgroundsManager = GetComponent<BackgroundsManager> ();
		powerupsManager = GetComponent<PowerupsManager> ();
		weaponsManager = GetComponent<WeaponsManager> ();
		genericObjectsManager = GetComponent<GenericObjectsManager> ();
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

			if (data.type == SceneObjectData.types.WEAPON_1)
				weaponsManager.InstantiateSceneOject(data);
			else if (data.type == SceneObjectData.types.ENERGY)
				powerupsManager.InstantiateSceneOject(data);
			else if (
				data.type == SceneObjectData.types.FIRE
				|| data.type == SceneObjectData.types.FAROL
				|| data.type == SceneObjectData.types.ARBUSTO
				|| data.type == SceneObjectData.types.BANCO
			)
				genericObjectsManager.InstantiateSceneOject(data);
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

	void Shuffle(List<Level> all)
	{
		for(int a = 0; a<30; a++)
		{
			Level l1 = all [0];
			int num = UnityEngine.Random.Range (0, all.Count);
			Level l2 = all [num];
			all [0] = l2;
			all [num] = l1;
		}
	}
}
