using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsMultipleManager : MonoBehaviour {

	public enum dashTypes
	{
		GUY
	}
    public enum deadTypes
    {
        RATI_ESCUDO,
        RATI_JUMP,
		GUY
    }
    public int dead_by_escudo;
    public int dead_by_rati_jump;
	public int dead_by_guy;

	public int dash_guy;
	public int dash_nisman;

    void Start () {
        AchievementsEvents.OnDie += OnDie;
		AchievementsEvents.OnDash += OnDash;

		dead_by_escudo = PlayerPrefs.GetInt("dead_by_escudo");
		dead_by_rati_jump = PlayerPrefs.GetInt("dead_by_rati_jump");
		dead_by_guy = PlayerPrefs.GetInt("dead_by_transeunte");
    }
	void OnDash(Enemy enemy)
	{
		dashTypes type = dashTypes.GUY;
		if (enemy.GetComponent<Victim> ()) {
			type = dashTypes.GUY;
			dash_guy++;
		}
		AchievementsEvents.OnNewDashComputed(type);
	}
    void OnDie(Enemy enemy)
    {
		deadTypes type = deadTypes.GUY;
        if (enemy.GetComponent<Rati>())
        {
            dead_by_escudo++;
            type = deadTypes.RATI_ESCUDO;
			SaveData ("dead_by_escudo", dead_by_escudo);
        }
        else if (enemy.GetComponent<RatiJump>())
        {
            dead_by_rati_jump++;
            type = deadTypes.RATI_JUMP;
			SaveData ("dead_by_rati_jump", dead_by_rati_jump);
        }
        else
        {
			dead_by_guy++;
			SaveData ("dead_by_transeunte", dead_by_guy);
        }       

        AchievementsEvents.OnNewDeadComputed(type);
    }
	void SaveData(string name, int data)
	{
		//PlayerPrefs.SetInt(name, data);
	}
    
}
