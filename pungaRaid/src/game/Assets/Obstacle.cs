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
    public GameObject[] obstacles;

    private string currentAnim;
    private BoxCollider2D collider2d;

    private int laneId;

    override public void Enemy_Activate()
    {
        collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        this.laneId = laneId;

        foreach (GameObject goToInactive in obstacles)
            goToInactive.SetActive(false);

        GameObject go = GetRandomGameObjects();

        go.SetActive(true);
        anim = go.GetComponent<Animator>();

        anim.Play("idle",0,0);
    }
    private GameObject GetRandomGameObjects()
    {
        foreach (GameObject go in obstacles)
            if (go.GetComponent<ObjectFilter>().CanBeAdded(Data.Instance.moodsManager.currentMood, laneId))
                return go;
        return obstacles[Random.Range(0, obstacles.Length)];
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
