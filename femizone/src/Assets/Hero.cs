using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character {

	public int id;
	private InputManager inputManager;

	public override void OnStart() {
		inputManager = GetComponent<InputManager> ();
	}
	public override void OnUpdate () {
		if (state == states.HITTING || state == states.HITTED)
			return;
		if ((inputManager.HorizontalDirection != 0 || inputManager.VerticalDirection != 0)) {			
			if(state != states.WALK)
				Walk ();
		}
		else if (state == states.WALK)
			Idle ();

		ChekToMove ();
	}
	void ChekToMove()
	{
		if (inputManager.HorizontalDirection < 0)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (inputManager.HorizontalDirection > 0)
			transform.localScale = new Vector3 (1, 1, 1);

		MoveTo (inputManager.HorizontalDirection, inputManager.VerticalDirection);

	}
	public override void OnReceiveHit(CharacterHitsManager.types type, int force)
	{
		print ("ReceiveHit " + type);
		string hitName = "hit_punch";

		switch (type) {
		case CharacterHitsManager.types.HIT_FORWARD:
			hitName = "hit_punch";
			break;
		case CharacterHitsManager.types.KICK_BACK:
			hitName = "hit_punch";
			break;
		case CharacterHitsManager.types.HIT_BACK:
			hitName = "hit_punch_back";
			break;
		case CharacterHitsManager.types.HIT_UPPER:
			hitName = "hit_upper";
			break;
		}

		state = states.HITTED;
		anim.Play (hitName);
		Invoke ("Idle", 0.7f);
		//stats.ReceiveHit (force);
	}
}
