using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour {

	public Hero hero_to_instantiate;
	public Transform container;
	public List<Hero> all;

	void Start () {
		Hero hero = Instantiate (hero_to_instantiate);
		hero.transform.SetParent (container);
		hero.transform.localPosition = new Vector3(-8,0,0);
		all.Add (hero);
	}
	public Vector3 CheckIfHeroIsClose(Character character)
	{
		Vector3 pos = Vector3.zero;
		float distance = 1000;
		foreach (Hero hero in all) {
			float newDistance = Vector3.Distance(hero.transform.position, character.transform.position);
			if (newDistance < distance) {
				if (hero.transform.position.x < character.transform.position.x)
					character.LookAt (true);
				else
					character.LookAt (false);
				if(newDistance > 4)
					pos = hero.transform.position;
			}
		}
		return pos;
	}
}
