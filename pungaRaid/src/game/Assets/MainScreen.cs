using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

    public RankingUI ranking;
    public GameObject connect;

    void Start()
    {
        Events.OnMusicChange("Menu");

        if (SocialManager.Instance.userData.logged)
        {
            ranking.gameObject.SetActive(true);
            connect.gameObject.SetActive(false);
        }
        else
        {
            ranking.gameObject.SetActive(false);
            connect.gameObject.SetActive(true);
        }
    }
}
