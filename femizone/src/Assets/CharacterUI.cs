using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

	public int life;
	public Text scoreField;
	public Image image;
	public Image bar;
	public int heroID;

	void Start () {
		life = Data.Instance.settings.totalLife;
		Events.OnHeroHitted += OnHeroHitted;
		Events.GrabPowerUp += GrabPowerUp;
	}
	void GrabPowerUp(Hero hero, Powerup powerup)
	{
		if (hero.id != heroID)
			return;
		life += Data.Instance.settings.totalLife/2;
		if (life >Data.Instance.settings.totalLife) {
			life = Data.Instance.settings.totalLife;
		} 
		bar.fillAmount = ((float)life)/(float)Data.Instance.settings.totalLife;
	}
	public void OnHeroHitted(int _heroID, int force)
	{
		if (heroID != _heroID)
			return;
		life -= force;
		if (life < 0) {
			life = 0;
			Events.OnHeroDie (_heroID);
		} 
		bar.fillAmount = ((float)life)/(float)Data.Instance.settings.totalLife;
	}
}
