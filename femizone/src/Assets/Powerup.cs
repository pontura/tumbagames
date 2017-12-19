using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : SceneObject {

	public void Init () {
		
	}
	void OnTriggerEnter(Collider col)
	{

		HitArea otherHitArea = col.gameObject.GetComponent<HitArea> ();

		print (otherHitArea.character);

		if (otherHitArea == null)
			return;

		Hero hero = otherHitArea.character.GetComponent<Hero> ();
		if (hero == null)
			return;

		Events.GrabPowerUp (hero, this);

		Destroy (this.gameObject);
	}
}
