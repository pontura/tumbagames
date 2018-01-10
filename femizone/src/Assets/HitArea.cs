using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {

	public Character character;
	public CharacterHitsManager.types type;
	public int force;

	void Start()
	{
		Events.OnHeroDie += OnHeroDie;
	}
	void OnDestroy()
	{
		Events.OnHeroDie -= OnHeroDie;
	}
	void OnHeroDie(int id)
	{
		Hero hero = character.GetComponent<Hero>();
		if (hero == null || hero.id != id)
			return;
		this.enabled = false;
	}
	public void OnTriggerEnter(Collider col)
	{
		
		HitArea otherHitArea = col.gameObject.GetComponent<HitArea> ();

		if (otherHitArea == null)
			return;

		if (otherHitArea.character == null)
			return;
		
		if (otherHitArea.character == character)
			return;

		if (otherHitArea.character.GetComponent<Hero>()  && character.GetComponent<Hero>())
			return;

		//primer golpecito
		if (type == CharacterHitsManager.types.RECEIVE_HIT && otherHitArea.type == CharacterHitsManager.types.RECEIVE_HIT			
			&&
			(otherHitArea.character.GetComponent<Enemy>() && character.GetComponent<Hero>())
		)
		{
			if (otherHitArea.character.GetComponent<Enemy> ().progressBar.bar.fillAmount == 1) {
				otherHitArea.character.ReceiveHit (this, 1);
				Events.OnReceiveit (type, otherHitArea.character);
			}
			return;
		}


		if ( otherHitArea != null) 
		{
			if (otherHitArea.character == character)
				return;
			if (otherHitArea.type == CharacterHitsManager.types.RECEIVE_HIT) {
				if (type != CharacterHitsManager.types.RECEIVE_HIT && otherHitArea.character.state != Character.states.DEAD) {
					otherHitArea.character.ReceiveHit (this, force);
					character.OnFreeze ();
					Events.OnReceiveit (type, otherHitArea.character);
				}
				
			}
		}
	}
	public void SetType(CharacterHitsManager.types _type, int _force)
	{
		type = _type;
		this.force = _force;
	}
}
