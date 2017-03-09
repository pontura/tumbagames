using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecordSignal : MonoBehaviour {

	public GameObject button;

	void Start () {
		button.SetActive (false);
		Events.OnHeroDie += OnHeroDie;
	}
	void OnDestroy () {
		Events.OnHeroDie -= OnHeroDie;
	}
	void OnHeroDie () {
		int score = Game.Instance.gameManager.score;
		int hiscore = SocialManager.Instance.userHiscore.GetCurrentHiscore();
		if(score>hiscore)
			button.SetActive (true);
	}
	public void Clicked()
	{
		int score = Game.Instance.gameManager.score;
		int moodID = Data.Instance.moodsManager.GetCurrentMoodID ();
		int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional ().id;
		Events.OnShareNewHiscore (moodID, seccionalID, score);
	}
}
