using UnityEngine;
using System.Collections;

public class Runner : Enemy
{
    public states state;
    public float speed;

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
        anim = GetComponentInChildren<Animator>();
        anim.Play("idle", 0, 0);
    }
    override public void Enemy_Pooled()
    {
        state = states.IDLE;
    }
    override public void Enemy_Update(Vector3 pos)
    {
        pos.x -= speed * Time.deltaTime;
        transform.localPosition = pos;
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

        Pool();
    }

}
