using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	void Update () {
		for (int a = 1; a < 5; a++) {
			if (Input.GetButtonDown ("Hit" + a) || Input.GetButtonDown ("Kick" + a) || Input.GetButtonDown ("Defense" + a)) {
				SceneManager.LoadScene ("Game");
			}
		}			
	}
}
