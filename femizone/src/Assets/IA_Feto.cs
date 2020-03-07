using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Feto : IA
{
    private void OnEnable()
    {
        Events.OnCutsceneDone += OnCutsceneDone;
    }
    private void OnDisable()
    {
        Events.OnCutsceneDone -= OnCutsceneDone;
    }
    void OnCutsceneDone()
    {
        enemy.Idle();
        enemy.AnimateSpecificAction("intro_start");
    }
    public override void Idle()
    {
        CancelInvoke();
        enemy.Idle();
        state = states.IDLE;
    }
}
