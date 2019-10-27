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
		if (character == null)
			return;
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

		Hero hero = character.GetComponent<Hero>();
		Enemy otherEnemy = otherHitArea.character.GetComponent<Enemy>();

		if (otherHitArea.character.GetComponent<Hero>()  && character.GetComponent<Hero>())
			return;

		//primer golpecito con el pecho
		if (type == CharacterHitsManager.types.RECEIVE_HIT && otherHitArea.type == CharacterHitsManager.types.RECEIVE_HIT			
			&&
			(otherEnemy != null && hero != null)
		)
		{
			if (otherEnemy.progressBar.bar.fillAmount == 1) {
				Events.OnAddScore(hero.id, 100);
				Events.OnMansPlaining (otherHitArea.character, false);
				otherHitArea.character.ReceiveHit (this, 1);
				Events.OnReceiveit (type, otherHitArea.character);
			}
			return;
		}


		if ( otherHitArea != null) 
		{
			if (otherHitArea.character == character)
				return;
			if (hero != null && otherEnemy != null && otherEnemy.progressBar.bar.fillAmount == 1) {
				Events.OnMansPlaining (otherHitArea.character, false);
			}
			if (otherHitArea.type == CharacterHitsManager.types.RECEIVE_HIT) {
				if (type != CharacterHitsManager.types.RECEIVE_HIT && otherHitArea.character.state != Character.states.DEAD) {

					if( hero != null && otherEnemy != null)
						Events.OnAddScore(hero.id, otherEnemy.stats.scoreByBeingHit);

					otherHitArea.character.ReceiveHit (this, force);
                    otherHitArea.character.OnFreeze ();
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
