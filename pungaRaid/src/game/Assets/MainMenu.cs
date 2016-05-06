using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void Connect()
    {
        Events.OnLoginAdvisor();
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("02_Main");
	}
    public void OnSettings()
    {
        if (Time.timeScale == 0) return;
        Events.OnSettings();
    }
}
