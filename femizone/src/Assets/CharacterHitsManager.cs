using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitsManager : MonoBehaviour {
	
	public enum types
	{
		HIT,
		HIT_BACK,
		KICK,
		KICK_BACK,
		HIT_FORWARD,
		KICK_FOWARD,
	}
	Character character;

	int punchHitID =1;

	public HitArea hitArea;

	void Start()
	{
		character = GetComponent<Character> ();
	}
	public void SetOn(types type)
	{
		if (character.state == Character.states.HITTING || character.state == Character.states.HITTED)
			return;
		
		if (type == types.HIT_FORWARD) {
			character.anim.Play ("upper");
			hitArea.SetType (HitArea.types.HIT_UPPER);
		} else if (type == types.HIT_BACK) {
			character.anim.Play ("retro_punch");
			hitArea.SetType (HitArea.types.HIT_BACK);
		} else if (type == types.HIT) {
			punchHitID++;
			if (punchHitID > 2) {
				hitArea.SetType (HitArea.types.HIT_FRONT);
				punchHitID = 1;
			} else {
				hitArea.SetType (HitArea.types.HIT_FRONT);
			}
			character.anim.Play ("punch_" + punchHitID);
		} else if (type == types.KICK_BACK) {
			character.anim.Play ("retro_kick");
			hitArea.SetType (HitArea.types.HIT_BACK);
		} else if (type == types.KICK_FOWARD) {
			character.anim.Play ("kick_1");
			hitArea.SetType (HitArea.types.HIT_BACK);
		}  else if (type ==  types.KICK) {
			character.anim.Play ("kick_2");
		}
		character.state = Character.states.HITTING;
		Invoke ("Reset", 0.2f);
	}
	void Reset(){
		character.Idle ();
	}
}
