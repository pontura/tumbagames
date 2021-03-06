﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsData : MonoBehaviour
{
    EnemiesManager enemiesManager;
    PowerupsManager powerupsManager;
    GenericObjectsManager genericObjectsManager;
    WeaponsManager weaponsManager;
    BackgroundsManager backgroundsManager;

    LevelsManager levelsManager;

    Levels levels;
    public List<Level> all;
    int levelsWidth;
    Vector3 offset;

    public void Init()
    {
        levelsManager = Data.Instance.levelsManager;
        int a = 0;
        foreach (LevelsManager.LevelsByDificulty l in levelsManager.All)
        {
            Shuffle(l.all);
            int id = 1;
            foreach (Level level in l.all)
            {
                if (id <= l.total)
                {
                    all.Add(level);
                  //  print(a + " id: " + id + "      total: " + l.total + "        Count: " + l.all.Count + "   name: " + level.name);
                }
                id++;
            }
            a++;
        }
        levels = GetComponent<Levels>();
        enemiesManager = GetComponent<EnemiesManager>();
        backgroundsManager = GetComponent<BackgroundsManager>();
        powerupsManager = GetComponent<PowerupsManager>();
        weaponsManager = GetComponent<WeaponsManager>();
        genericObjectsManager = GetComponent<GenericObjectsManager>();
        levelsWidth = Data.Instance.settings.LevelsWidth;
    }
    public void AddNewLevel(int id)
    {
        offset = new Vector3(id * levelsWidth, 0, 0);
        Load(all[id]);
    }
    public Level level;
    void Load(Level level)
    {
        this.level = level;
        
        SceneObjectData[] allSceneObjectData = level.GetComponentsInChildren<SceneObjectData>();
        foreach (SceneObjectData data in allSceneObjectData)
        {
            data.pos = data.transform.position + offset;

            if (data.type == SceneObjectData.types.WEAPON || data.type == SceneObjectData.types.CINTURONGA)
                weaponsManager.InstantiateSceneOject(data);
            else if (data.type == SceneObjectData.types.ENERGY)
                powerupsManager.InstantiateSceneOject(data);
            else if (
                data.type == SceneObjectData.types.FIRE
                || data.type == SceneObjectData.types.FAROL
                || data.type == SceneObjectData.types.ARBUSTO
                || data.type == SceneObjectData.types.BANCO
                || data.type == SceneObjectData.types.CAR
                 || data.type == SceneObjectData.types.ARBOL
                 || data.type == SceneObjectData.types.ESVASTICA
                  || data.type == SceneObjectData.types.TACHO
            )
                genericObjectsManager.InstantiateSceneOject(data);
            else
                enemiesManager.InstantiateSceneOject(data);
        }

        backgroundsManager.AddBackground(level.background, offset);

        if (enemiesManager.all.Count == 0)
            World.Instance.levels.StageClear();

    }
    void StageClearDelay()
    {
        World.Instance.levels.StageClear();
    }

    void Shuffle(List<Level> all)
    {
        for (int a = 0; a < 30; a++)
        {
            Level l1 = all[0];
            int num = UnityEngine.Random.Range(0, all.Count);
            Level l2 = all[num];
            all[0] = l2;
            all[num] = l1;
        }
    }
}
