using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Summary : MonoBehaviour {

	void Start () {
		Invoke ("Done", 3);
		Events.OnMusicOff (true);
	}
	
	void Done () {
		SceneManager.LoadScene ("Intro");
	}
}
