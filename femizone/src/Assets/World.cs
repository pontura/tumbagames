using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{
    public states state;
    public enum states
    {
        FIGHTING,
        LEVEL_CLEAR,
        AWAITING,
        GAME_OVER
    }
    static World mInstance = null;
    public Camera camera;

    public EnemiesManager enemiesManager;
    public HeroesManager heroesManager;

    public WorldCamera worldCamera;
    public Levels levels;
    public ObjectsManager objectsManager;

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
    private void Start()
    {
        Events.OnReceiveit += OnReceiveit;
    }
    private void OnDestroy()
    {
        Events.OnReceiveit -= OnReceiveit;
    }
    void OnReceiveit(CharacterHitsManager.types t, Character ch)
    {

        if (state != states.FIGHTING && (worldCamera.transform.position.x >= newXLimit-10))
        {
            print("OnInitFight");
            Events.OnInitFight();
            state = states.FIGHTING;
        }
    }
    public void OnRestart()
    {
        Data.Instance.LoadScene("Intro");
    }
    public int newXLimit;
    float levelStartedTime;
    public void LevelClear()
    {
        levelStartedTime = Time.time;
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
        if (state == states.FIGHTING || state == states.AWAITING)
            return;

        if (state == states.LEVEL_CLEAR)
        {
            float percentPosition = heroesManager.GetPercentPosition();
            float globalPos = percentPosition - worldCamera.transform.position.x;

            float limit = Data.Instance.settings.limit_to_walk - 10;

            if (worldCamera.transform.position.x >= newXLimit)
            {
                percentPosition = newXLimit;
                state = states.AWAITING;
            }
            else if (globalPos > limit)
            {
                worldCamera.UpdatePosition(1);
            }
        }
    }

}
