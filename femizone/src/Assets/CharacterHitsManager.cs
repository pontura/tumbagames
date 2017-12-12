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
	public HitArea hitArea;

	void Start()
	{
		character = GetComponent<Character> ();
	}
	public void SetOn(types type)
	{
		if (character.state == Character.states.HITTING || character.state == Character.states.HITTED)
			return;
		
		if (type == types.UPPER) {
			character.anim.Play ("upper");
			hitArea.SetType (HitArea.types.HIT_UPPER);
		} else
		if (type == types.HIT_BACK) {
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
		} else if (type == types.KICKBACK) {
			character.anim.Play ("retro_kick");
			hitArea.SetType (HitArea.types.HIT_BACK);
		}  else if (type ==  types.KICK) {
			kickhHitID++;
			if (kickhHitID > 2) {
				hitArea.SetType(HitArea.types.HIT_FRONT);
				kickhHitID = 1;
			} else {
				hitArea.SetType (HitArea.types.HIT_FRONT);
			}
			character.anim.Play ("kick_"+ kickhHitID);
		}
		character.state = Character.states.HITTING;
		Invoke ("Reset", 0.2f);
	}
	void Reset(){
		character.Idle ();
	}
}
