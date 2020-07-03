using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserHiscore : MonoBehaviour {

    private string TABLE = "Ranking";
    public int money;
    public int barProgress;
    //public int hiscore;
    public bool isLoaded;
    public List<LevelScore> levelsHiscore;

    [Serializable]
    public class LevelScore
    {
        public string id;
        public int hiscore;
    }    

	void Start () {
        SocialEvents.OnNewHiscore += OnNewHiscore;
        SocialEvents.OnUpdateMoney += OnUpdateMoney;
        SocialEvents.OnSetToTotalBarScore += OnSetToTotalBarScore;
        SocialEvents.OnUserReady += OnUserReady;
        SocialEvents.ResetApp += ResetApp;

        money = PlayerPrefs.GetInt("money", 0) + 2000;
        barProgress = PlayerPrefs.GetInt("barProgress", 0);

        Invoke("LoadHiscores", 0.5f);
	}
    void LoadHiscores()
    {
        int levelID = 1;
        foreach (TextsMoods.Data data in Data.Instance.moodsManager.data.data)
        {
            int seccionalID = 1;
            foreach (Seccional seccional in data.seccional)
            {
                LevelScore levelScore = new LevelScore();
                levelScore.id = levelID + "_" + seccionalID;
                int hiscore = PlayerPrefs.GetInt("level_" + levelScore.id, 0);
                levelScore.hiscore = hiscore;
                levelsHiscore.Add(levelScore);
                seccionalID++;
            }

            levelID++;
        }
    }
    void OnUserReady(string username, string email)
    {

    }
    void OnUpdateMoney(int qty)
    {
        money += qty;
        PlayerPrefs.SetInt("money", money);
        Events.OnMoneyUpated(money);
    }
    void OnSetToTotalBarScore(int total)
    {
        barProgress = total;
        PlayerPrefs.SetInt("barProgress", total);
    }
    void ResetApp()
    {
        money = 0;
        ResetHiscores();
    }
    void ResetHiscores()
    {
        foreach(LevelScore data in levelsHiscore)
            data.hiscore = 0;
    }
    void SetHiscore(int moodID, int seccionalID, int score)
    {
        print("SetHiscore " + moodID + " seccionalID: " + seccionalID);
        PlayerPrefs.SetInt("level_" + moodID + "_" + seccionalID, score);
        foreach (LevelScore _levelsHiscore in levelsHiscore)
        {
            if (_levelsHiscore.id == moodID + "_" + seccionalID)
                _levelsHiscore.hiscore = score;
        }
    }
    public int GetCurrentHiscore()
    {
        int levelID = Data.Instance.moodsManager.GetCurrentMoodID();
        int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional().id;
      
        string str = "level_" + levelID + "_" + seccionalID;

        return PlayerPrefs.GetInt(str, 0);
    }
    public int GetHiscore(int levelID, int seccionalID)
    {
        return PlayerPrefs.GetInt("level_" + levelID + "_" + seccionalID, 0);
    }
    void OnNewHiscore(int score)
    {
        print("________OnNewHiscore" + score);

        int levelID = Data.Instance.moodsManager.GetCurrentMoodID();
        int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional().id;

        if (GetHiscore(levelID, seccionalID) < score)
        {
            SocialEvents.OnSaveNewHiscore(levelID, seccionalID, score);
            SetHiscore(levelID, seccionalID, score);
        }

        if (!SocialManager.Instance.userData.logged) return;

        //string dbID = GetLevelScore(levelID).dbID;

        //if (dbID == "")
        //    AddNewHiscore(levelID, score);
        //else
        //    UpdateScore(dbID, score);
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

        //HTTP.Request theRequest = new HTTP.Request("post", SocialManager.Instance.FIREBASE  + "/level" + levelID + ".json", data);

        //theRequest.Send((request) =>
        //{
        //    Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
        //    if (jsonObj == null)
        //    {
        //        Debug.LogError("server returned null or malformed response ):");
        //    }
        //    Debug.Log("GRABO NUVEO SCORE");
        //    SetHiscore(levelID,  score);
        //    //vuelve a levantarlo para grabar el id:
        //    LoadHiscoreFromDB(levelID);
        //});
    }
    protected void UpdateScore(string id, int score)
    {
        int levelID = 0;
       // levelID = Data.Instance.moodsManager.GetCurrentMoodID();
        print("__update score id: " + id + " score: " + score);

        Hashtable data = new Hashtable();
        data.Add("score", score);

        //HTTP.Request theRequest = new HTTP.Request("patch", SocialManager.Instance.FIREBASE + "/level" + levelID + "/" + id + "/.json", data);
        //theRequest.Send((request) =>
        //{
        //    Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
        //    if (jsonObj == null)
        //    {
        //        Debug.LogError("server returned null or malformed response ):");
        //    }
        //    Debug.Log("score updated: " + request.response.Text + " level: " + levelID + " score: " + score);
        //    SetHiscore(levelID, score);
        //});
    }
}
