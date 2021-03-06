using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsMultipleManager : MonoBehaviour {

	public enum dashTypes
	{
		GUY,
		NISMAN
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
	public int pungs;
	public int pungs_while_gil;
	public int dash_while_gil;
	public int dash_guy;
	public int dash_nisman;
	public int soreteQty;
	public int shootsQty;
	public int crash_while_moto;

    void Start () {
		Events.OnGameStart += OnGameStart;
        AchievementsEvents.OnDie += OnDie;
		AchievementsEvents.OnPung += OnPung;
		AchievementsEvents.OnPung_While_Gil += OnPung_While_Gil;
		AchievementsEvents.OnDash_While_Gil += OnDash_While_Gil;
		AchievementsEvents.OnCrash_While_Moto += OnCrash_While_Moto;
		AchievementsEvents.OnDash += OnDash;
		AchievementsEvents.OnShoot += OnShoot;
		AchievementsEvents.OnSpecialEnemyPung += OnSpecialEnemyPung;
		AchievementsEvents.OnPowerUp += OnPowerUp;
		AchievementsEvents.OnPowerupOff += OnPowerupOff;

		pungs = PlayerPrefs.GetInt("pungs");
		dead_by_escudo = PlayerPrefs.GetInt("dead_by_escudo");
		dead_by_rati_jump = PlayerPrefs.GetInt("dead_by_rati_jump");
		dead_by_guy = PlayerPrefs.GetInt("dead_by_transeunte");
    }
	void OnGameStart()
	{
		pungs_while_gil = 0;
		pungs = 0;
		dead_by_escudo = 0;
		dead_by_rati_jump = 0;
		dead_by_guy = 0;
		dash_guy = 0;
		dash_nisman = 0;
		soreteQty = 0;
		shootsQty = 0;
		pungs_while_gil = 0;
		dash_while_gil = 0;
		crash_while_moto = 0;
	}
	void OnPowerupOff(PowerupManager.types type)
	{
		if(type == PowerupManager.types.CHUMBO && shootsQty == 0)
			AchievementsEvents.OnZero_Shoot_While_Chumbo ();
	}
	void OnDash(Enemy enemy)
	{
		dashTypes type = dashTypes.GUY;
		if (enemy.GetComponent<Victim> ()) {
			type = dashTypes.GUY;
			dash_guy++;
			AchievementsEvents.OnNewDashComputed(type);
		}
	}
	void OnPowerUp(string powerUpName)
	{
		if (powerUpName == "SORETE") {
			soreteQty++;
			AchievementsEvents.OnNewMultiplePowerUpComputed ();
		}
	}
	void OnShoot()
	{
		//Debug.Log ("OnShoot ");
		shootsQty++;
		AchievementsEvents.OnNewMultiplePowerUpComputed ();
	}
	void OnPung()
	{
		//Debug.Log ("OnPung ");
		pungs++;
		AchievementsEvents.OnPungComputed(pungs);
	}
	void OnDash_While_Gil()
	{
		dash_while_gil++;
		AchievementsEvents.OnActionWhilePowerUPComputed(AchievementActionWhilePowerup.actions.DASH_WHILE_GIL, pungs_while_gil);
	}
	void OnPung_While_Gil()
	{
		pungs_while_gil++;
		AchievementsEvents.OnActionWhilePowerUPComputed (AchievementActionWhilePowerup.actions.STEAL_WHILE_GIL, pungs_while_gil);
	}
	void OnCrash_While_Moto()
	{
		crash_while_moto++;
		AchievementsEvents.OnActionWhilePowerUPComputed (AchievementActionWhilePowerup.actions.CRASH_WHILE_MOTO, crash_while_moto);
	}
	void OnSpecialEnemyPung(string name)
	{
		//Debug.Log ("OnSpecialEnemyPung " + name);
		dashTypes type = dashTypes.GUY;
		if (name == "Nisman") {
			dash_nisman++;
			type = dashTypes.NISMAN;
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
