using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	private Image image;
	public GameObject blocked;
	private Achievements achievements;
	private Achievement achievement;
	private bool isReady;

	public void Init(Achievements achievements,  Achievement _achievement, bool ready) {
		
		if (ready)
			isReady = true;
		
		this.achievement = _achievement;
		this.achievements = achievements;
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(Clicked);
		
		if (isReady)
			blocked.SetActive (false);
	}
	public void Clicked()
	{
		achievements.Selected(achievement);
	}
}
