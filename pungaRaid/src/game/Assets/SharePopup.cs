using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SharePopup : MonoBehaviour {

	public GameObject popup;
	public Text field;
	public Image BannerContainer;
	public GameObject button;

	void Start()
	{
		Events.OnShareNewHiscore += OnShareNewHiscore;
		Events.OnScreenShotReady += OnScreenShotReady;
		popup.SetActive (false);
	}
	void OnDestroy()
	{
		Events.OnShareNewHiscore -= OnShareNewHiscore;
		Events.OnScreenShotReady -= OnScreenShotReady;
	}
	void OnScreenShotReady()
	{
		Invoke("Close", 0.5f);
	}
	void OnShareNewHiscore(int moodID, int seccionalID, int score)
	{		
		string barrio = Data.Instance.moodsManager.data.data [moodID - 1].seccional [seccionalID - 1].title;
		string _score = Utils.IntToMoney (score);
		string fieldText = "Levanté " + _score + " laburando en " + barrio + ", vó?";
		field.text = fieldText;
		popup.SetActive (true);
		button.SetActive (true);
		BannerContainer.sprite = Resources.Load("seccionales/" + moodID + "_" + seccionalID, typeof(Sprite)) as Sprite;
	}
	public void Share()
	{
		GetComponent<ShareScreenshot> ().TakeScreenshot ();
		button.SetActive (false);
	}
	public void Close()
	{
		popup.SetActive (false);
	}
}
