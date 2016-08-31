using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour
{

    public RankingUI rankingUI;
    public Challenges challenges;
    public GenericPopup genericPopup;

    void Start()
    {
        genericPopup.gameObject.SetActive(false);

        Events.OnRanking += OnRanking;
        Events.OnChallenges += OnChallenges;
        Events.OnGenericPopup += OnGenericPopup;
    }

    void OnRanking()
    {
        rankingUI.Init();
    }
    void OnChallenges()
    {
        challenges.Init();
    }
    void OnGenericPopup(string title, string desc)
    {
        genericPopup.gameObject.SetActive(true);
        genericPopup.Init(title, desc);
    }
    public void Close()
    {
        genericPopup.gameObject.SetActive(false);
    }
}
