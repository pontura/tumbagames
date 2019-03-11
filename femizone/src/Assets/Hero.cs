using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{

    public int id;
    private InputManager inputManager;
    public WeaponPickable weaponPickable;
    public HeroWeapons weapons;
    Rigidbody rb;
    WorldCamera worldCamera;
    int offsetMoveX = 10;

    public override void OnStart()
    {
        worldCamera = World.Instance.worldCamera;
        inputManager = GetComponent<InputManager>();
        weapons = GetComponent<HeroWeapons>();
        rb = GetComponent<Rigidbody>();
    }
    public override void OnUpdate()
    {
        if (state == states.DEAD || state == states.HITTING || state == states.HITTED)
            return;
        if ((inputManager.HorizontalDirection != 0 || inputManager.VerticalDirection != 0))
        {
            if (state != states.WALK)
                Walk();
        }
        else if (state == states.WALK)
            Idle();

        ChekToMove();
        rb.velocity = Vector3.zero;
    }
    public override void Idle()
    {
        state = states.IDLE;
        if (!weapons.HasWeapon())
            anim.Play("idle");
        else if (weapons.type == WeaponPickable.types.WEAPON1)
            anim.Play("idle_gun");
        else if (weapons.type == WeaponPickable.types.WEAPON2)
            anim.Play("melee_idle");
        OnIdle();
    }
    public override void Walk()
    {
        if (state == states.HITTING)
            return;
        state = states.WALK;
        if (!weapons.HasWeapon())
            anim.Play("walk");
        else if (weapons.type == WeaponPickable.types.WEAPON1)
            anim.Play("gun_walk");
        else if (weapons.type == WeaponPickable.types.WEAPON2)
            anim.Play("melee_walk");
    }
    void ChekToMove()
    {
        if (inputManager.HorizontalDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            if (transform.localPosition.x < worldCamera.transform.localPosition.x - offsetMoveX)
                return;
        }
        else if (inputManager.HorizontalDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (transform.localPosition.x > worldCamera.transform.localPosition.x + offsetMoveX)
                return;
        }
        MoveTo(inputManager.HorizontalDirection, inputManager.VerticalDirection);

    }
    public override void OnReceiveHit(HitArea hitArea, float force)
    {
        print("hitted " + force + " state:  " + state);
        if (state == states.DEAD || state == states.HITTED)
            return;

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
    public override void OnDie()
    {
        base.OnDie();
        rb.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        CancelInvoke();
    }
    void GotoIdleAfterBeingHitted()
    {
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

        weapons.GetWeapon(weaponPickable.type);
        if (weaponPickable.type == WeaponPickable.types.WEAPON1)
            anim.Play("pick_gun");
        else if (weaponPickable.type == WeaponPickable.types.WEAPON2)
            anim.Play("pick_melee");
        weaponPickable.GotIt();
        weaponPickable = null;
    }
}
