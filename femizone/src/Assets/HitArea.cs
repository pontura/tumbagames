using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {

	public Character character;
	public types type;
	public enum types
	{
		HIT_UPPER,
		HIT_FRONT,
		HIT_BACK,
		HIT_DOWN,
		RECEIVE_HIT
	}

	public void OnTriggerEnter(Collider col)
	{
		HitArea punchHitArea = col.gameObject.GetComponent<HitArea> ();
		if ( punchHitArea != null) 
		{
			if (punchHitArea.character == character)
				return;
			if (punchHitArea.type == types.RECEIVE_HIT)
				punchHitArea.character.ReceiveHit (this, 1);
		}
	}
	public void SetType(types _type)
	{
		type = _type;
	}
}
