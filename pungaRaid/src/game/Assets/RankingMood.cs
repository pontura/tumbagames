using UnityEngine;
using System.Collections;

public class RankingMood : MonoBehaviour {

    public RankingButton button;
    public Transform container;
    public bool loaded = false;
    public int levelID;

    public void Init(int levelID)
    {
        this.levelID = levelID;
        loaded = false;
    }
	void Update () {

        if (levelID < 1) return;
        if (loaded) return;

        //if (!SocialManager.Instance.userHiscore.isLoaded) return;

        //if (SocialManager.Instance.facebookFriends.all.Count==0) return;

        if (SocialManager.Instance.ranking.levels[levelID-1].data.Count > 0)
        {
            //SocialEvents.OnRefreshRanking();
            loaded = true;
            foreach (Transform childTransform in container.transform)
                Destroy(childTransform.gameObject);

            foreach (Ranking.RankingData data in SocialManager.Instance.ranking.levels[levelID - 1].data)
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
}
