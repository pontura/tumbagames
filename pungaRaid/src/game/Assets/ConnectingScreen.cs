using UnityEngine;
using System.Collections;

public class ConnectingScreen : MonoBehaviour {

	void Start () {
        SocialEvents.OnUserReady += OnUserReady;
        SocialEvents.OnFacebookLoginError += OnFacebookLoginError;
    }
    void OnDestroy()
    {
        SocialEvents.OnUserReady -= OnUserReady;
        SocialEvents.OnFacebookLoginError -= OnFacebookLoginError;
    }
    void OnFacebookLoginError()
    {
        Events.OnGenericPopup("Rescatada con error", "Algún guacho de facebook se ortibó en el login...");
        Data.Instance.LoadLevel("02_Intro");
    }
    void OnUserReady(string facebookID, string username, string email)
    {
        Data.Instance.LoadLevel("02_Intro");
    }
}
