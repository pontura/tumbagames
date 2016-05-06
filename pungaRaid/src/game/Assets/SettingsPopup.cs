using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopup : MonoBehaviour {

    public Text LoginField;
    public Text musicLabel;
    public Text sfxLabel;

    public GameObject panel;
    private GraphicRaycaster graphicRaycaster;
    private float timescale = 1;

	void Start () {
     //   panel.transform.localScale = Data.Instance.screenManager.scale;

        
        panel.SetActive(false);
        Events.OnSettings += OnSettings;
        SocialEvents.OnFacebookNotConnected += OnFacebookNotConnected;

        SetMusicLabel();
        SetSFXLabel();
        
	}
    void OnDestroy()
    {
        Events.OnSettings -= OnSettings;
        SocialEvents.OnFacebookNotConnected -= OnFacebookNotConnected;
    }
    void OnSettings()
    {
       // SetLoginField(FB.IsLoggedIn);
        if(!Data.Instance.musicManager.disabled)
          Events.OnMusicVolumeChanged(0.2f);

        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        graphicRaycaster.enabled = true;

        panel.SetActive(true);
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOn",0,0);
        timescale = Time.timeScale;
        Time.timeScale = 0;

    }
    public void Tutorial()
    {
        PlayerPrefs.SetString("tutorialReady", "false");

        if(Application.loadedLevelName == "04_Game")
            Game.Instance.gameManager.RePlayTutorial();
        else
            Data.Instance.LoadLevel("04_Game");
    }
    public void FBLoginInOut()
    {
        //if (!FB.IsLoggedIn)
        //{
            OnFacebookNotConnected();
            SetLoginField(false);
        //}
        //else
        //{
        //    Data.Instance.loginManager.ParseFBLogout();
        //    SetLoginField(true);
        //}
    }
    void SetLoginField(bool conected)
    {
        if (conected)
            LoginField.text = "Desconectarte";
        else 
            LoginField.text = "Registrate!";
    }
    public void OnFacebookNotConnected()
    {
        print("FB: OnFacebookNotConnected");
        CloseOff();
        Events.OnLoginAdvisor();
    }
    public void Close()
    {
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOff", 0, 0);
        Invoke("CloseOff", 0.2f);
        Time.timeScale = timescale;
        Events.OnMusicVolumeChanged(1);
    }
    void CloseOff()
    {
        //graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
       // graphicRaycaster.enabled = false;
        panel.SetActive(false);
    }
    public void Challenges()
    {
        CloseOff();
        if (SocialManager.Instance.userData.logged)
            Events.OnChallenges();
        else
            Events.OnLoginAdvisor();
        
    }
    public void Ranking()
    {
        CloseOff();
        if (SocialManager.Instance.userData.logged)
            Events.OnRanking();
        else
            Events.OnLoginAdvisor();
        
    }
    public void Reset()
    {
        SocialEvents.ResetApp();
        PlayerPrefs.DeleteAll();        
        Application.LoadLevel("01_Splash");
        CloseOff();
        Time.timeScale = 1;
        Events.OnMusicVolumeChanged(1);
        Events.OnMusicChange("");
    }
    public void SwitchMusic()
    {
        if (Data.Instance.musicManager.volume == 0)
            Data.Instance.musicManager.OnMusicOff(false);
        else
            Data.Instance.musicManager.OnMusicOff(true);

        SetMusicLabel();
    }
    public void SwitchSFX()
    {
        float vol = 1;
        if (Data.Instance.soundManager.volume == 0)
            vol = 1;
        else
            vol = 0;

        Events.OnSoundsVolumeChanged(vol);

        SetSFXLabel();
    }
    void SetMusicLabel()
    {
        if(Data.Instance.musicManager.disabled)
            musicLabel.text = "Música ON";
        else 
            musicLabel.text = "Música OFF";

    }
    void SetSFXLabel()
    {
        if (Data.Instance.soundManager.volume == 0)
            sfxLabel.text = "SFX ON";
        else
            sfxLabel.text = "SFX OFF";

    }


}
