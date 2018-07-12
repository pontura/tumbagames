using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScreen : MonoBehaviour {

	public GameObject panel;
	bool isOn;
	void Start () {
		panel.SetActive (false);

		if (Data.Instance.dontExitOnESC)
			return;
		
		Events.OnExit += OnExit;
	}

	void OnExit () {
		Time.timeScale = 0;
		panel.SetActive (true);
		isOn = true;
	}
	void Update()
	{
		if (Data.Instance.dontExitOnESC)
			return;
		
		if (SceneManager.GetActiveScene ().name == "11_Hiscores" || SceneManager.GetActiveScene ().name == "01_Splash")
			return;
		if (Input.GetKeyUp (KeyCode.Alpha1))
			OnExit ();
		if(!isOn)
			return;		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Application.Quit();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			Close ();
		}		
	}
	void Close()
	{
		Time.timeScale = 1;
		isOn = false;
		panel.SetActive (false);
	}
}
