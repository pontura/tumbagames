using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown("Hit1")) 
		{
			SceneManager.LoadScene ("Game");
		}
			
	}
}
