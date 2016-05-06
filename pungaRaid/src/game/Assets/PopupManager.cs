using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour
{

    public RankingUI rankingUI;
    public Challenges challenges;

    void Start()
    {
        Events.OnRanking += OnRanking;
        Events.OnChallenges += OnChallenges;
    }

    void OnRanking()
    {
        rankingUI.Init();
    }
    void OnChallenges()
    {
        challenges.Init();
    }
}
