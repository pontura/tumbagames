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
		Events.OnHeroDie += OnHeroDie;
		Events.AddHero += AddHero;
	}

	public void AddHero(int characterID)
	{
		Hero hero;
		switch (characterID) {
		case 1:
			hero = Instantiate (hero1);
			break;	
		case 2:
			hero = Instantiate (hero2);
			break;	
		case 3:
			hero = Instantiate (hero3);
			break;	
		default:
			hero = Instantiate (hero4);
			break;	
		}
		hero.transform.SetParent (container);
		Vector3 pos = World.Instance.worldCamera.transform.localPosition;
		pos.x += Random.Range (-4, 4);
		pos.y = 0;
		pos.z =  Random.Range (2, 6);
		hero.transform.localPosition = pos;
		all.Add (hero);
	}
	void OnDestroy () {
		Events.OnHeroDie -= OnHeroDie;
		Events.AddHero -= AddHero;
	}
	void OnHeroDie(int id)
	{		
		Hero hero = GetHero(id);
		hero.Die ();
		all.Remove (hero);		
		if (all.Count == 0)
			World.Instance.GameOver ();
	}
	Hero GetHero(int id)
	{
		foreach (Hero hero in all) {
			if (hero.id == id)
				return hero;
		}
		return null;
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
	public float GetPercentPosition()
	{
		float totalPosition = 0;
		foreach (Hero hero in all) {
			totalPosition += hero.transform.position.x;
		}
		totalPosition/=all.Count;
		return totalPosition;
	}
}
