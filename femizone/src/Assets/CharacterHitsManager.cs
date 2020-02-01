using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitsManager : MonoBehaviour {
	
	public enum types
	{
		RECEIVE_HIT,
		HIT_FORWARD,
		HIT_BACK,
		HIT_UPPER,
		KICK_FOWARD,
		KICK_BACK,
		KICK_DOWN,
        GUN_FIRE,
		MELEE,
		SPECIAL,
		FIRE,
        HEAD,
        KICK_JUMP,
        HIT_JUMP,
        CINTURONGA,
        HIT_DASH
	}
	Character character;

	int punchHitID =1;

	public HitArea hitArea;
    public HitArea hitAreaReceiver;
    HeroJump heroJump;
    HeroPowerManager powerManager;
    Hero hero;

    void Start()
	{       
        character = GetComponent<Character> ();
        hero = character.GetComponent<Hero>();

        hitArea.character = character;
        heroJump = character.GetComponent<HeroJump>();
        powerManager = GetComponent<HeroPowerManager>();
    }
	bool AttakEnabled()
	{
        if (character.state == Character.states.STRESS)
            return false;
        if (character.state == Character.states.DEAD || character.state == Character.states.HITTING || character.state == Character.states.HITTED)
			return false;
		return true;
	}
    public void SetStateForReceiver(bool isOn)
    {
        hitAreaReceiver.enabled = isOn;
    }

	public void SetOn(types type)
	{
        float attackPowerLess = 0;

        if (powerManager != null)
            attackPowerLess = powerManager.CalculateStress();

        if (character.state == Character.states.DEAD)
            return;

        if (!AttakEnabled () && type != types.SPECIAL)
			return;
		
		CancelInvoke ();

        if (heroJump != null)
        {
            if (type == types.KICK_DOWN || type == types.KICK_FOWARD)
            {
                if (heroJump.state != HeroJump.states.NONE)
                    type = types.KICK_JUMP;
            } else if (type == types.HIT_FORWARD || type == types.HIT_UPPER)
            {
                if (heroJump.state != HeroJump.states.NONE)
                    type = types.HIT_JUMP;
            }
        }
        if (hero != null && hero.move.type == HeroMove.types.RUN)
        {
            hero.move.ChangeType(HeroMove.types.NORMAL);
            type = types.HIT_DASH;
        }

        AttackStyle attackStyle = character.stats.GetAttackByType (type);
		string clipName = attackStyle.animClip.name;
      
		if (type == types.HIT_FORWARD) {
			punchHitID++;
			if (punchHitID > 2) 				
				punchHitID = 1;
			clipName = "punch_" + punchHitID;
		}
        character.anim.Play (clipName);

		hitArea.SetType (type, attackStyle.force);

		character.state = Character.states.HITTING;

		Invoke ("Reset", attackStyle.timeToReset);

		Events.OnAttack (attackStyle.type, character);

	}
	void Reset(){
		if (character.state == Character.states.DEAD || character.state == Character.states.HITTED)
			return;
		character.Idle ();
	}
}
