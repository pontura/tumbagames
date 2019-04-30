﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public states state;
    public enum states
    {
        FIGHTING,
        LEVEL_CLEAR,
        GAME_OVER
    }
    static World mInstance = null;
    public Camera camera;

    public EnemiesManager enemiesManager;
    public HeroesManager heroesManager;

    public WorldCamera worldCamera;
    public Levels levels;

    public static World Instance
    {
        get
        {
            return mInstance;
        }
    }

    void Awake()
    {
        if (!mInstance)
            mInstance = this;

        heroesManager = GetComponent<HeroesManager>();
        enemiesManager = GetComponent<EnemiesManager>();
        levels = GetComponent<Levels>();
	}
    public void OnRestart()
    {
        Data.Instance.LoadScene("Intro");
    }
    public int newXLimit;
    public void LevelClear()
    {
        state = states.LEVEL_CLEAR;
        newXLimit = (levels.activeLevelID * Data.Instance.settings.LevelsWidth);
    }
    float timeGameOver;
    public void GameOver()
    {
        timeGameOver = Time.time;
        if(timeGameOver == 1)
            this.timeGameOver = 0;

        state = states.GAME_OVER;
		UI.Instance.mainHiscores.GameOver();
        Events.GameOver();
        //SceneManager.LoadScene ("Summary");
    }
    void Update()
    {
        if (state == states.FIGHTING)
            return;

        if (state == states.LEVEL_CLEAR)
        {
            float percentPosition = heroesManager.GetPercentPosition();
            float globalPos = percentPosition - worldCamera.transform.position.x;

            float limit = Data.Instance.settings.limit_to_walk - 10;

            if (worldCamera.transform.position.x >= newXLimit)
            {
                percentPosition = newXLimit;
                state = states.FIGHTING;
            }
            else if (globalPos > limit)
            {
                worldCamera.UpdatePosition(1);
            }
        }
    }
}
