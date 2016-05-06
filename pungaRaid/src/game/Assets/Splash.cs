using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    void Start()
    {
        Invoke("GotoGame", 7);
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("02_Intro");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
