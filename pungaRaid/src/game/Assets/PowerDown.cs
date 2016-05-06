using UnityEngine;
using System.Collections;

public class PowerDown : Enemy
{
    public GameObject sorete;

    public PowerdownManager.types type;

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {

    }
    public void InitPowerDown()
    {
     //   int id = Random.Range(1, 1);

        int id = 1;

        sorete.SetActive(false);

        switch (id)
        {
            case 1: sorete.SetActive(true); type = PowerdownManager.types.SORETE; break;
        }
    }

    public void Activate()
    {
        Events.OnPowerDown(type);
        sorete.GetComponent<Animator>().Play("shitSplat",0,0);
        Invoke("PoolIt", 0.6f);
    }
    void PoolIt()
    {
        sorete.GetComponent<Animator>().Play("shitIdle", 0, 0);
        Pool();
    }
}
