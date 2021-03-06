﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public Ruleta ruleta;
    public GameObject ruletaContainer;

    public GameObject panel;

    public Text hiscoreStaticField;
    public Text yourHiscoreField;
    public Text hiscoreField;
    public Text totalScore;

	public Text barrioField;

   // public RankingUI ranking;
   // public GameObject challengeButton;

    public Image bar;
    public int totalToWin = 100000;
    private float score;
    private float total_from;
    private float total_bar_from;
    private int total_to;
    private int total_bar_to;
    private bool ready;

    public AvatarThumb avatarThumb;
    public Text winnerScoreField;
    public Text winnerNameField;
    public GameObject buttons;

    public states state;
    public enum states
    {
        OFF,
        COUNT_DOWN,
        READY
    }

    void Start()
    {
        buttons.SetActive(false);
        panel.SetActive(false);
    }
    public void Init()
    {
        panel.SetActive(true);
        print("OnPoolAllItemsInScene");
        yourHiscoreField.text = Utils.IntToMoney(SocialManager.Instance.userHiscore.GetCurrentHiscore());
        Ranking.LevelData levelData = SocialManager.Instance.ranking.GetCurrentRanking();
        if(levelData != null && levelData.data.Count>0)
        {
            Ranking.RankingData data = levelData.data[0];
            string winnerName = data.username;
            int winnerScore = data.score;

            avatarThumb.Init(data.userID, data.profilePhotoID);

            winnerNameField.text = winnerName;
            winnerScoreField.text = Utils.IntToMoney((int)winnerScore);
        }     
		barrioField.text = Data.Instance.moodsManager.GetCurrentSeccional().title;
       

        Events.OnMusicChange("Raticity");
        Events.OnPoolAllItemsInScene();
        //if (SocialManager.Instance.userData.logged)
        //{
        //    ranking.gameObject.SetActive(true);
        //    challengeButton.SetActive(false);
        //}
        //else
        //{
        //    ranking.gameObject.SetActive(false);
        //    challengeButton.SetActive(true);
        //}
        
       // panel.GetComponent<Animator>().Play("PopupOn");
        score = Game.Instance.gameManager.score;

		int moodID = Data.Instance.moodsManager.GetCurrentSeccional ().moodID;
		int seccionalID = Data.Instance.moodsManager.GetCurrentSeccional ().id;

		AchievementsEvents.OnAreaPlayed (moodID, seccionalID);


       // SendHiscore((int)score);
        string scoreToMoney = Utils.IntToMoney((int)score);
        if (score > SocialManager.Instance.userHiscore.GetCurrentHiscore())
        {
            // nuevo hiscore:
            SendHiscore((int)score);
            hiscoreStaticField.text = scoreToMoney;
            yourHiscoreField.text = scoreToMoney;
        }
        else
        {
            hiscoreStaticField.text = scoreToMoney;
        }
        total_from = SocialManager.Instance.userHiscore.money;
        total_bar_from = SocialManager.Instance.userHiscore.barProgress;

        total_to = (int)total_from + (int)score;
        total_bar_to = (int)total_bar_from + (int)score;
        SocialEvents.OnUpdateMoney((int)score);

        if (total_bar_to>totalToWin)
            SocialEvents.OnSetToTotalBarScore(0);
        else
            SocialEvents.OnSetToTotalBarScore(total_bar_to);

        state = states.COUNT_DOWN;

    }
    void Update()
    {
        if (state == states.COUNT_DOWN)
        {
            float resta = (score / 20) * (Time.deltaTime*100);
            total_from += resta;
            total_bar_from += resta;
            score -= resta;
            if (score <= 1)
            {
                state = states.READY;
                total_from = total_to;
                score = 0;
                CheckedIfCanBuy();
                hiscoreField.text = "";
            }
            if (total_bar_from > totalToWin)
            {
                total_bar_from = totalToWin;
            }
            totalScore.text = Utils.IntToMoney((int)total_from);

            if(score>0)
                hiscoreField.text = Utils.IntToMoney((int)score);
                

            bar.fillAmount = total_bar_from / totalToWin;
        }
    }
    public void SendHiscore(int score)
    {
        SocialEvents.OnNewHiscore(score);
    }
    public void Restart()
    {
       // StatsManager.TrackEvent("Summary_Restart");
        Data.Instance.LoadLevel("04_Game");
    }
    public void Map()
    {
       // StatsManager.TrackEvent("Summary_toMap");
        Data.Instance.LoadLevel("02_Main");
    }
    public void LoginAdvisor()
    {
        Events.OnLoginAdvisor();
    }
    public void Challenge()
    {
        Data.Instance.LoadLevel("08_ChallengesCreator");
    }
    void CheckedIfCanBuy()
    {
        Seccional newxtSeccionalToBuy = Data.Instance.moodsManager.GetNextSeccionalToBuy();

        if (newxtSeccionalToBuy != null && newxtSeccionalToBuy.price > 0)
        {
           // print(newxtSeccionalToBuy + " newxtSeccionalToBuy __________" + newxtSeccionalToBuy.title);
            GetComponent<BuySignal>().Init(newxtSeccionalToBuy);
        }
        else
            CheckedIfShowRuleta();
    }
    public void CheckedIfShowRuleta()
    {
        buttons.SetActive(true);
        if ((int)total_bar_from >= totalToWin)
            Invoke("AddRuleta", 0.2f);
    }
    void AddRuleta()
    {
        Ruleta ruletaNew = Instantiate(ruleta);
        ruletaNew.transform.SetParent(ruletaContainer.transform);
        ruletaNew.transform.localScale = Vector2.one;
        ruletaNew.transform.localPosition = Vector2.zero;
        ruletaNew.Init();
    }
}
