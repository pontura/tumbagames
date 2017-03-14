using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SharePopup : MonoBehaviour {

	public GameObject popup;
	public Text field;
	public Image medal;
	public GameObject hideInShare;
	public GameObject newAchievement;

	void Start()
	{
		Events.OnShowAchievementSignal += OnShowAchievementSignal;
		Events.OnScreenShotReady += OnScreenShotReady;
		popup.SetActive (false);
	}
	void OnDestroy()
	{
		Events.OnShowAchievementSignal -= OnShowAchievementSignal;
		Events.OnScreenShotReady -= OnScreenShotReady;
	}
	void OnScreenShotReady()
	{
		Invoke("Close", 0.5f);
	}
	void OnShowAchievementSignal(string medalName, string _text)
	{				
		if (SceneManager.GetActiveScene ().name == "04_Game")
			newAchievement.SetActive (true);
		else
			newAchievement.SetActive (false);
		
		field.text = _text;
		popup.SetActive (true);
		hideInShare.SetActive (true);
		medal.sprite = Resources.Load("achievements/" + medalName, typeof(Sprite)) as Sprite;
	}
	public void Share()
	{
		hideInShare.SetActive (false);
		GetComponent<ShareScreenshot> ().TakeScreenshot ();
	}
	public void Close()
	{
		Events.OnCloseSharePopup ();
		popup.SetActive (false);
	}
}
