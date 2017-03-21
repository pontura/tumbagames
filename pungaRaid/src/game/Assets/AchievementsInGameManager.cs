using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsInGameManager : MonoBehaviour {

	bool ready;
	private int moodID;
	private int seccionalID;

	void Start () {
		Events.OnHeroDie += OnHeroDie;
		Events.OnCloseSharePopup += OnCloseSharePopup;
		AchievementsEvents.OnAchievementReady += OnAchievementReady;

		Loop ();
		moodID = Data.Instance.moodsManager.GetCurrentSeccional ().moodID;
		seccionalID = Data.Instance.moodsManager.GetCurrentSeccional ().id;
	}
	void OnDestroy()
	{
		Events.OnHeroDie -= OnHeroDie;
		Events.OnCloseSharePopup -= OnCloseSharePopup;
		AchievementsEvents.OnAchievementReady -= OnAchievementReady;
	}
	void OnHeroDie()
	{
		ready = true;
		CancelInvoke ();
	}
	float lastTimeScale;
	void OnAchievementReady(Achievement ach)
	{
		lastTimeScale = Time.timeScale;

		string medalName = ach.image;
		string text = ach.title;

		Events.OnShowAchievementSignal (medalName, text, true);
		Invoke ("Delayed", 0.1f);

		if(Data.Instance.musicManager.volume==1)
			Events.OnMusicVolumeChanged(0.2f);
	}
	void Delayed()
	{
		lastTimeScale = Time.timeScale;
		Time.timeScale = 0;
	}
	void Loop () {
		
		Invoke ("Loop", 2);

		if (Time.timeScale == 0)
			return;

		if (ready)
			return;

		int score = Game.Instance.gameManager.score;
		float distance = Game.Instance.gameManager.distance;

		AchievementsEvents.OnNewDistance (moodID, seccionalID, distance);
		AchievementsEvents.OnCheckMoney (moodID,seccionalID , score);

	}
	void OnCloseSharePopup()
	{
		if(Data.Instance.musicManager.volume==0.2f)
			Events.OnMusicVolumeChanged(1);
		
		Time.timeScale = 1;
	}
}
