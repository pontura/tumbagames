using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroJump : MonoBehaviour
{

    Hero hero;

    float duration = 0.25f;
    float _height = 4;

    public states state;

    public enum states
    {
        NONE,
        JUMPING_UP,
        JUMPING_DOWN
    }

    public void Init(Hero hero)
    {
        this.hero = hero;
        state = states.NONE;
    }
    public void Jump()
    {
        if (state != states.NONE)
            return;

        hero.anim.Play("jump");
        state = states.JUMPING_UP;
        hero.hitsManager.SetStateForReceiver(false);
        
        iTween.MoveTo(hero.asset, iTween.Hash(
               "y", _height,
               "time", duration,
               "easetype", iTween.EaseType.easeOutCirc,               
               "oncomplete", "IsOnTop",
               "islocal", true,
               "oncompletetarget", this.gameObject
            ));
        
    }
    void IsOnTop()
    {
        Invoke("ResetCollider", duration / 2);
        state = states.JUMPING_DOWN;
        iTween.MoveTo(hero.asset, iTween.Hash(
             "y", 0,
             "time", duration,
             "easetype", iTween.EaseType.easeInCirc,
             "oncomplete", "ResetJump",
             "islocal", true,
             "oncompletetarget", this.gameObject
          ));
    }
    void ResetCollider()
    {
        hero.hitsManager.SetStateForReceiver(true);
    }

    void ResetJump()
    {
        state = states.NONE;
        hero.asset.transform.localPosition = Vector3.zero;
        hero.Idle();
    }
}
