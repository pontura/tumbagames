using UnityEngine;
using System.Collections;

public class ConnectingScreen : MonoBehaviour {

	void Start () {
	    SocialEvents.OnFacebookLogin += OnFacebookLogin;
    }
    void OnDestroy()
    {
        SocialEvents.OnFacebookLogin -= OnFacebookLogin;
    }
    void OnFacebookLogin()
    {
        Data.Instance.LoadLevel("02_Intro");
    }
}
