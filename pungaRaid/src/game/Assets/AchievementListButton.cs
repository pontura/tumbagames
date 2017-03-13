using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementListButton : MonoBehaviour {

	public GameObject doneGO;
	public Text field;
	private AchievementListsPopup popup;

	public void Init(AchievementListsPopup popup,  Achievement ach)
	{
		this.popup = popup;
		this.field.text = ach.title;
		if (ach.ready)
			doneGO.SetActive (true);
		else
			doneGO.SetActive (false);
	}
}
