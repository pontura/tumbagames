using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickable : SceneObject {
	
	public types type;
	public int uses;
	public enum types
	{
		NONE,
		WEAPON1,
		WEAPON2,
        CINTURONGA
	}
	public void Init () {

	}
	void OnTriggerEnter(Collider col)
	{
		HitArea otherHitArea = col.gameObject.GetComponent<HitArea> ();

		if (otherHitArea == null)
			return;

		Hero hero = otherHitArea.character.GetComponent<Hero> ();
		if (hero == null)
			return;

		hero.IsOverPickable (this);
	}
	void OnTriggerExit(Collider col)
	{
		HitArea otherHitArea = col.gameObject.GetComponent<HitArea> ();

		if (otherHitArea == null)
			return;

		Hero hero = otherHitArea.character.GetComponent<Hero> ();
		if (hero == null)
			return;

		hero.IsOverPickable (null);
	}
	public void GotIt()
	{
		Destroy (gameObject);
	}
}