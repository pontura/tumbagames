﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreSummary : MonoBehaviour
{

    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
    }
    void OnHeroDie()
    {
        if (SocialManager.Instance.challengeData.isOn) return;
        Invoke("TimeOut", 1.5f);
    }
    void TimeOut()
    {
        panel.SetActive(true);
		Loop ();
    }
	void Loop()
	{
		Invoke ("Loop", 1);
		AchievementsEvents.NewSecondKilled ();
	}
    public void Ready()
    {
		CancelInvoke ();
        GetComponent<Summary>().Init();
        panel.SetActive(false);
    }
}
