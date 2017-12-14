using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {

	public Character character;
	public CharacterHitsManager.types type;

	public void OnTriggerEnter(Collider col)
	{
		
		HitArea otherHitArea = col.gameObject.GetComponent<HitArea> ();

		if (otherHitArea == null)
			return;
		
		if (type == CharacterHitsManager.types.RECEIVE_HIT && otherHitArea.type == CharacterHitsManager.types.RECEIVE_HIT) {
			if(otherHitArea.character.GetComponent<Enemy>() && character.GetComponent<Hero>())
				otherHitArea.character.ReceiveHit (this, 1);
			return;
		}


		if ( otherHitArea != null) 
		{
			if (otherHitArea.character == character)
				return;
			if (otherHitArea.type == CharacterHitsManager.types.RECEIVE_HIT) {
				otherHitArea.character.ReceiveHit (this, 1);
			}
		}
	}
	public void SetType(CharacterHitsManager.types _type)
	{
		type = _type;
	}
}
