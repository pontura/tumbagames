using UnityEngine;
using System.Collections;

public class Vereda : Enemy {

    public bool blockLane4;
    public bool blockLane3;
    public bool blockLane2;
    public bool blockLane1;
    public bool blockLane0;

    void Start()
    {
        offsetToBeOff = 30;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId) 
    {
       SpriteRenderer[] sps = GetComponentsInChildren<SpriteRenderer>();
       foreach (SpriteRenderer sp in sps)
       {
          // sp.sortingOrder = -1;
          // sp.sortingLayerName = "Default"; 
       }
    }
}
