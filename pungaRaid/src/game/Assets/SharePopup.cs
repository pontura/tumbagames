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
	public Animator anim;

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
	void OnShowAchievementSignal(string medalName, string _text)
	{				
		if (SceneManager.GetActiveScene ().name == "04_Game") {
			Events.OnSoundFX("PowerUpItem");
			anim.Play ("achievementWonInGame");
			if(Data.Instance.musicManager.volume==1)
				Events.OnMusicVolumeChanged(0.2f);
		} else {
			anim.Play ("achievementPopup");
		}
		
		field.text = _text;
		popup.SetActive (true);
		hideInShare.SetActive (true);
		medal.sprite = Resources.Load("achievements/" + medalName, typeof(Sprite)) as Sprite;
	}
	public void Share()
	{
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
