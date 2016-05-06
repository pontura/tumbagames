using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginAdvisor : MonoBehaviour {

    public GameObject panel;
    private GraphicRaycaster graphicRaycaster;

	void Start () {
     //   panel.transform.localScale = Data.Instance.screenManager.scale;
        panel.SetActive(false);
        Events.OnLoginAdvisor += OnLoginAdvisor;
        SocialEvents.OnFacebookLoginCanceled += OnFacebookLoginCanceled;
	}
    void OnLoginAdvisor()
    {
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        graphicRaycaster.enabled = true;

        panel.SetActive(true);
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOn");
        
    }
    public void LoginToFacebook()
    {        
        SocialManager.Instance.loginManager.FBLogin();
        Data.Instance.LoadLevel("03_Connecting");
        Close();
        
    }
    public void Close()
    {
        panel.GetComponent<Animator>().Play("PopupOff");
        Invoke("CloseOff", 0.2f);
        Time.timeScale = 1;
        Events.OnMusicVolumeChanged(1);
    }
    void CloseOff()
    {
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        panel.SetActive(false);
    }
    void OnFacebookLoginCanceled()
    {
        Close();
        Data.Instance.LoadLevel("02_Intro");
    }
}
