using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActivator : MonoBehaviour
{
    public CutsceneInGame.types type;
    bool done;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
            return;
        
        Hero hero = World.Instance.heroesManager.GetClosestHero(transform);
        if (hero == null) return;

        if (hero.transform.position.x >= transform.parent.transform.position.x - 12)
        {
            done = true;
            Events.OnCutscene(type);
        }
    }
}
