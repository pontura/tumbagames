using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWeapons : MonoBehaviour {

	public WeaponPickable.types type;
	public int totalUses;
	Hero hero;
    public Wearable wearable_cinturonga;
    Wearable cinturonga;

    void Start()
	{
		hero = GetComponent<Hero>();
        foreach (WearableItem wearableItem in GetComponentsInChildren<WearableItem>())
            if (wearableItem.weaponPickableType == WeaponPickable.types.CINTURONGA)
            {
                cinturonga = Instantiate(wearable_cinturonga);
                cinturonga.transform.SetParent(wearableItem.transform);
                cinturonga.transform.localPosition = Vector3.zero;
                cinturonga.transform.localScale = Vector3.one;
                cinturonga.transform.localEulerAngles = Vector3.zero;
            }

        ActivateWearable(cinturonga, false);
    }
	public void Use()
	{
		totalUses--;
		if (totalUses <= 0) {
			Reset ();
		}
        if (type == WeaponPickable.types.WEAPON1)
            hero.hitsManager.SetOn(CharacterHitsManager.types.GUN_FIRE);
        else if (type == WeaponPickable.types.WEAPON2)
            hero.hitsManager.SetOn(CharacterHitsManager.types.MELEE);
        else if (type == WeaponPickable.types.CINTURONGA)
        {
            hero.hitsManager.SetOn(CharacterHitsManager.types.CINTURONGA);
            cinturonga.Attack(1);
        }
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
        if(weapon.type == WeaponPickable.types.CINTURONGA)
            ActivateWearable(cinturonga, true);
    }
	public void Reset()
	{
		type = WeaponPickable.types.NONE;
        ActivateWearable(cinturonga, false);
    }
    void ActivateWearable(Wearable wearableItem, bool isOn)
    {
        wearableItem.gameObject.SetActive(isOn);
    }
}
