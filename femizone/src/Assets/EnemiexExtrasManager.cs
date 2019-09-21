using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiexExtrasManager : MonoBehaviour
{
	public bool canAdd;
	EnemiesManager enemiesManager;
	int maxToAdd = 1;
	int added;
	int stagesClear;

    void Start()
    {
		enemiesManager = GetComponent<EnemiesManager> ();
		Events.OnStageClear += OnStageClear;
		Events.OnMansPlaining += OnMansPlaining;
		Invoke ("Loop", 5);
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
		added = 0;
		maxToAdd += stagesClear-1;
		stagesClear++;
	}

	void Loop()
	{
		Invoke ("Loop", 5);
		CheckToAdd ();
	}
	Enemy enemy;
	void CheckToAdd()
	{
		if (!canAdd ||  GetComponent<HeroesManager> ().all.Count == 0)
			return;
		
		if(added >= maxToAdd)
		{
			canAdd = false;
			return;
		}

		SceneObjectData data = new SceneObjectData ();
		int rand = Random.Range(0,10);

        List < SceneObjectData.types > all =  new List<SceneObjectData.types>();

        all.Add(SceneObjectData.types.WARNES_MAN);
        all.Add(SceneObjectData.types.CEO);
        all.Add(SceneObjectData.types.MODERNO);

        if (stagesClear > 4)
            all.Add(SceneObjectData.types.COP);
        if (stagesClear > 7)
            all.Add(SceneObjectData.types.COP_GUN);

        data.type = GetRandomBetween(all);
		
		Vector3 pos = World.Instance.worldCamera.transform.position;

		if(Random.Range(0,10)<5)
			pos.x += 15 + Random.Range(0,4);
		else
			pos.x -= 15 + Random.Range(0,4);	
		
		pos.z = Random.Range(-1,10);
		data.pos = pos;
		enemy = enemiesManager.InstantiateSceneOject (data);
		enemy.Idle ();

		added++;
		Invoke ("Delayed", 0.5f);
	}
	void Delayed()
	{
        if (enemy != null && World.Instance.state != World.states.GAME_OVER) {
			Events.OnMansPlaining (enemy, false);
			HitArea ha = GetComponent<HeroesManager> ().all [0].hitsManager.hitArea;
			enemy.ReceiveHit (ha, 1);
        }
	}
    SceneObjectData.types GetRandomBetween(List<SceneObjectData.types> all)
    {
        return all[Random.Range(0, all.Count)];
    }
}
