using UnityEngine;
using System.Collections;

public class Resorte : Enemy
{

    public int money;
    public states state;

    public enum states
    {
        IDLE
    }
    private BoxCollider2D collider;

    override public void Enemy_Activate()
    {
        collider.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        collider = GetComponent<BoxCollider2D>();
        anim.Play("alcantaIdle", 0, 0);
    }
    public void Activate()
    {
        Events.OnSoundFX("Alcantarilla");
        anim.Play("alcantaJump", 0, 0);
        Invoke("SetPool", 1.5f);
    }
    public void SetPool()
    {
        Pool();
    }

}
