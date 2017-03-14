using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	private Image image;
	public Color inactiveColor;
	private Achievements achievements;
	private Achievement achievement;
	private bool isReady;

	public void Init(Achievements achievements,  Achievement _achievement, bool ready) {
		this.achievement = _achievement;
		this.achievements = achievements;
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(Clicked);

		if (isReady)
			return;

		image = GetComponentInChildren<Image> ();
		if (ready)
			isReady = true;
		else
			image.color = inactiveColor;
	}
	public void Clicked()
	{
		achievements.Selected(achievement);
	}
}
