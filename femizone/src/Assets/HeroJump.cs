using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeroJump : MonoBehaviour
{
    Hero hero;

    float duration = 0.25f;
    float _height = 2;

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
        if (hero.state == Character.states.DEAD)
            return;
        if (state != states.NONE)
            return;
        Events.OnJump(hero);
        hero.anim.Play("jump");
        state = states.JUMPING_UP;
        hero.hitsManager.SetStateForReceiver(false);
        hero.asset.transform.DOMoveY(_height, duration).OnComplete(IsOnTop).SetEase(Ease.OutCirc);        
    }
    void IsOnTop()
    {
        Invoke("ResetCollider", duration / 2);
        state = states.JUMPING_DOWN;
        hero.asset.transform.DOMoveY(-_height, duration).OnComplete(ResetJump).SetEase(Ease.InCirc);
    }
    void ResetCollider()
    {
        hero.hitsManager.SetStateForReceiver(true);
    }

    void ResetJump()
    {
        if (hero.state == Character.states.DEAD)
            return;
        state = states.NONE;
        hero.asset.transform.localPosition = Vector3.zero;
        hero.Idle();
    }
}
