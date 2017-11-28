using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public int HorizontalDirection;
	public int VerticalDirection;
	Character character;
	public bool justTurned;

	void Start()
	{
		character = GetComponent<Character> ();
	}
	public int newDirection;
	float timeOut = 0.085f;
	void Update () {
		

		if (Input.GetKey (KeyCode.DownArrow) )
			VerticalDirection = -1;
		else if (Input.GetKey (KeyCode.UpArrow))
			VerticalDirection = 1;
		else
			VerticalDirection = 0;

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if(justTurned)
				character.hitsManager.SetOn (CharacterHitsManager.types.HIT_BACK);
			else if(VerticalDirection == 1)
				character.hitsManager.SetOn (CharacterHitsManager.types.UPPER);
			else
				character.hitsManager.SetOn (CharacterHitsManager.types.HIT);
			newDirection = 0;
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
			if(justTurned)
				character.hitsManager.SetOn (CharacterHitsManager.types.KICKBACK);
			else
				character.hitsManager.SetOn (CharacterHitsManager.types.KICK);
			newDirection = 0;
		}

		if (justTurned)
			return;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (character.transform.localScale.x > 0 && HorizontalDirection ==0) {				
				justTurned = true;
				Invoke ("ResetJustTurned", timeOut);
				newDirection = -1;
			} else {
				HorizontalDirection = -1;
			}
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			if (character.transform.localScale.x < 0 && HorizontalDirection ==0) {
				justTurned = true;
				CancelInvoke ();
				Invoke ("ResetJustTurned", timeOut);
				newDirection = 1;
			} else {
				HorizontalDirection = 1;
			}
		} else {
			newDirection = 0;
			HorizontalDirection = 0;
		}
	}
	void ResetJustTurned()
	{

		if (newDirection != 0) {
			HorizontalDirection = newDirection;
			character.transform.localScale = new Vector3 (newDirection, 1, 1);
		}
		
		justTurned = false;
	}

}
