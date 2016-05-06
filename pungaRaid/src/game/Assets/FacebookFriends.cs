﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Facebook.Unity;

public class FacebookFriends : MonoBehaviour {

    [Serializable]
    public class Friend
    {
        public string id;
        public string username;
        public Texture2D picture;
    }
    public IList<string> ids;
    public List<Friend> all;

	void Start () {
        ids = new List<string>();
        SocialEvents.AddFacebookFriend += AddFacebookFriend;
        SocialEvents.OnFacebookInviteFriends += OnFacebookInviteFriends;
	}
    public void GetFriends()
    {
        //  print("GetFriendsGetFriendsGetFriendsGetFriendsGetFriendsGetFriends");
        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.API("/me?fields=id,name,friends.limit(100).fields(name,id)", Facebook.Unity.HttpMethod.GET, FBFriendsCallback);
    }
    void FBFriendsCallback(IGraphResult result)
    {
        if (result.Error != null)
        {
            Debug.LogError(result.Error);
            // Let's just try again
            FB.API("/me?fields=id,name,friends.limit(100).fields(name,id)", Facebook.Unity.HttpMethod.GET, FBFriendsCallback);
            return;
        }

        var data = Facebook.MiniJSON.Json.Deserialize(result.RawResult) as Dictionary<string, object>;
        IDictionary dict = Facebook.MiniJSON.Json.Deserialize(result.RawResult) as IDictionary;
        var friends = dict["friends"] as Dictionary<string, object>;
        System.Collections.Generic.List<object> ff = friends["data"] as System.Collections.Generic.List<object>;

        foreach (var obj in ff)
        {
            Dictionary<string, object> facebookFriendData = obj as Dictionary<string, object>;
            SocialEvents.AddFacebookFriend(facebookFriendData["id"].ToString(), facebookFriendData["name"].ToString());
        }
        print("OnFacebookFriends");
        SocialEvents.OnFacebookFriends();
    }


    public string GetUsernameByFacebookID(string _facebookID)
    {
        foreach (Friend data in all)
        {
            if (data.id == _facebookID)
                return data.id;
        }
        return "";
    }
    void OnFacebookInviteFriends()
    {
        //FB.AppRequest(
        //    "Running!", null, null, null, null, "Come and play Running!", null
        //);
    }
    void AddFacebookFriend(string id, string username)
    {
        ids.Add(id);
        Friend friend = new Friend();
        friend.id = id;
        friend.username = username;
        all.Add(friend);
        StartCoroutine(GetPicture(id));
    }
    IEnumerator GetPicture(string facebookID)
    {
        if (facebookID == "")
            yield break;

        WWW receivedData = new WWW("https" + "://graph.facebook.com/" + facebookID + "/picture?width=128&height=128");
        yield return receivedData;
        if (receivedData.error == null)
            SetProfilePicture(facebookID, receivedData.texture);
    }
    public void SetProfilePicture(string facebookID, Texture2D picture)
    {
        foreach (Friend friend in all)
        {
            if (friend.id == facebookID)
                friend.picture = picture;
        }
    }
    public Texture2D GetProfilePicture(string facebookID)
    {
        foreach (Friend friend in all)
        {
            if (friend.id == facebookID)
                return friend.picture;
        }
        return null;
    }
}
