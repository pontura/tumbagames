using UnityEngine;
using System.Collections;

public class Obstacle : Enemy
{
    public states state;

    public enum states
    {
        IDLE,
        CRASHED
    }
    private int randObstacleID = -1;
    public GameObject[] obstaclesFreeLanes;
    public GameObject[] obstaclesLane4;
    private GameObject[] obstacles;

    private string currentAnim;
    private BoxCollider2D collider2d;

    override public void Enemy_Activate()
    {
        collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        foreach (GameObject goToInactive in obstaclesFreeLanes)
            goToInactive.SetActive(false);
        foreach (GameObject goToInactive in obstaclesLane4)
            goToInactive.SetActive(false);

        GameObject go = null;

        if (laneId == 4)
            go = obstaclesLane4[Random.Range(0, obstaclesLane4.Length)];  
        else
            go = obstaclesFreeLanes[Random.Range(0, obstaclesFreeLanes.Length)];


        go.SetActive(true);
        anim = go.GetComponent<Animator>();

        anim.Play("idle",0,0);
    }
    override public void Enemy_Pooled()
    {
        state = states.IDLE;
    }
    override public void OnCrashed()
    {
        if (state == states.CRASHED) return;
        anim.Play("hit");
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;

        Events.OnAddExplotion(laneId, (int)transform.localPosition.x);

        //anim.Play("crashed", 0, 0);
       // if (collider2d) collider2d.enabled = false;
        Pool();
        //Destroy(gameObject);
    }

}
