using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public float distance;
    public int score;

    public states state;
    public enum states
    {
        INACTIVE,
        TUTORIAL,
        ACTIVE,        
        ENDING,
        ENDED
    }

    public float speed;
    public float realSpeed = 0;

    public GameObject container;
    public CharacterManager characterManager;
    private LevelsManager levelsManager;
    public MainCamera camera;
    public List<BackgroundScrolleable> backgroundsScrolleable;
    public ParticleSystem explotion;
    private CombosManager combosManager;

    private float SPEED_ACCELERATION = 0.008f;
    public float DEFAULT_SPEED = 0.09f;

    
    public void Init()
    {
        combosManager = Data.Instance.combosManager;

        Events.OnScoreAdd += OnScoreAdd;
        Events.OnHeroDie += OnHeroDie;
        Events.StartGame += StartGame;
        Events.OnExplotion += OnExplotion;
        Events.OnChangeSpeed += OnChangeSpeed;
        Events.OnResetSpeed += OnResetSpeed;

        characterManager = GetComponent<CharacterManager>();
        characterManager.Init();

        levelsManager = GetComponent<LevelsManager>();
        levelsManager.Init();

        GameObject mood = Instantiate(Data.Instance.moodsManager.GetCurrentMoodAsset());
        mood.transform.SetParent(container.transform);
        mood.transform.localPosition = Vector2.zero;

        foreach (BackgroundScrolleable bg in mood.GetComponentsInChildren<BackgroundScrolleable>())
            backgroundsScrolleable.Add(bg);

        Events.OnStartCountDown();

        Events.OnMusicChange("Gameplay");

        score = 0;

        camera.UpdatePosition(distance);
        characterManager.UpdatePosition(distance);

        if (PlayerPrefs.GetString("tutorialReady") != "true")
            DEFAULT_SPEED = 0.065f;

    }
    void OnDestroy()
    {
        Events.OnScoreAdd -= OnScoreAdd;
        Events.OnHeroDie -= OnHeroDie;
        Events.StartGame -= StartGame;
        Events.OnExplotion -= OnExplotion;
        Events.OnChangeSpeed -= OnChangeSpeed;
        Events.OnResetSpeed -= OnResetSpeed;

        combosManager = null;
    }
    public void OnScoreAdd(int _score)
    {
        score += _score * combosManager.comboID;
        Events.OnRefreshScore(score);
    }
    void StartGame()
    {
        speed = DEFAULT_SPEED;
        //state = states.ACTIVE;
        Invoke("goOn", 0.2f);
        EachSecond();
    }
    void OnHeroDie()
    {
        state = states.ENDING;
        Game.Instance.state = Game.states.ENDED;
        Events.OnSoundFX("warningPopUp");
        Events.OnMusicChange("gameOverTemp");              
    }
    public void RePlayTutorial()
    {
        Events.OnLoadingPanel();
        Events.OnPoolAllItemsInScene();
        Invoke("AllPooled", 2);
    }
    void AllPooled()
    {
        Application.LoadLevel("04_Game");
    }
    void OnChangeSpeed(float _speed, bool accelerating)
    {
        speed *= _speed;
        print(speed);
        if (!accelerating)
           realSpeed = speed;
    }
    void OnResetSpeed()
    {
        speed = DEFAULT_SPEED;
    }
    void goOn()
    {
        if(state != states.TUTORIAL)
            state = states.ACTIVE;
    }
    void EachSecond()
    {
        Invoke("EachSecond", 5);

        if (state == states.INACTIVE)
            return;

        DEFAULT_SPEED += (distance/1000)*SPEED_ACCELERATION;
    }
    void Update()
    {
        if (state == states.INACTIVE)
            return;

        if (Game.Instance.state == Game.states.ENDED)
        {
            realSpeed -= 0.001f;
            if (realSpeed <= 0)
                state = states.ENDED;
        }
        else
            realSpeed += 0.001f;

        if (realSpeed > speed)
            realSpeed = speed;
        else if (realSpeed < 0)
            realSpeed = 0;

        float _speed = (realSpeed*100)*Time.smoothDeltaTime;
        distance += _speed;

        if (state != states.ENDED)
        {
            foreach (BackgroundScrolleable bgScrolleable in backgroundsScrolleable)
                bgScrolleable.UpdatePosition(distance, _speed);

            camera.UpdatePosition(distance);
            characterManager.UpdatePosition(distance);

            if (state == states.ENDING)
                return;

            levelsManager.CheckForNewLevel(distance);
        }        
	}
    void OnExplotion()
    {
        Character character = characterManager.character;
        ParticleSystem ps = Instantiate(explotion) as ParticleSystem;
        ps.transform.SetParent(character.transform.parent.transform);
        ps.transform.localScale = Vector3.one;
        Vector3 newPos = character.transform.localPosition;
        newPos.y += 3;
        ps.transform.localPosition = newPos;
        ps.Play();
    }
}
