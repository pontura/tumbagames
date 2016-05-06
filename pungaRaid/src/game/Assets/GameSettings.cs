using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class GameSettings : MonoBehaviour  {

    public void Login()
    {
        SocialEvents.OnFacebookNotConnected();
    }
    public string GetUsername(string _username)
    {
        _username = _username.ToUpper();
        string[] nameArr = Regex.Split(_username, " ");
        if (nameArr.Length > 0)
        {
            string firstLetter = "";
            if(nameArr.Length>1 && nameArr[1].Length > 0)
                 firstLetter = nameArr[1].Substring(0, 1);
            return nameArr[0] + " " + firstLetter;
        }
        else return _username;
    }
}
