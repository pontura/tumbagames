using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersUI : MonoBehaviour
{
	public CharacterUI[] all;

    void Start()
    {
		int id = 1;
		foreach (CharacterUI cui in all) {
			cui.Init (id);
			id++;
		}
		Events.OnKeyPress += OnKeyPress;
		Events.OnHeroHitted += OnHeroHitted;
		Events.GrabPowerUp += GrabPowerUp;
	}
	void OnDestroy()
	{
		Events.OnKeyPress -= OnKeyPress;
		Events.OnHeroHitted -= OnHeroHitted;
		Events.GrabPowerUp -= GrabPowerUp;
	}
	void OnKeyPress(int characterID)
	{
		CharacterUI cui = GetUI (characterID);
		if (cui.id == characterID && cui.state == CharacterUI.states.WAITING) {
			cui.SetState (CharacterUI.states.PLAYING);
			Events.AddHero (characterID);
		}
	}
	void OnHeroHitted(int characterID, float force)
	{
		print ("OnHeroHitted " + force);
		CharacterUI cui = GetUI (characterID);
		cui.OnHeroHitted (force);
	}
	void GrabPowerUp(Hero hero, Powerup powerup)
	{
		CharacterUI cui = GetUI (hero.id);
		cui.GrabPowerUp (powerup);
	}
	CharacterUI GetUI(int characterID)
	{
		foreach (CharacterUI cui in all) 
			if(cui.id == characterID)
				return cui;	
		return null;
	}

}
