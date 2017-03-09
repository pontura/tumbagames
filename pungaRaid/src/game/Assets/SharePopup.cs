using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SharePopup : MonoBehaviour {

	public GameObject popup;
	public Text field;
	public Image medal;
	public GameObject button;
	public GameObject closeButton;

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
		field.text = _text;
		popup.SetActive (true);
		button.SetActive (true);
		closeButton.SetActive (true);
		medal.sprite = Resources.Load("achievements/" + medalName, typeof(Sprite)) as Sprite;
	}
	public void Share()
	{
		button.SetActive (false);
		closeButton.SetActive (false);
		GetComponent<ShareScreenshot> ().TakeScreenshot ();
	}
	public void Close()
	{
		popup.SetActive (false);
	}
}
