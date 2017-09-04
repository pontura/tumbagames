using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
		
	public int HorizontalDirection;
	public int VerticalDirection;


	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow)) {
			HorizontalDirection = -1;
		}
		else if (Input.GetKey (KeyCode.RightArrow))
			HorizontalDirection = 1;
		else
			HorizontalDirection = 0;
		
		if (Input.GetKey (KeyCode.DownArrow) )
			VerticalDirection = -1;
		else if (Input.GetKey (KeyCode.UpArrow))
			VerticalDirection = 1;
		else
			VerticalDirection = 0;

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			
		}
		if (Input.GetKeyDown (KeyCode.LeftControl)) 
		{
			
		}

	}
}
