using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

	private Image image;
	public Color inactiveColor;
	private Achievements achievements;
	private int id;

	public void Init(Achievements achievements,  int id, bool ready) {
		this.id = id;
		this.achievements = achievements;
		image = GetComponentInChildren<Image> ();

		if(!ready)
			image.color = inactiveColor;
	}
	public void Clicked()
	{
		achievements.Selected(id);
	}
}
