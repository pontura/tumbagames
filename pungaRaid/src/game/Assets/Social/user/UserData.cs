using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
        {
            SetUser(PlayerPrefs.GetString("username"), PlayerPrefs.GetString("facebookID"));
            SocialManager.Instance.GetComponent<DataController>().LoadDataForExistingUser(facebookID);
        }

	}
    void SetUser(string username, string facebookID)
    {
        this.facebookID = facebookID;
        this.username = username;
        if (username.Length > 1)
        {
            print("UserData SetUser ::::::::::::::" + username + " logged: " + logged);
            logged = true;
        }
    }
    public void OnUserReady(string facebookID, string username, string nick)
    {
        PlayerPrefs.SetString("facebookID", facebookID);
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("nick", nick);
        SetUser(username, facebookID);
    }
    public void Reset()
    {
        logged = false;
        facebookID = "";
        username = "";
    }  
}
