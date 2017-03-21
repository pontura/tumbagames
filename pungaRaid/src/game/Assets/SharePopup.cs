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
	public GameObject logos;
	public Animator anim;
	public GameObject PlayButton;
	public GameObject shareButton;

	void Start()
	{
		Events.OnShowAchievementSignal += OnShowAchievementSignal;
		Events.OnScreenShotReady += OnScreenShotReady;
		popup.SetActive (false);
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
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
	void OnShowAchievementSignal(string medalName, string _text, bool isReady)
	{				
		Vector3 pos = PlayButton.transform.localPosition;
		if (isReady) {
			shareButton.SetActive (true);
			pos.x = 110; 
		} else {
			shareButton.SetActive (false);
			pos.x = 0;
		}
		PlayButton.transform.localPosition = pos;

		if (SceneManager.GetActiveScene ().name == "04_Game") {
			logos.SetActive (true);
			newAchievement.SetActive (true);
			Events.OnSoundFX("PowerUpItem");
			anim.Play ("achievementWonInGame");
			if(Data.Instance.musicManager.volume==1)
				Events.OnMusicVolumeChanged(0.2f);
		} else {
			logos.SetActive (false);
			newAchievement.SetActive (false);
			anim.Play ("achievementPopup");
		}
		
		field.text = _text;
		popup.SetActive (true);
		hideInShare.SetActive (true);
		medal.sprite = Resources.Load("achievements/" + medalName, typeof(Sprite)) as Sprite;
	}
	public void Share()
	{
		logos.SetActive (true);
		hideInShare.SetActive (false);
		Invoke ("ReOpen", 1);
		GetComponent<ShareScreenshot> ().TakeScreenshot ();
	}
	void ReOpen()
	{
		hideInShare.SetActive (true);
	}
	public void Close()
	{
		Events.OnCloseSharePopup ();
		popup.SetActive (false);
	}
}
