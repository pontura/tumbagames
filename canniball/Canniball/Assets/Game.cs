using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

    public float distance;
    public Character character;
    public float speed;
    public float TargetSpeed;
    private float desaceleration = 4f;
    private float MAX_SPEED = 6;
    public List<Scrolleable> scrolleables;
    public List<Obstacle> obstacles;

    public Transform container;
    public Enemy enemy;

    public float distanceToEnemy;
    public float distanceBetweenEnemies = 40;
    public float speedInFloor = 50;

    public states state;
    public enum states
    {
        PLAYING,
        DEAD,
        COMIDO,
        READY
    }
    bool gameOver;

	void Start () {
        scrolleables[2].gameObject.SetActive(false);
        distanceToEnemy = 80;
        Events.OnHeroDie += OnHeroDie;
        Events.OnHeroComido += OnHeroComido;
        Events.GameOver += GameOver;
        Events.OnLevelComplete += OnLevelComplete;
        Invoke("Init", 0.1f);
	}
    void Init()
    {
        if (gameOver) return;
        Utils.RemoveAllChildsIn(container);
        
        if (Data.Instance.GetComponent<LevelsData>().level == 2)
            scrolleables[2].gameObject.SetActive(true);

        foreach (Obstacle sc in obstacles)
            sc.Init();
        Events.OnMusicChange("win_rugby3");
        character.Restart();
        state = states.PLAYING;
    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
        Events.OnHeroComido -= OnHeroComido;
        Events.GameOver -= GameOver;
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void GameOver()
    {
        gameOver = true;
        Invoke("MainMenu", 5);
    }
    void MainMenu()
    {
        Data.Instance.LoadScene("Intro");
    }
    void OnHeroComido()
    {
        state = states.COMIDO;
    }
    void OnHeroDie()
    {
        state = states.DEAD;
        Invoke("Init", 4);
    }
    void Restart()
    {
        Events.Restart();
        gameOver = false;
        distance = 0;
        distanceToEnemy = 0;
        Init();
    }
    void OnLevelComplete()
    {
        Utils.RemoveAllChildsIn(container);
        state = states.READY;
        Invoke("Restart", 5);
    }
    public void Run()
    {
        if (state == states.DEAD) return;
        if (state == states.COMIDO) return;
        speed += 0.6f;
    }
    void Update()
    {
        if (state == states.READY)
        {
            if (speed > 0) speed -= Time.deltaTime;
            if (speed < 0) speed = 0;
        } else
        if (state == states.DEAD || state == states.COMIDO)
        {
            if (speed > 0) speed -= Time.deltaTime;
            if (speed < 0) speed = 0;
        }
        if (speed > 0) speed -= Time.deltaTime * desaceleration;
        if (speed < 0) speed = 0;
        else if (speed > MAX_SPEED) speed = MAX_SPEED;

        character.SetSpeed(speed);

        foreach (Scrolleable sc in scrolleables)
            sc.Move(speed);

        foreach (Obstacle sc in obstacles)
            sc.Move(speed / speedInFloor);

        distance += speed / 10;

        if (distance > distanceToEnemy)
        {
            distanceToEnemy += distanceBetweenEnemies;
            AddEnemies();
        }

    }
    void AddEnemies()
    {
        Enemy enemyNew = Instantiate(enemy);
        enemyNew.transform.SetParent(container);
        bool right = false;
        if(Random.Range(0,10)>5)
            right = true;
        enemyNew.Init(this, right);
    }

}
