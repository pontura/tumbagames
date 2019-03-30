using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary : MonoBehaviour {

	void Start () {
		Invoke ("Done", 3);
		Events.OnMusicOff (true);
	}
	
	void Done () {
		Data.Instance.LoadScene ("Intro");
	}
}
