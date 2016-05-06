using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Summary : MonoBehaviour {

    public Ruleta ruleta;
    public GameObject ruletaContainer;

    public GameObject panel;
    public Text hiscoreStaticField;
    public Text hiscoreField;
    public Text totalScore;
    public RankingUI ranking;
    public GameObject challengeButton;

    public Image bar;
    public int totalToWin = 100000;
    private float score;
    private float total_from;
    private float total_bar_from;
    private int total_to;
    private int total_bar_to;
    private bool ready;

    public states state;
    public enum states
    {
        OFF,
        COUNT_DOWN,
        READY
    }

    void Start()
    {
        panel.SetActive(false);
    }
    public void Init()
    {
        Events.OnMusicChange("Raticity");
        Events.OnPoolAllItemsInScene();
        if (SocialManager.Instance.userData.logged)
        {
            ranking.gameObject.SetActive(true);
            challengeButton.SetActive(false);
        }
        else
        {
            ranking.gameObject.SetActive(false);
            challengeButton.SetActive(true);
        }
        panel.SetActive(true);
       // panel.GetComponent<Animator>().Play("PopupOn");
        score = Game.Instance.gameManager.score;
        if (score > SocialManager.Instance.userHiscore.GetHiscore())
        {
            SendHiscore((int)score);
            hiscoreStaticField.text = "NUEVO RECORD! $" + score;
        }
        else
        {
            hiscoreStaticField.text = "Hicites $" + score;
        }
        total_from = SocialManager.Instance.userHiscore.totalScore;
        total_bar_from = SocialManager.Instance.userHiscore.barProgress;
        total_to = (int)total_from + (int)score;
        total_bar_to = (int)total_bar_from + (int)score;
        SocialEvents.OnAddToTotalScore((int)score);

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
                CheckedIfShowRuleta();
            }
            if (total_bar_from > totalToWin)
            {
                total_bar_from = totalToWin;
            }
            totalScore.text = "$" + (int)total_from;
            hiscoreField.text = "$" + (int)score;

            bar.fillAmount = total_bar_from / totalToWin;
        }
    }
    public void SendHiscore(int distance)
    {
        SocialEvents.OnNewHiscore(distance);
    }
    public void Restart()
    {
        Data.Instance.LoadLevel("04_Game");
    }
    public void Map()
    {
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
    void CheckedIfShowRuleta()
    {
        if ((int)total_bar_from == totalToWin)
            AddRuleta();
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
