using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character {
	
	private InputManager inputManager;

	public override void OnStart() {
		inputManager = GetComponent<InputManager> ();	
		Events.OnCharacterHit += OnCharacterHit;
	}
	public override void OnUpdate () {
		if (state == states.HITTING)
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
}
