using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitsManager : MonoBehaviour {

	public enum types
	{
		HIT,
		HIT_BACK,
		KICK,
		KICKBACK,
		UPPER
	}
	Character character;

	int punchHitID =1;
	int kickhHitID =1;

	void Start()
	{
		character = GetComponent<Character> ();
	}
	public void SetOn(types type)
	{
		if (character.state == Character.states.HITTING)
			return;
		if (type == types.UPPER) {
			character.anim.Play ("upper");
		} else
		if (type == types.HIT_BACK) {
			character.anim.Play ("retro_punch");
		} else if (type == types.HIT) {
			punchHitID++;
			if (punchHitID > 2)
				punchHitID = 1;
			character.anim.Play ("punch_" + punchHitID);
		} else if (type == types.KICKBACK) {
			character.anim.Play ("retro_kick");
		}  else if (type ==  types.KICK) {
			kickhHitID++;
			if (kickhHitID > 2)
				kickhHitID = 1;
			character.anim.Play ("kick_"+ kickhHitID);
		}
		character.state = Character.states.HITTING;
		Invoke ("Reset", 0.2f);
	}
	void Reset(){
		character.Idle ();
	}
}
