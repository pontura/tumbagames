using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using Soomla.Store;


public class UserData : MonoBehaviour {

    public bool logged;
    public string facebookID;
    public string username;

    void Start()
    {
        SocialEvents.ResetApp += ResetApp;
    }
    void ResetApp()
    {
        username = facebookID = "";
        logged = false;
    }
	public void Init () {

        SocialEvents.OnUserReady += OnUserReady;
#if UNITY_EDITOR
        SetUser("", "");
        return;
#endif
        if (PlayerPrefs.GetString("username") != "" && PlayerPrefs.GetString("facebookID") != "")
            SetUser(PlayerPrefs.GetString("username"), PlayerPrefs.GetString("facebookID"));

	}
    void SetUser(string username, string facebookID)
    {
        this.facebookID = facebookID;
        this.username = username;
        if (username.Length > 1)
        {
            print("::::::::::::::" + username + " logged: " + logged);
            logged = true;
        }
        SocialEvents.OnFacebookLogin();
    }
    public void OnUserReady(string username, string facebookID)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("facebookID", facebookID);
        SetUser(username, facebookID);
    }
    public void Reset()
    {
        logged = false;
        facebookID = "";
        username = "";
    }  
}
