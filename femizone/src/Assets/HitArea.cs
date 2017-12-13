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
		
		HitArea otherHitArea = col.gameObject.GetComponent<HitArea> ();

		if (otherHitArea == null)
			return;
		
		if (type == types.RECEIVE_HIT && otherHitArea.type == types.RECEIVE_HIT) {
			if(otherHitArea.character.GetComponent<Enemy>() && character.GetComponent<Hero>())
				otherHitArea.character.ReceiveHit (this, 1);
			return;
		}

		print ("AAAAAAA" + col.name);

		if ( otherHitArea != null) 
		{
			if (otherHitArea.character == character)
				return;
			if (otherHitArea.type == types.RECEIVE_HIT)
				otherHitArea.character.ReceiveHit (this, 1);
		}
	}
	public void SetType(types _type)
	{
		type = _type;
	}
}
