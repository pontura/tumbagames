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
		if (state == states.DEAD  || state == states.HITTING || state == states.HITTED)
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
	public override void OnReceiveHit(HitArea hitArea, int force)
	{
		if (state == states.DEAD  || state == states.HITTED)
			return;
		string hitName = "hit_punch";

		switch (hitArea.type) {
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
		Invoke ("GotoIdleAfterBeingHitted", 0.7f);

		Events.OnHeroHitted (id, force);
		//stats.ReceiveHit (force);
	}
	public override void OnDie ()
	{
		base.OnDie ();
		CancelInvoke ();
	}
	void GotoIdleAfterBeingHitted()
	{
		if (state == states.DEAD)
			return;
		Idle ();
	}
}
