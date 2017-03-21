using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericVictim : Enemy
{
    public states state;
    public float speed;

    public ObjectFilter[] obstacles;

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

        List<GameObject> newArray = new List<GameObject>();
        MoodsManager.moods mood = Data.Instance.moodsManager.currentMood;


        foreach (ObjectFilter of in obstacles)
        {
            if (of.CanBeAdded(mood, 0))
                newArray.Add(of.gameObject);
            else
                of.gameObject.SetActive(false);
        }

        if (newArray.Count > 0)
        {
            int rand = Random.Range(0, newArray.Count);
            int id = 0;
            foreach (GameObject go in newArray)
            {
                if (id == rand)
                {
                    anim = go.GetComponentInChildren<Animator>();
                    anim.Play("idle", 0, 0);
                    go.SetActive(true);
                }
                else
                    go.SetActive(false);
                id++;
            }
        }
        

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

		AchievementsEvents.OnSpecialEnemyPung (anim.gameObject.name);

        Events.OnAddExplotion(laneId, (int)transform.localPosition.x);

        Pool();
    }
    public void Steal()
    {
        if (Game.Instance.gameManager.state == GameManager.states.ENDING) return;
        
		AchievementsEvents.OnSpecialEnemyPung (anim.gameObject.name);

        anim.Play("pung", 0, 0);

        Events.OnScoreUpdate((Random.Range(5, 10) * 10) );

        Events.OnAddCoins(laneId, transform.localPosition.x, 3);
    }

}
