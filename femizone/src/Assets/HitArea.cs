using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {

	public Character character;
	public types type;
	public enum types
	{
		HIT,
		RECEIVE_HIT
	}

	public void OnTriggerEnter(Collider col)
	{
		HitArea hitArea = col.gameObject.GetComponent<HitArea> ();
		if ( hitArea != null) 
		{
			if (hitArea.character == character)
				return;
			if (hitArea.type == types.RECEIVE_HIT)
				hitArea.character.ReceiveHit (1);
		}
	}
}
