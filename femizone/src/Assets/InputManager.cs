using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public int HorizontalDirection;
	public int VerticalDirection;
	Character character;

	bool justTurnedHorizontal;
	bool justForwardHorizontal;
	int newHorizontalDirection;

	void Start()
	{
		character = GetComponent<Character> ();
	}

	float timeOut = 0.1f;
	void Update () {
		

		if (Input.GetKey (KeyCode.DownArrow) )
			VerticalDirection = -1;
		else if (Input.GetKey (KeyCode.UpArrow))
			VerticalDirection = 1;
		else
			VerticalDirection = 0;

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if(justTurnedHorizontal)
				character.hitsManager.SetOn (CharacterHitsManager.types.HIT_BACK);
			else if(justForwardHorizontal)
				character.hitsManager.SetOn (CharacterHitsManager.types.HIT_FORWARD);
			else
				character.hitsManager.SetOn (CharacterHitsManager.types.HIT);
			newHorizontalDirection = 0;
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
			if(justTurnedHorizontal)
				character.hitsManager.SetOn (CharacterHitsManager.types.KICK_BACK);
			else if(justForwardHorizontal)
				character.hitsManager.SetOn (CharacterHitsManager.types.KICK_FOWARD);
			else
				character.hitsManager.SetOn (CharacterHitsManager.types.KICK);
			newHorizontalDirection = 0;
		}

		if (justTurnedHorizontal || justForwardHorizontal)
			return;
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (character.transform.localScale.x > 0 && HorizontalDirection == 0) {				
				justTurnedHorizontal = true;
				Invoke ("ResetJustTurned", timeOut);
				newHorizontalDirection = -1;
			} else  {	
				justForwardHorizontal = true;
				Invoke ("ResetJustTurned", timeOut);
				HorizontalDirection = -1;
			}
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			if (character.transform.localScale.x < 0 && HorizontalDirection ==0) {
				justTurnedHorizontal = true;
				CancelInvoke ();
				Invoke ("ResetJustTurned", timeOut);
				newHorizontalDirection = 1;
			} else {	
				justForwardHorizontal = true;
				Invoke ("ResetJustTurned", timeOut);
				HorizontalDirection = 1;
			}
		} else {
			newHorizontalDirection = 0;
			HorizontalDirection = 0;
		}
	}
	void ResetJustTurned()
	{

		if (newHorizontalDirection != 0) {
			HorizontalDirection = newHorizontalDirection;
			character.transform.localScale = new Vector3 (newHorizontalDirection, 1, 1);
		}
		
		justTurnedHorizontal = false;
		justForwardHorizontal = false;
	}

}
