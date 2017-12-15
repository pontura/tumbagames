using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesManager : MonoBehaviour {

	public Hero hero1;
	public Hero hero2;
	public Hero hero3;
	public Hero hero4;

	public Transform container;
	public List<Hero> all;

	void Start () {
		Hero hero;
		hero = Instantiate (hero1);
		hero.transform.SetParent (container);
		hero.transform.localPosition = new Vector3(-8,0,0);
		all.Add (hero);

		hero = Instantiate (hero2);
		hero.transform.SetParent (container);
		hero.transform.localPosition = new Vector3(-4,0,0);
		all.Add (hero);
	}

	public Vector3 CheckIfHeroIsClose(Character character)
	{
		Hero closestHero = GetClosestHero(character);
		if (closestHero != null) {

			if (closestHero.transform.position.x < character.transform.position.x)
				character.LookAt (true);
			else
				character.LookAt (false);

			float _x = Mathf.Abs(closestHero.transform.position.x -character.transform.position.x);
			float _z = Mathf.Abs(closestHero.transform.position.z -character.transform.position.z);

			if (_x < 2 && _z < 0.5f) {
				Vector3 newPos = transform.position;
				if (closestHero.transform.position.x < character.transform.position.x)
					newPos.x += 3;
				else
					newPos.x -= 3;
				return newPos;
			}
			else if(_x > 3.5f || _z > 1f)
				return closestHero.transform.position;
				
		}
		return Vector3.zero;
	}
	public Hero GetClosestHero(Character character)
	{
		float distance = 1000;
		Hero closestHero = null;
		foreach (Hero hero in all) {
			float distance_x = Mathf.Abs(hero.transform.position.x -character.transform.position.x);
			float distance_z = Mathf.Abs(hero.transform.position.z -character.transform.position.z);
			float realDistance = distance_x+(distance_z*2);
			if (realDistance < distance) {
				closestHero = hero;
				distance = realDistance;
			}
		}
		return closestHero;
	}


	public Vector3 CheckIfHeroIsClose_(Character character)
	{
		Vector3 pos = Vector3.zero;
		float distance = 1000;
		foreach (Hero hero in all) {
			float _x = Mathf.Abs(hero.transform.position.x -character.transform.position.x);
			float _z = Mathf.Abs(hero.transform.position.z -character.transform.position.z);
			if (_x+_z < distance) {
				if (hero.transform.position.x < character.transform.position.x)
					character.LookAt (true);
				else
					character.LookAt (false);
				if(_x > 1 || _z > 0.5f)
					pos = hero.transform.position;
			}
		}
		return pos;
	}
	public float GetMostAdvancedPosition()
	{
		float biggest_x = 0;
		float lowest_x = 1000;
		foreach (Hero hero in all) {
			if (hero.transform.position.x > biggest_x)
				biggest_x = hero.transform.position.x;
			else
				if (hero.transform.position.x < lowest_x)
					lowest_x = hero.transform.position.x;
		}
		if (biggest_x - lowest_x > 5)
			return 0;
		return biggest_x;
	}
}
