using UnityEngine;
using System.Collections;

public class GenericVictim : Enemy
{
    public states state;
    public float speed;

    public GameObject[] obstacles;

    public enum states
    {
        IDLE,
        CRASHED
    }

    private string currentAnim;
    private BoxCollider2D collider2d;

    override public void Enemy_Activate()
    {
        collider2d = GetComponent<BoxCollider2D>();
        collider2d.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        foreach (GameObject goToInactive in obstacles)
            goToInactive.SetActive(false);

        GameObject go = GetRandomGameObjects();

        go.SetActive(true);
        anim = go.GetComponentInChildren<Animator>();

        anim.Play("idle", 0, 0);
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
    //override public void Enemy_Update(Vector3 pos)
    //{
    //    pos.x -= speed * Time.deltaTime;
    //    transform.localPosition = pos;
    //}
    override public void OnCrashed()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;
        anim.Play("pung");
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;

        Events.OnAddExplotion(laneId, (int)transform.localPosition.x);

        Pool();
    }
    public void Steal()
    {
        if (Game.Instance.gameManager.state == GameManager.states.ENDING) return;
        
        anim.Play("pung", 0, 0);

        Events.OnScoreUpdate((Random.Range(5, 10) * 10) );

        Events.OnAddCoins(laneId, transform.localPosition.x, 3);
    }

}
