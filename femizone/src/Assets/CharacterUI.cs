using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

	public int life = 100;
	public Text scoreField;
	public Image image;
	public Image bar;
	public int heroID;

	void Start () {
		Events.OnCharacterDie += OnCharacterDie;
		Events.OnHeroHitted += OnHeroHitted;
		Events.GrabPowerUp += GrabPowerUp;
	}
	void GrabPowerUp(Hero hero, Powerup powerup)
	{
		if (hero.id != heroID)
			return;
		life += 50;
		if (life >100) {
			life = 100;
		} 
		bar.fillAmount = ((float)life)/100;
	}
	void OnCharacterDie(Character character)
	{
		Hero hero = character.GetComponent<Hero> ();
		if (hero == null)
			return;
		
		if (hero.id == heroID)
			print ("DIE");
	}
	public void OnHeroHitted(int _heroID, int force)
	{
		//print ("OnHeroHitted" + _heroID + " force" + force +  "  life  " + life);
		if (heroID != _heroID)
			return;
		life -= force;
		if (life < 0) {
			life = 0;
		} 
		bar.fillAmount = ((float)life)/100;
	}
}
