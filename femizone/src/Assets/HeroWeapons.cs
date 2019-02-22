using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapons : MonoBehaviour {

	public WeaponPickable.types type;
	public int totalUses;

	public void Use()
	{
		totalUses--;
		if (totalUses <= 0) {
			Reset ();
		}
	}
	public bool HasWeapon()
	{
		if (type ==  WeaponPickable.types.NONE)
			return false;
		return true;
	}
	public void GetWeapon(WeaponPickable.types _type)
	{
		totalUses = 3;
		type = _type;
	}
	public void Reset()
	{
		type = WeaponPickable.types.NONE;
	}
}
