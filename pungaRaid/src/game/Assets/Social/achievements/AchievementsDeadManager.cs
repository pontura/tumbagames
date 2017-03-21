using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsDeadManager : MonoBehaviour {

    public enum deadTypes
    {
        RATI_ESCUDO,
        RATI_JUMP,
        TRANSEUNTE
    }
    public int dead_by_escudo;
    public int dead_by_rati_jump;
    public int dead_by_transeunte;

    void Start () {
        AchievementsEvents.OnDie += OnDie;

        dead_by_escudo = PlayerPrefs.GetInt("RATI_ESCUDO");
        dead_by_rati_jump = PlayerPrefs.GetInt("RATI_JUMP");
        dead_by_transeunte = PlayerPrefs.GetInt("TRANSEUNTE");
    }
    void OnDie(Enemy enemy)
    {
        deadTypes type = deadTypes.TRANSEUNTE;
        if (enemy.GetComponent<Rati>())
        {
            dead_by_escudo++;
            type = deadTypes.RATI_ESCUDO;
            PlayerPrefs.SetInt(type.ToString(), dead_by_escudo);
        }
        else if (enemy.GetComponent<RatiJump>())
        {
            dead_by_rati_jump++;
            type = deadTypes.RATI_JUMP;
            PlayerPrefs.SetInt(type.ToString(), dead_by_rati_jump);
        }
        else
        {
            dead_by_transeunte++;
            PlayerPrefs.SetInt(type.ToString(), dead_by_transeunte);
        }       

        AchievementsEvents.OnNewDeadComputed(type);
    }
    
}
