using UnityEngine;
using System.Collections;

public class RankingMood : MonoBehaviour {

    public RankingButton button;
    public Transform container;
    public bool loaded = false;
    private int moodID;
    private int seccionalID;

    public void Init(int moodID, int seccionalID)
    {
        this.moodID = moodID;
        this.seccionalID = seccionalID+1;

        loaded = false;
        SocialManager.Instance.ranking.LoadRanking(this.moodID, this.seccionalID);
    }
	void Update () {

        if (loaded) return;

        if (SocialManager.Instance.ranking.GetRanking(moodID, seccionalID) == null) return;

        loaded = true;
        foreach (Transform childTransform in container.transform)
            Destroy(childTransform.gameObject);

        foreach (Ranking.RankingData data in SocialManager.Instance.ranking.GetRanking(moodID, seccionalID).data)
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
