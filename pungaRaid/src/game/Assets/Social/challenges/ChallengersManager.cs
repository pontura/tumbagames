using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChallengersManager : MonoBehaviour {

    public bool showFacebookFriends;

    public List<PlayerData> received;
    public List<PlayerData> made;

    public state made_state;
    public state received_state;

    public enum state
    {
        LOADING,
        REFRESHING,
        READY
    }

    [Serializable]
    public class PlayerData
    {
        public string objectID;
        public string facebookID;
        public string playerName;
        public float score;
       // public int level;

        public float score2;
        public string winner;
        public bool notificated;
    }
    void Start()
    {
        SocialEvents.OnChallengeCreate += OnChallengeCreate;
        SocialEvents.OnFacebookLogin += OnFacebookLogin;
        SocialEvents.OnChallengeClose += OnChallengeClose;
        //Invoke("LoadReceived", 1);
       // LoadMade();
    }
    void OnFacebookLogin(string facebookID, string username, string email)
    {
        LoadReceived();
        LoadMade();
    }
    public void LoadReceived()
    {
        received_state = state.LOADING;
        received.Clear();
        //string url = SocialManager.Instance.FIREBASE + "/challenges.json";
        //    //?orderBy=\"time\"&limitToLast=30";
        //url += "?orderBy=\"op_facebookID\"&equalTo=\"" + SocialManager.Instance.userData.facebookID + "\"";
        //Debug.Log("LoadReceived: " + url);
        //HTTP.Request someRequest = new HTTP.Request("get", url);
        //someRequest.Send((request) =>
        //{
        //    Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);
        //    if (decoded == null)
        //    {
        //        Debug.LogError("server returned null or     malformed response ):");
        //        return;
        //    }

        //    foreach (DictionaryEntry json in decoded)
        //    {
        //        Hashtable jsonObj = (Hashtable)json.Value;
        //        PlayerData newData = new PlayerData();
        //        newData.objectID = (string)json.Key;
        //        newData.facebookID = (string)jsonObj["facebookID"];
        //        newData.playerName = (string)jsonObj["playerName"];
        //        newData.score = (int)jsonObj["score"];
        //        newData.score2 = (int)jsonObj["score2"];
        //        newData.winner = (string)jsonObj["winner"];
        //        newData.notificated = (bool)jsonObj["notificated"];
        //        received.Add(newData);
        //    }
        //    received_state = state.READY;
        //});
    }
    public void LoadMade()
    {
        made_state = state.LOADING;
        made.Clear();
        //string url = SocialManager.Instance.FIREBASE + "/challenges.json";
        //    //?orderBy=\"time\"&limitToLast=30";
        //url += "?orderBy=\"facebookID\"&equalTo=\"" + SocialManager.Instance.userData.facebookID + "\"";

        //Debug.Log("LoadMade: " + url);
        //HTTP.Request someRequest = new HTTP.Request("get", url);
        //someRequest.Send((request) =>
        //{
        //    Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);
        //    if (decoded == null)
        //    {
        //        Debug.LogError("server returned null or     malformed response ):");
        //        return;
        //    }

        //    foreach (DictionaryEntry json in decoded)
        //    {
        //        Hashtable jsonObj = (Hashtable)json.Value;
        //        PlayerData newData = new PlayerData();
        //        newData.objectID = (string)json.Key;
        //        newData.facebookID = (string)jsonObj["op_facebookID"];
        //        newData.playerName = (string)jsonObj["op_playerName"];
        //        newData.score = (int)jsonObj["score"];
        //        newData.score2 = (int)jsonObj["score2"];
        //        newData.winner = (string)jsonObj["winner"];
        //        newData.notificated = (bool)jsonObj["notificated"];
        //        made.Add(newData);
        //    }
        //    made_state = state.READY;
        //});
    }

    //void LoadChallenge(bool _received, ParseQuery<ParseObject> query)
    //{
    //    query.FindAsync().ContinueWith(t =>
    //    {
    //        IEnumerable<ParseObject> results = t.Result;

    //      //  if (_received && loaded_received) return;

    //        if (!_received) 
    //        {
    //           if( made_state == state.READY) return;
    //            made.Clear();
    //        }

    //        foreach (var result in results)
    //        {
    //            string objectID = result.ObjectId;
    //            string facebookID = result.Get<string>("facebookID");
    //            string op_facebookID = result.Get<string>("op_facebookID");
    //            string op_playerName = result.Get<string>("op_playerName");
    //            string playerName = result.Get<string>("playerName");
    //            bool notificated = result.Get<bool>("notificated");
                
    //            int level = result.Get<int>("level");
    //            float score = result.Get<float>("score");

    //            float score2 = 0;
    //            string winner = "";
    //            try
    //            {
    //                score2 = result.Get<float>("score2");
    //                winner = result.Get<string>("winner");
    //            }
    //            catch
    //            {
    //            }
    //            PlayerData playerData = new PlayerData();
    //            playerData.objectID = objectID;
    //            playerData.facebookID = facebookID;
    //            playerData.playerName = playerName;
    //            playerData.notificated = notificated;

    //            if (!_received)
    //            {
    //                playerData.facebookID = op_facebookID;
    //                playerData.playerName = op_playerName;
    //            }

    //            playerData.score = score;
    //            playerData.level = level;

    //            playerData.winner = winner;
    //            playerData.score2 = score2;

    //            if (_received)
    //            {
    //                received.Add(playerData);
    //               // loaded_received = true;
    //            }
    //            else
    //            {
    //                made.Add(playerData);
    //                made_state = state.READY;
    //            }
    //        }
    //    }
    //    );
    //}
    void OnChallengeCreate(string oponent_username, string oponent_facebookID, int score)
    {
        made_state = state.REFRESHING;
        Hashtable data = new Hashtable();
        data.Add("facebookID", SocialManager.Instance.userData.facebookID);
        data.Add("playerName", SocialManager.Instance.userData.username);

        data.Add("op_playerName", oponent_username);
        data.Add("op_facebookID", oponent_facebookID);
        data.Add("score", score);
        data.Add("score2", 0);
        data.Add("notificated", false);

        Hashtable time = new Hashtable();
        time.Add(".sv", "timestamp");
        data.Add("time", time);

        //HTTP.Request theRequest = new HTTP.Request("post", SocialManager.Instance.FIREBASE + "/challenges.json", data);

        //theRequest.Send((request) =>
        //{
        //    Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
        //    if (jsonObj == null)
        //    {
        //        Debug.LogError("server returned null or malformed response ):");
        //    }
        //    Debug.Log("__OnChallengeCreated!");
        //    LoadMade();
        //});
    }
    public void OnChallengeClose(string objectID, string op_facebookID, string winner, float newScore)
    {
        print("OnChallengeClose objectID " + objectID + " winner: " + winner);

        Hashtable data = new Hashtable();
        data.Add("winner", winner);
        data.Add("score2", newScore);

        //HTTP.Request theRequest = new HTTP.Request("patch", SocialManager.Instance.FIREBASE + "/challenges/" + objectID + "/.json", data);
        //theRequest.Send((request) =>
        //{
        //    Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
        //    if (jsonObj == null)
        //    {
        //        Debug.LogError("server returned null or malformed response ):");
        //    }
        //    Debug.Log("challenge updated: " + request.response.Text);
        //});
    }
    public int GetNewChallenges()
    {
        int total = 0;
        foreach(PlayerData data in received)
        {
            if (data.winner.Length == 0)
                total++;
        }
        return total;
    }
}
