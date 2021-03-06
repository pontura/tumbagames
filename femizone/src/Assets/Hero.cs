﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public int id;
    private InputManager inputManager;
    public WeaponPickable weaponPickable;
    public HeroWeapons weapons;
    Rigidbody rb;
    int offsetMoveX = 10;
    public HeroMove move;
    public HeroJump jump;
    HeroPowerManager heroPowerManager;

    public override void OnStart()
    {
        Events.OnCutscene += OnCutscene;

        move = GetComponent<HeroMove>();
        jump = GetComponent<HeroJump>();
        inputManager = GetComponent<InputManager>();
        weapons = GetComponent<HeroWeapons>();
        rb = GetComponent<Rigidbody>();
        heroPowerManager = GetComponent<HeroPowerManager>();

        move.Init(this);
        jump.Init(this);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        Events.OnCutscene -= OnCutscene;
    }    
    void OnCutscene(CutsceneInGame.types type)
    {
        state = states.FREEZED;
        anim.Play("run");
        move.ForceScreenCenter();
    }
    
    public void OnUpdateByInput(int HorizontalDirection, int VerticalDirection)
    {
         if (state == states.STRESS)
            return;
        if (state == states.DEAD || state == states.HITTING || state == states.HITTED)
            return;
        if ((HorizontalDirection != 0 || VerticalDirection != 0))
        {
            move.ChekToMove(HorizontalDirection, VerticalDirection);
            if (state != states.WALK)
                Walk();
        }
        else if (state == states.WALK)
            Idle();

        rb.velocity = Vector3.zero;
    }
    public override void Idle()
    {
        if (state == states.STRESS)
            return;
        if (state == states.DEAD)
            return;
       state = states.IDLE;
        if (jump.state != HeroJump.states.NONE)
            print("jumping");
        else if (weapons.type == WeaponPickable.types.WEAPON1)
            anim.Play("idle_gun");
        else if (weapons.type == WeaponPickable.types.WEAPON2)
            anim.Play("melee_idle");
        else
            anim.Play("idle");
        OnIdle();
        move.OnIdle();
    }
    public override void Walk()
    {
       
        if (state == states.DEAD || state == states.HITTING)
            return;

        state = states.WALK;

        if (jump.state != HeroJump.states.NONE)
            return;
        if (move.type == HeroMove.types.NORMAL)
        {
            if (weapons.type == WeaponPickable.types.WEAPON1)
                anim.Play("gun_walk");
            else if (weapons.type == WeaponPickable.types.WEAPON2)
                anim.Play("melee_walk");
            else
                anim.Play("walk");
        }
        else
        {
            if (!weapons.HasWeapon())
                anim.Play("run");
            else if (weapons.type == WeaponPickable.types.WEAPON1)
                anim.Play("gun_run");
            else if (weapons.type == WeaponPickable.types.WEAPON2)
                anim.Play("melee_run");
        }
    }
    public void Stress()
    {
        state = states.STRESS;
        anim.Play("stress");
        Invoke("ResetStress", 2);
    }
    void ResetStress()
    {
        if (state == states.STRESS)
            state = states.WALK;
        Idle();
    }
    public override void OnReceiveHit(HitArea hitArea, float force)
    {        
        if (state == states.DEAD || state == states.HITTED)
            return;

        move.ResetMove();

        if (state == states.DEFENDING)
        {
            force /= 2;
        }
        else
        {
            string hitName = "hit_punch_front";

            switch (hitArea.type)
            {
                case CharacterHitsManager.types.HIT_FORWARD:
                    hitName = "hit_punch_front";
                    break;
                case CharacterHitsManager.types.KICK_BACK:
                    hitName = "hit_punch_back";
                    break;
                case CharacterHitsManager.types.HIT_BACK:
                    hitName = "hit_punch_back";
                    break;
                case CharacterHitsManager.types.HIT_UPPER:
                    hitName = "hit_upper_front";
                    break;
            }
            state = states.HITTED;
            anim.Play(hitName);
            Invoke("GotoIdleAfterBeingHitted", stats.mana);
        }

        Events.OnHeroHitted(id, (force * 2) / stats.defense);
    }
    public override void OnFire(bool isOver)
    {
        if (state == states.DEAD || state == states.HITTED)
            return;
        if (isOver)
        {
            LoopInFire();
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
                sr.color = Color.red;
        }
        else
        {
            CancelInvoke();
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
                sr.color = Color.white;
        }

    }
    void LoopInFire()
    {
        if (state == states.DEAD || state == states.HITTED)
            return;
        Events.OnHeroHitted(id, 1);
        Invoke("LoopInFire", 0.2f);
    }
    public override void OnDie()
    {
        base.OnDie();
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        CancelInvoke();
    }
    void GotoIdleAfterBeingHitted()
    {
        if (state == states.STRESS)
            return;
        if (state == states.DEAD)
            return;
        Idle();
    }
    public void IsOverPickable(WeaponPickable _weaponPickable)
    {
        weaponPickable = _weaponPickable;
    }
    public void OnPick()
    {
        if (state == states.DEAD)
            return;
        weapons.GetWeapon(weaponPickable);

        if (weaponPickable.type == WeaponPickable.types.WEAPON1)
            anim.Play("pick_gun");
        else if (weaponPickable.type == WeaponPickable.types.WEAPON2)
            anim.Play("pick_melee");
        else if (weaponPickable.type == WeaponPickable.types.CINTURONGA)
            anim.Play("pick");

        weaponPickable.GotIt();
        weaponPickable = null;
    }
    public void Jump()
    {
        if (state == states.STRESS)
             return;
        if (state == states.DEAD)
            return;
        if (state == states.WALK || state == states.IDLE )
            jump.Jump();
    }
}
