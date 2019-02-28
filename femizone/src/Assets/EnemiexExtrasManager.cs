using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiexExtrasManager : MonoBehaviour
{
	public bool canAdd;
	EnemiesManager enemiesManager;
	int maxToAdd = 2;
	int added;
    void Start()
    {
		enemiesManager = GetComponent<EnemiesManager> ();
		Events.OnStageClear += OnStageClear;
		Events.OnMansPlaining += OnMansPlaining;
		Loop ();
    }

    void OnDestroy()
    {
		Events.OnStageClear -= OnStageClear;
		Events.OnMansPlaining -= OnMansPlaining;
    }
	void OnMansPlaining(Character c, bool isOn)
	{
		canAdd = true;
	}
	void OnStageClear()
	{
		canAdd = false;
	}

	void Loop()
	{
		Invoke ("Loop", 1);
		CheckToAdd ();
	}
	Enemy enemy;
	void CheckToAdd()
	{
		if (!canAdd)
			return;
		if (enemy != null) {
			Events.OnMansPlaining (enemy, false);
			HitArea ha = GetComponent<HeroesManager> ().all [0].hitsManager.hitArea;
			enemy.ReceiveHit (ha, 1);
		}
		if(added >= maxToAdd)
		{
			canAdd = false;
			return;
		}

		SceneObjectData data = new SceneObjectData ();
		data.type = SceneObjectData.types.WARNES_MAN;
		Vector3 pos = enemiesManager.all [0].transform.position;
		pos.x += 8;
		pos.z += Random.Range(0,7);
		data.pos = pos;
		enemy = enemiesManager.InstantiateSceneOject (data);
		enemy.Idle ();

		added++;

	}
}
