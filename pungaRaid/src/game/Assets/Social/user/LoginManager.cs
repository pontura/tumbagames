using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class LoginManager : MonoBehaviour {

    private string id;
    private string facebookID;
    private string username;

    void Awake()
    {
        Debug.Log("Login Manager AWAKE");
        FB.Init(SetInit, OnHideUnity);
        SocialEvents.OnFacebookLogout += OnFacebookLogout;
    }
    void Start()
    {
        if (!FB.IsLoggedIn)
        {
            SocialEvents.OnFacebookLoginPressed += OnFacebookLoginPressed;
        }
    }
    void OnFacebookLogout()
    {
        FB.LogOut();
    }
    void SetInit()
    {
        if (FB.IsLoggedIn) {
            Debug.Log ("FB is logged in");
            FB.API("/me?fields=name", HttpMethod.GET, LogInDone);           
        } else {
            Debug.Log ("FB is not logged in");
        }
    }
    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void OnFacebookLoginPressed()
    {
        if (FB.IsLoggedIn) return;

        List<string> permissions = new List<string>() { "public_profile", "email", "user_friends" };

        FB.LogInWithReadPermissions (permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null) {
            Debug.Log (result.Error);
        } else {
            SetInit();
        }
    }
    void LogInDone(IResult result)
    {
        facebookID = result.ResultDictionary["id"].ToString();
        username = result.ResultDictionary["name"].ToString();
        Debug.Log("facebookID: " + facebookID + " username:" + username  );
        Invoke("SetDataDelayed", 0.25f);
        Invoke("Delay", 2);
    }
    void SetDataDelayed()
    {
        SocialEvents.OnFacebookLogin(facebookID, username, "");
    }
    bool friendsLogged;
    void Delay()
    {
        if (friendsLogged) return;
        friendsLogged = true;
        SocialManager.Instance.facebookFriends.GetFriends();
    }
}
