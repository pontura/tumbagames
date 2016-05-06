using UnityEngine;
using System.Collections;

public class Coins : Enemy {

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
    }
    public void Activate()
    {
        Pool();
    }   
    
}
