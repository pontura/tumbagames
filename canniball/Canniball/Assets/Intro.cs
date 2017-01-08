using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {

	void Start()
    {
        Events.OnMusicChange("win_rugby1");
    }
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            Data.Instance.LoadScene("Intro2");
        }
	}
}
