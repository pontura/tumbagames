using UnityEngine;
using System.Collections;

public class RankingUI : MonoBehaviour {

    public RankingButton button;
    public Transform container;
    public bool loaded = false;

    public void Init()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
	void Update () {
        if (loaded) return;

        if (!SocialManager.Instance.userHiscore.isLoaded) return;

        if (SocialManager.Instance.facebookFriends.all.Count==0) return;

        if (SocialManager.Instance.ranking.data.Count > 0)
        {
            SocialEvents.OnRefreshRanking();
            loaded = true;
            foreach(Ranking.RankingData data in  SocialManager.Instance.ranking.data)
            {
                RankingButton newButton = Instantiate(button);
               
                newButton.transform.SetParent(container.transform);
                newButton.Init(data.facebookID, data.score, data.playerName);

                newButton.transform.localScale = Vector2.one;

                if (data.isYou)
                    newButton.IsYou();
            }
        }
	}
    public void Back()
    {
        Events.OnMusicVolumeChanged(1);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
