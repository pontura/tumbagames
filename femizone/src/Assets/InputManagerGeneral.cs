using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerGeneral : MonoBehaviour
{
	void Update () {
		for (int a = 1; a < 5; a++) {
			if (Input.GetButtonDown ("Hit" + a)) {
				Events.OnKeyPress (a);
			} else if (Input.GetButtonDown ("Kick" + a)) {
				Events.OnKeyPress (a);
			} 
		}
	}
}
