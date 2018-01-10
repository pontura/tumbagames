﻿using System.Collections;
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
		KICK_DOWN
	}
	Character character;

	int punchHitID =1;

	public HitArea hitArea;

	void Start()
	{
		character = GetComponent<Character> ();
		hitArea.character = character;
	}
	public void SetOn(types type)
	{
		if (character.state == Character.states.DEAD || character.state == Character.states.HITTING || character.state == Character.states.HITTED)
			return;
		
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
		Invoke ("Reset", 0.2f);

		Events.OnAttack (attackStyle.type, character);
	}
	void Reset(){
		if (character.state == Character.states.DEAD || character.state == Character.states.HITTED)
			return;
		character.Idle ();
	}
}
