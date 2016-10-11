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

	void Start () {
        distanceToEnemy = 80;
	}
    public void Run()
    {
        speed += 0.6f;
    }
	void Update () {
        if (speed > 0) speed -= Time.deltaTime * desaceleration;
        if (speed < 0) speed = 0;
        else if (speed > MAX_SPEED) speed = MAX_SPEED;

        character.SetSpeed(speed);       

        foreach (Scrolleable sc in scrolleables)
            sc.Move(speed);

        foreach (Obstacle sc in obstacles)
            sc.Move(speed / speedInFloor);

        distance += speed/10;

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
