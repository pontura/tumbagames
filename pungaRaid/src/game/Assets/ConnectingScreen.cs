using UnityEngine;
using System.Collections;

public class ConnectingScreen : MonoBehaviour {

	void Start () {
        SocialEvents.OnUserReady += OnUserReady;
	  //  SocialEvents.OnFacebookLogin += OnFacebookLogin;
    }
    void OnDestroy()
    {
        SocialEvents.OnUserReady -= OnUserReady;
      //  SocialEvents.OnFacebookLogin -= OnFacebookLogin;
    }
    void OnUserReady(string facebookID, string username, string email)
    {
        Data.Instance.LoadLevel("02_Intro");
    }
}
