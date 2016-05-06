using UnityEngine;
using System.Collections;

public class CoinsParticles : Enemy {

    public ParticleSystem particles;
    private bool isOn;

    public void InitParticles()
    {
        isOn = true;
        Invoke("ForcePool", 0.5f);
    }
    void ForcePool()
    {
        if (isOn)
        Pool();
    }
    public override void Enemy_Pooled() 
    {
        isOn = false;
        particles.Stop();
    }

}
