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
		GUN_FIRE
	}
	Character character;

	int punchHitID =1;

	public HitArea hitArea;

	void Start()
	{
		character = GetComponent<Character> ();
		hitArea.character = character;
	}
	bool AttakEnabled()
	{
		if (character.state == Character.states.DEAD || character.state == Character.states.HITTING || character.state == Character.states.HITTED)
			return false;
		return true;
	}

	public void SetOn(types type)
	{
		if (!AttakEnabled ())
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

		print (type + " " + clipName);
	}
	void Reset(){
		if (character.state == Character.states.DEAD || character.state == Character.states.HITTED)
			return;
		character.Idle ();
	}
}
