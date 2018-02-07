using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public int HorizontalDirection;
	public int VerticalDirection;
	Hero hero;

	bool justTurnedHorizontal;
	bool justForwardHorizontal;
	int newHorizontalDirection;

	void Start()
	{
		hero = GetComponent<Hero> ();
	}

	float timeOut = 0.1f;
	void Update () {
		if (hero.state == Character.states.DEAD)
			return;

		if (Input.GetAxis("Vertical" +hero.id) == -1) 
			VerticalDirection = -1;
		else if (Input.GetAxis("Vertical" +hero.id) == 1) 
			VerticalDirection = 1;
		else
			VerticalDirection = 0;

		if (Input.GetButtonDown("Hit" +hero.id)) 
		{
			if (hero.weaponPickable != null)
				hero.OnPick ();
			else if (hero.weapons.HasWeapon ())
				hero.hitsManager.SetOn (CharacterHitsManager.types.GUN_FIRE);
			else if(justTurnedHorizontal)
				hero.hitsManager.SetOn (CharacterHitsManager.types.HIT_BACK);
			else if(justForwardHorizontal)
				hero.hitsManager.SetOn (CharacterHitsManager.types.HIT_UPPER);
			else
				hero.hitsManager.SetOn (CharacterHitsManager.types.HIT_FORWARD);
			newHorizontalDirection = 0;
		}
		if (Input.GetButtonDown("Kick" +hero.id)) 
		{
			if(justTurnedHorizontal)
				hero.hitsManager.SetOn (CharacterHitsManager.types.KICK_BACK);
			else if(justForwardHorizontal)
				hero.hitsManager.SetOn (CharacterHitsManager.types.KICK_FOWARD);
			else
				hero.hitsManager.SetOn (CharacterHitsManager.types.KICK_DOWN);
			newHorizontalDirection = 0;
		}

		if (justTurnedHorizontal || justForwardHorizontal)
			return;
		
		if (Input.GetAxis("Horizontal" +hero.id) == -1 && newHorizontalDirection != -1)  {
			if (hero.transform.localScale.x > 0 && HorizontalDirection == 0) {				
				justTurnedHorizontal = true;
				Invoke ("ResetJustTurned", timeOut);
			} else  {	
				justForwardHorizontal = true;
				Invoke ("ResetJustTurned", timeOut);
			}
			newHorizontalDirection = -1;
		} else if (Input.GetAxis("Horizontal" +hero.id) == 1 && newHorizontalDirection != 1)  {
			if (hero.transform.localScale.x < 0 && HorizontalDirection == 0) {
				justTurnedHorizontal = true;
				CancelInvoke ();
				Invoke ("ResetJustTurned", timeOut);
			} else {	
				justForwardHorizontal = true;
				Invoke ("ResetJustTurned", timeOut);
			}
			newHorizontalDirection = 1;
		} else {
			newHorizontalDirection = 0;
			HorizontalDirection = 0;
		}
	}
	void ResetJustTurned()
	{

		if (newHorizontalDirection != 0) {
			HorizontalDirection = newHorizontalDirection;
			hero.transform.localScale = new Vector3 (newHorizontalDirection, 1, 1);
		}
		newHorizontalDirection = 0;
		justTurnedHorizontal = false;
		justForwardHorizontal = false;
	}

}
