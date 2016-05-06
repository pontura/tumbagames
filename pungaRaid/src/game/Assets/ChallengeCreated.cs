using UnityEngine;
using System.Collections;

public class ChallengeCreated : MonoBehaviour {

    public GameObject panel;
	void Start () {
        SocialEvents.OnChallengeCreate += OnChallengeCreate;
        panel.SetActive(false);
	}
    void OnDestroy()
    {
        SocialEvents.OnChallengeCreate -= OnChallengeCreate;
    }
    void OnChallengeCreate(string facebookID, string op_facebookID, int score)
    {
        panel.SetActive(true);
        Invoke("LoopUntilReady", 1);
    }
    void LoopUntilReady()
    {
        if (SocialManager.Instance.challengesManager.made_state == ChallengersManager.state.READY)
            Ready();
        else
            Invoke("LoopUntilReady", 1);
    }
    void Ready()
    {
        panel.SetActive(false);
        Data.Instance.LoadLevel("08_ChallengesCreator");
    }
}
