using UnityEngine;
using System.Collections;

public class Rati : Enemy {

    public states state;
    public SpriteRenderer heads;

    public enum states
    {
        IDLE,
        WALKING,
        STOLEN,
        CRASHED
    }

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        anim.Play("copShieldIdle");
        state = states.IDLE;
        GetComponent<BoxCollider2D>().enabled = true;
        heads.sprite = Data.Instance.enemiesManager.GetRandomHead();
    }

    public override void OnCrashed()
    {
        anim.Play("copShieldCollide");
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        Events.OnAddExplotion(laneId, (int)transform.localPosition.x);
        state = states.CRASHED;
       // anim.Play("crashed");
        GetComponent<BoxCollider2D>().enabled = false;
        Pool();
    }
}
