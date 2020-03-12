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
    public EnemiesXtrasData enmiesXtraData;
    int sec;

    void Start()
    {
        enemiesManager = GetComponent<EnemiesManager>();
        Events.OnStageClear += OnStageClear;
        Events.OnInitFight += OnInitFight;
        //  Invoke ("Loop", 5);
        Loop();
    }

    void OnDestroy()
    {
        Events.OnStageClear -= OnStageClear;
        Events.OnInitFight -= OnInitFight;
    }
    void OnInitFight()
    {
        canAdd = true;
        enmiesXtraData = World.Instance.levels.levelsData.level.GetComponent<EnemiesXtrasData>();
        if (enmiesXtraData == null)
            return;
        sec = 0;
    }
    void OnStageClear()
    {
        canAdd = false;
        //added = 0;
        //maxToAdd += stagesClear-1;
        stagesClear++;
    }
    void Loop()
    {
        Invoke("Loop", 1);
        CheckToAdd();
    }
    //void Loop()
    //{
    //	Invoke ("Loop", 5);
    //	CheckToAdd ();
    //}
    Enemy enemy;
    void CheckToAdd()
    {
        if (enmiesXtraData == null)
            return;
        if (//stagesClear < 2 || 
            !canAdd ||  
            GetComponent<HeroesManager>().all.Count == 0)
            return;

        sec++;

        //if(added >= maxToAdd)
        //{
        //	canAdd = false;
        //	return;
        //}    

       // print("EnemiexExtrasManager Add new");
       // SceneObjectData data = new SceneObjectData();
        //int rand = Random.Range(0, 10);
        //List<SceneObjectData.types> all = new List<SceneObjectData.types>();
        foreach(EnemiesXtrasData.Config c in enmiesXtraData.all)
        {
            if(c.delay == sec)
            {
                print(c.delay + "    sec: " + sec);
                SceneObjectData d = new SceneObjectData();
                d.type = c.type;
                AddEnemy(d, c.positionData);
            }
        }
        
    }
    void AddEnemy(SceneObjectData data, EnemiesXtrasData.Config.PositionData posData)
    {
        print("ADDDDDDD " + data.type + "   posData : " + posData + "   sec " + sec );
        //all.Add(SceneObjectData.types.WARNES_MAN);
        //all.Add(SceneObjectData.types.CEO);
        //all.Add(SceneObjectData.types.MODERNO);

        //if (stagesClear > 4)
        //    all.Add(SceneObjectData.types.COP);
        //if (stagesClear > 7)
        //    all.Add(SceneObjectData.types.COP_GUN);

        //data.type = GetRandomBetween(all);
		
		Vector3 pos = World.Instance.worldCamera.transform.position;

		if(posData == EnemiesXtrasData.Config.PositionData.LEFT)
			pos.x += 16 + Random.Range(0,4);
		else
			pos.x -= 16 + Random.Range(0,4);	
		
		pos.z = Random.Range(-1,10);
		data.pos = pos;
		enemy = enemiesManager.InstantiateSceneOject (data);
		enemy.Idle ();
        enemy.ActivateToFight();
       
	}
    SceneObjectData.types GetRandomBetween(List<SceneObjectData.types> all)
    {
        return all[Random.Range(0, all.Count)];
    }
}
