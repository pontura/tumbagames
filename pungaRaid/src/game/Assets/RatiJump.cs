using UnityEngine;
using System.Collections;

public class RatiJump : Enemy
{
    public Collider2D characterCollisionCheck;
    public states state;
    public SpriteRenderer heads;
    private bool moved;

    public enum states
    {
        IDLE,
        WALKING,
        STOLEN,
        CRASHED
    }

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        moved = false;
        anim.Play("idle");
        heads.sprite = Data.Instance.enemiesManager.GetRandomHead();
    }
    override public void OnCrashed()
    {
        anim.Play("attack");
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        state = states.CRASHED;
        anim.Play("crashed");
        GetComponent<BoxCollider2D>().enabled = false;
        Events.OnAddExplotion(laneId, (int)transform.localPosition.x);
    }
    override public void OnSecondaryCollision(Collider2D other)
    {
        if (moved) return; 
        moved = true;
        anim.Play("jump");
        if (other.tag == "Player")
        {
            int characterLane = Game.Instance.gameManager.characterManager.lanes.laneActiveID;

           // print("characterLane " + characterLane);
            if (this.laneId == characterLane) return;

            Lane newLane = Game.Instance.gameManager.characterManager.lanes.all[characterLane];
            Game.Instance.gameManager.characterManager.lanes.changeEnemyLane(this, newLane);

        }

    }
}
