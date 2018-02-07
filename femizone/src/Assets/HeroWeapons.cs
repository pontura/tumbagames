using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapons : MonoBehaviour {

	public WeaponPickable.types type;

	public bool HasWeapon()
	{
		if (type ==  WeaponPickable.types.NONE)
			return false;
		return true;
	}
	public void GetWeapon(WeaponPickable.types _type)
	{
		type = _type;
	}
	public void Reset()
	{
		type = WeaponPickable.types.NONE;
	}
}
