using System;
using System.Collections.Generic;
using UnityEngine;
using Facebook;


public class FacebookShare : MonoBehaviour {

    string linkName = "PungaRaid";
    private string picture_URL = "http://tipitap.com/running-icon.jpg";


    public void ShareToFriend(string friend_facebookID, string linkCaption)
    {

  
    }

    public void MultiplayerHiscore(string score)
    {

    }

    //public void WinChallengeTo(string challenge_username)
    //{
    //    if (FB.IsLoggedIn)
    //    {
    //        string  linkCaption = "You Won the challenge to " + challenge_username;

    //        FB.Feed(
    //            linkCaption: linkCaption,
    //            //  picture: "<INSERT A LINK TO A PICTURE HERE>",
    //            linkName: linkName,
    //            link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest")
    //            );
    //    }
    //}
    //public void LostChallengeTo(string challenge_username)
    //{
    //    if (FB.IsLoggedIn)
    //    {
    //        string linkCaption = "You lost the challenge with " + challenge_username;

    //        FB.Feed(
    //            linkCaption: linkCaption,
    //            //  picture: "<INSERT A LINK TO A PICTURE HERE>",
    //            linkName: linkName,
    //            link: "http://apps.facebook.com/" + FB.AppId + "/?challenge_brag=" + (FB.IsLoggedIn ? FB.UserId : "guest")
    //            );
    //    }
    //}
    public void NewHiscore(string linkCaption)
    {

    }
}
