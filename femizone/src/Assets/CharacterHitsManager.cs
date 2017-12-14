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
		KICK_DOWN
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
		
		if (type == types.HIT_UPPER) {
			character.anim.Play ("upper");
			hitArea.SetType (types.HIT_UPPER);
		} else if (type == types.HIT_BACK) {
			character.anim.Play ("retro_punch");
			hitArea.SetType (types.HIT_BACK);
		} else if (type == types.HIT_FORWARD) {
			punchHitID++;
			if (punchHitID > 2) {
				hitArea.SetType (types.HIT_FORWARD);
				punchHitID = 1;
			} else {
				hitArea.SetType (types.HIT_FORWARD);
			}
			character.anim.Play ("punch_" + punchHitID);
		} else if (type == types.KICK_BACK) {
			character.anim.Play ("retro_kick");
			hitArea.SetType (types.KICK_BACK);
		} else if (type == types.KICK_FOWARD) {
			character.anim.Play ("kick_1");
			hitArea.SetType (types.KICK_FOWARD);
		}  else if (type == types.KICK_DOWN) {
			character.anim.Play ("kick_2");
		}
		character.state = Character.states.HITTING;
		Invoke ("Reset", 0.2f);
	}
	void Reset(){
		character.Idle ();
	}
}
