using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoSignal : MonoBehaviour
{
	public GameObject panel;
	void Start()
	{
		panel.SetActive (false);
		Events.OnStageClear += OnStageClear;
		Events.OnMansPlaining += OnMansPlaining;
        Events.GameOver += GameOver;

    }

	void OnDestroy()
	{
		Events.OnStageClear -= OnStageClear;
		Events.OnMansPlaining -= OnMansPlaining;
        Events.GameOver -= GameOver;
    }
    void GameOver()
    {
        Destroy(gameObject);
    }

    void SetOff()
	{
		CancelInvoke ();
		panel.SetActive (false);
	}
	void OnMansPlaining(Character c, bool isOn)
	{
		SetOff ();
	}
	void OnStageClear()
	{
		Loop ();
		panel.SetActive (true);
	}
	void Loop()
	{
		if (World.Instance.state == World.states.FIGHTING ||  World.Instance.state == World.states.AWAITING) {
			SetOff ();
			return;
		}
		Invoke ("Loop", 1);
	}
}
