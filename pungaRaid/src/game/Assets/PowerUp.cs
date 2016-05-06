using UnityEngine;
using System.Collections;

public class PowerUp : Enemy {

    public int lives;
    public GameObject moto;
    public GameObject chumbo;
    public GameObject gil;

    public PowerupManager.types type;

    override public void Enemy_Init(EnemySettings settings, int laneId)
    {

    }
    public void InitPowerUp()
    {
        int id = Random.Range(1, 4);
        
        moto.SetActive(false);
        chumbo.SetActive(false);
        gil.SetActive(false);
        
        switch (id)
        {
            case 1: moto.SetActive(true);   type = PowerupManager.types.MOTO;  break;
            case 2: chumbo.SetActive(true); type = PowerupManager.types.CHUMBO; break;
            case 3: gil.SetActive(true);    type = PowerupManager.types.GIL; break;
        }
    }

    public void Activate()
    {
        Events.OnPowerUp( type );
        Pool();
    }
}
