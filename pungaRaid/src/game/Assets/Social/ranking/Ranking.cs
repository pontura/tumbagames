using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Ranking : MonoBehaviour {

    public List<LevelData> levels;

    [Serializable]
    public class LevelData
    {
        public string id;
        public List<RankingData> data;
    }

    [Serializable]
    public class RankingData
    {        
        public int score;
        public string userID;
        public int profilePhotoID;
        public string username;
        public bool isYou;
    }
    void Start()
    {
        SocialEvents.OnNewHiscore += OnNewHiscore;
        SocialEvents.OnRefreshRanking += OnRefreshRanking;
    }
    void OnDestroy()
    {
        SocialEvents.OnNewHiscore -= OnNewHiscore;
        SocialEvents.OnRefreshRanking -= OnRefreshRanking;
    }
    public LevelData GetCurrentRanking()
    {
        return GetRanking(Data.Instance.moodsManager.GetCurrentMoodID(), Data.Instance.moodsManager.GetCurrentSeccional().id);
    }
    public LevelData GetRanking(int moodID, int seccionalID)
    {
        foreach (LevelData data in levels)
        {
            if (data.id == moodID + "_" + seccionalID)
                return data;
        }
        return null;
    }
    int moodID;
    int seccionalID;
    public void LoadRanking(int moodID, int seccionalID)
    {
        this.moodID = moodID;
        this.seccionalID = seccionalID;
        foreach (LevelData data in levels)
        {
            if (data.id == moodID + "_" + seccionalID)
                return;
        }
        SocialEvents.OnGetRanking(RankingReady, moodID, seccionalID);
    }

    void RankingReady(string result, int moodID, int seccionalID)
    {
        Debug.Log("RankingReady "  + moodID + " :" + seccionalID + "     :"  + result);
      //  RankingData rd = JsonUtility.FromJson<RankingData>(result);


        LevelData levelData = JsonUtility.FromJson<LevelData>(result);
        levelData.id = moodID + "_" + seccionalID;

        //string[] allData = Regex.Split(result, "</n>");
        //levelData.data = new List<RankingData>();
        //for (var i = 0; i < allData.Length - 1; i++)
        //{
        //    RankingData rankingData = new RankingData();
        //    string[] userData = allData[i].Split(":"[0]);

        //    rankingData.facebookID = userData[1];
        //    rankingData.user = userData[2];
        //    rankingData.score = int.Parse(userData[3]);
        //    levelData.data.Add(rankingData);
        //}
        levels.Add(levelData);
    }
    void OnRefreshRanking()
    {
        //int hiscore = 0;
        //bool userExistsInRanking = false;
        //foreach(RankingData rankingData in data)
        //{
        //    if (rankingData.facebookID == SocialManager.Instance.userData.facebookID)
        //    {
        //        rankingData.score = hiscore;
        //        userExistsInRanking = true;
        //        rankingData.isYou = true;
        //    }
        //}
        //if (!userExistsInRanking)
        //{
        //    RankingData rankingData = new RankingData();
        //    rankingData.facebookID =  SocialManager.Instance.userData.facebookID;
        //    rankingData.score =  hiscore;
        //    rankingData.playerName =  SocialManager.Instance.userData.username;
        //    rankingData.isYou = true;
        //    data.Add(rankingData);
        //}
        ////OrderByScore();
    }
    List<RankingData> OrderByScore(List<RankingData> rankingData)
    {
        return rankingData.OrderBy(go => go.score).Reverse().ToList();
    }
    void OnNewHiscore(int score)
    {
        if (!SocialManager.Instance.userData.logged) return;

        int moodID = Data.Instance.moodsManager.GetCurrentMoodID();
        int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional().id;
        
        foreach (LevelData levelData in levels)
        {
            if (levelData.id == moodID + "_" + seccionalID)
            {
                List<RankingData> currentLevelData = levelData.data;

                foreach (RankingData rankingData in currentLevelData)
                {
                    //if (rankingData.facebookID == SocialManager.Instance.userData.facebookID)
                    //{
                    //    rankingData.score = score;
                    //    levelData.data = OrderByScore(currentLevelData);
                    //    return;
                    //}
                }
                RankingData newData = new RankingData();
              //  newData.facebookID = SocialManager.Instance.userData.facebookID;
                newData.isYou = true;
                newData.username = SocialManager.Instance.userData.username;
                newData.score = score;
                newData.profilePhotoID = SocialManager.Instance.userData.profilePhotoID;
                currentLevelData.Add(newData);
                levelData.data = OrderByScore(currentLevelData);

            }
        }        
    }
}
