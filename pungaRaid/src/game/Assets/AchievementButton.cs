using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	private Image image;
	public Color inactiveColor;
	private Achievements achievements;
	private Achievement achievement;

	public void Init(Achievements achievements,  Achievement _achievement, bool ready) {
		this.achievement = _achievement;
		this.achievements = achievements;
		image = GetComponentInChildren<Image> ();

		if(!ready)
			image.color = inactiveColor;

		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(Clicked);
	}
	public void Clicked()
	{
		achievements.Selected(achievement);
	}
}
