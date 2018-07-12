using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecordSignal : MonoBehaviour {

	public GameObject button;

	void Start () {
		button.SetActive (false);

		if (Data.Instance.isArcade)
			return;
		
		Events.OnHeroDie += OnHeroDie;
	}
	void OnDestroy () {
		Events.OnHeroDie -= OnHeroDie;
	}
	void OnHeroDie () {
		
		if(PlayerPrefs.GetString("tutorialReady") !=  "true") 
			return;
		 
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

		string medalName = ""; 
		if (moodID == 1 && seccionalID == 1)
			medalName = "achivements_RECAMIER_1";
		else if (moodID == 1 && seccionalID == 2)
			medalName = "achivements_NORCO_1";
		else if (moodID == 1 && seccionalID == 3)
			medalName = "achivements_ZABECA_1";
		else if (moodID == 2 && seccionalID == 1)
			medalName = "achivements_MAMERTO_1";
		else if (moodID == 2 && seccionalID == 2)
			medalName = "achivements_DILDO_1";
		else if (moodID == 2 && seccionalID == 3)
			medalName = "achivements_PUERTO_1";
		
		string barrio = Data.Instance.moodsManager.data.data [moodID - 1].seccional [seccionalID - 1].title;
		string _score = Utils.IntToMoney (score);
		string fieldText = "Levanté " + _score + " laburando en " + barrio + ", vó?";


		Events.OnShowAchievementSignal (medalName, fieldText, true);
	}
}
