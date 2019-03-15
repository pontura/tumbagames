using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapons : MonoBehaviour {

	public WeaponPickable.types type;
	public int totalUses;
	Hero hero;
	void Start()
	{
		hero = GetComponent<Hero>();
	}
	public void Use()
	{
		totalUses--;
		if (totalUses <= 0) {
			Reset ();
		}
		if(type == WeaponPickable.types.WEAPON1)
			hero.hitsManager.SetOn (CharacterHitsManager.types.GUN_FIRE);
		else if(type == WeaponPickable.types.WEAPON2)
			hero.hitsManager.SetOn (CharacterHitsManager.types.MELEE);
	}
	public bool HasWeapon()
	{
		if (type ==  WeaponPickable.types.NONE)
			return false;
		return true;
	}
	public void GetWeapon(WeaponPickable weapon)
	{
		totalUses = weapon.uses;
		type = weapon.type;
	}
	public void Reset()
	{
		type = WeaponPickable.types.NONE;
	}
}
