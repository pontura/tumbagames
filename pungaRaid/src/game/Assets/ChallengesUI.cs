using UnityEngine;
using System.Collections;

public class ChallengesUI : MonoBehaviour {

    public ChallengeButton button;
    public Transform container;
	public bool loaded;

	void Update () {
        if (loaded) return;
        if (SocialManager.Instance.ranking.data.Count > 0)
        {
            loaded = true;
            foreach(Ranking.RankingData data in  SocialManager.Instance.ranking.data)
            {
                ChallengeButton newButton = Instantiate(button);
                newButton.transform.SetParent(container.transform);
                newButton.Init(this, data.facebookID, data.playerName, false);
            }
        }
	}
}
