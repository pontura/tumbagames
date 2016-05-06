using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserHiscore : MonoBehaviour {

    private string TABLE = "Ranking";
    public int totalScore;
    public int barProgress;
    //public int hiscore;
    public bool isLoaded;
    public List<LevelScore> levelsHiscore;

    [Serializable]
    public class LevelScore
    {
        public int id;
        public string dbID;
        public int hiscore;
    }
    

	void Start () {
        SocialEvents.OnNewHiscore += OnNewHiscore;
        SocialEvents.OnAddToTotalScore += OnAddToTotalScore;
        SocialEvents.OnSetToTotalBarScore += OnSetToTotalBarScore;
        SocialEvents.OnFacebookLogin += OnFacebookLogin;
        SocialEvents.ResetApp += ResetApp;
        int levelID = 1; 
        foreach(LevelScore scoreData in levelsHiscore)
        {
            scoreData.hiscore = PlayerPrefs.GetInt("UserHiscoreLevel" + levelID, 0);
            levelID++;
        }
        totalScore = PlayerPrefs.GetInt("totalScore", 0);
        barProgress = PlayerPrefs.GetInt("barProgress", 0);
	}
    void OnFacebookLogin()
    {
        if (!isLoaded)
        {
            int levelID = 1;
            foreach (LevelScore scoreData in levelsHiscore)
            {
                LoadHiscoreFromDB(levelID);
                levelID++;
            }            
        }
    }
    void OnAddToTotalScore(int qty)
    {
        totalScore += qty;
        PlayerPrefs.SetInt("totalScore", totalScore);
    }
    void OnSetToTotalBarScore(int total)
    {
        barProgress = total;
        PlayerPrefs.SetInt("barProgress", total);
    }
    void ResetApp()
    {
        totalScore = 0;
        ResetHiscores();
    }
    void ResetHiscores()
    {
        foreach(LevelScore data in levelsHiscore)
            data.hiscore = 0;
    }
    void SetHiscore(int levelID, int score)
    {
        foreach (LevelScore data in levelsHiscore)
            if (data.id == levelID)
            {
                data.hiscore = score;
                PlayerPrefs.SetInt("UserHiscoreLevel" + levelID, score);
                return;
            }
    }
    void SetDB_ID(int levelID, string dbID)
    {
        foreach (LevelScore data in levelsHiscore)
            if (data.id == levelID)
            {
                data.dbID = dbID;
                return;
            }
    }
    public LevelScore GetLevelScore(int levelID)
    {
        foreach (LevelScore data in levelsHiscore)
            if (data.id == levelID)
                return data;
        return null;
    }
    public int GetHiscore()
    {
        return GetHiscore(Data.Instance.moodsManager.currentMood);
    }
    public int GetHiscore(int levelID)
    {
        foreach (LevelScore data in levelsHiscore)
            if (data.id == levelID)
                return data.hiscore;
        return 0;
    }
    void LoadHiscoreFromDB(int LevelID)
    {
        string url = SocialManager.Instance.FIREBASE + "/level" + LevelID + ".json?orderBy=\"facebookID\"&equalTo=\"" + SocialManager.Instance.userData.facebookID + "\"";

        Debug.Log(url);

        HTTP.Request someRequest = new HTTP.Request("get", url);
        someRequest.Send((request) =>
        {
            Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);

            isLoaded = true;
            if (decoded == null)
            {
                Debug.Log("no existe el user or malformed response ):");
                return;
            }
            else if (decoded.Count == 0)
            {
                SetHiscore(LevelID, 0 );
            }
            else
            {
                foreach (DictionaryEntry json in decoded)
                {
                    Hashtable jsonObj = (Hashtable)json.Value;
                    //Score s = new Score();
                    string id = (string)json.Key.ToString();
                    SetDB_ID(LevelID, id);
                    int hiscore = (int)jsonObj["score"];
                    SetHiscore(LevelID, hiscore);
                }
            }
        });
    }

    void OnNewHiscore(int score)
    {
         int levelID = Data.Instance.moodsManager.currentMood;

        if (GetHiscore(Data.Instance.moodsManager.currentMood) < score)
            SetHiscore(Data.Instance.moodsManager.currentMood, score);

        if (!SocialManager.Instance.userData.logged) return;

        string dbID = GetLevelScore(levelID).dbID;

        if (dbID == "")
            AddNewHiscore(Data.Instance.moodsManager.currentMood, score);
        else
            UpdateScore(dbID, score);
    }
    protected void AddNewHiscore(int levelID, int score)
    {
        Hashtable data = new Hashtable();
        data.Add("facebookID", SocialManager.Instance.userData.facebookID);
        data.Add("playerName", SocialManager.Instance.userData.username);
        data.Add("score", score);

        Hashtable time = new Hashtable();
        time.Add(".sv", "timestamp");
        data.Add("time", time);

        HTTP.Request theRequest = new HTTP.Request("post", SocialManager.Instance.FIREBASE  + "/level" + levelID + ".json", data);

        theRequest.Send((request) =>
        {
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (jsonObj == null)
            {
                Debug.LogError("server returned null or malformed response ):");
            }
            Debug.Log("GRABO NUVEO SCORE");
            SetHiscore(levelID,  score);
            //vuelve a levantarlo para grabar el id:
            LoadHiscoreFromDB(levelID);
        });
    }
    protected void UpdateScore(string id, int score)
    {
        int levelID = Data.Instance.moodsManager.currentMood;
        print("__update score id: " + id + " score: " + score);

        Hashtable data = new Hashtable();
        data.Add("score", score);

        HTTP.Request theRequest = new HTTP.Request("patch", SocialManager.Instance.FIREBASE + "/level" + levelID + "/" + id + "/.json", data);
        theRequest.Send((request) =>
        {
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (jsonObj == null)
            {
                Debug.LogError("server returned null or malformed response ):");
            }
            Debug.Log("score updated: " + request.response.Text + " level: " + levelID + " score: " + score);
            SetHiscore(levelID, score);
        });
    }
}
