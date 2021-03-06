﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {
	public GameObject hipster;
	public GameObject warnesMan;
	public GameObject ceoMan;
	public GameObject opusMan;
    public GameObject cop;
	public GameObject cop_gun;
	public GameObject boss1;
    public GameObject scrumMaster;
    public GameObject moderno;
    public GameObject exhibicionista;
    public GameObject trapper;
    public GameObject klansman;
    public GameObject rugbier;
    public GameObject feto;

    public Enemy enemy_to_instantiate;
	public Transform container;
	public List<Enemy> all;
	public WorldCamera cam;

	void Start () {
		Events.OnCharacterDie += OnCharacterDie;
        Events.OnInitFight += OnInitFight;
        Events.KillEverybody += KillEverybody;
    }
	void OnDestroy () {
		Events.OnCharacterDie -= OnCharacterDie;
        Events.OnInitFight -= OnInitFight;
        Events.KillEverybody -= KillEverybody;
    }
    
    void OnCharacterDie(Character character)
	{
		Enemy enemy = character.GetComponent<Enemy> ();
		if (enemy == null)
			return;
		all.Remove (enemy);
		if (all.Count == 0) {
			World.Instance.levels.StageClear ();
		}
	}
	public Enemy InstantiateSceneOject(SceneObjectData data)
    {
        GameObject so = ceoMan;
        switch (data.type)
        {
            case SceneObjectData.types.WARNES_MAN:
                so = warnesMan;
                break;
			case SceneObjectData.types.HIPSTER:
				so = hipster;
				break;
            case SceneObjectData.types.CEO:
                so = ceoMan;
                break;
            case SceneObjectData.types.OPUS:
                so = opusMan;
                break;
            case SceneObjectData.types.COP:
                so = cop;
                break;
			case SceneObjectData.types.COP_GUN:
				so = cop_gun;
				break;
			case SceneObjectData.types.BOSS:
				so = boss1;
				break;
            case SceneObjectData.types.SCRUM_MASTER:
                so = scrumMaster;
                break;
            case SceneObjectData.types.MODERNO:
				so = moderno;
				break;
            case SceneObjectData.types.EXHIBICIONISTA:
                so = exhibicionista;
                break;
            case SceneObjectData.types.TRAPPER:
                so = trapper;
                break;
            case SceneObjectData.types.KKK:
                so = klansman;
                break;
            case SceneObjectData.types.RUGBIER:
                so = rugbier;
                break;
            case SceneObjectData.types.FETO:
                so = feto;
                break;
        }
        Enemy enemy = Instantiate(enemy_to_instantiate);
        enemy.transform.SetParent(container);
        enemy.Init(so);
        data.pos.y = 0;
        enemy.transform.localPosition = data.pos;
		all.Add (enemy);
		Events.OnMansPlaining (enemy, true);
		return enemy;
    }
    void OnInitFight()
    {
        CancelInvoke();
        Invoke("Loop", 2);
    }
    void Loop()
    {
        if (World.Instance.state != World.states.FIGHTING)
            return;
        RandomlyActivateEnemy();
        Invoke("Loop", 2);
    }
    void RandomlyActivateEnemy()
    {
        foreach(Enemy enemy in all)
        {
            if (enemy.state == Character.states.SLEEP && Random.Range(0,10)>7)
                enemy.ActivateToFight();

        }
    }
    void KillEverybody()
    {
        int i = all.Count;
           while(i>0)
            {
             all[i - 1].Idle();
               all[i - 1].OnReceiveHit(new HitArea(), 1000);
                i--;
            }
    }
}
