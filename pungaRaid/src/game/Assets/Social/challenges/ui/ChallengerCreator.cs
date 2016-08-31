using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ChallengerCreator : MonoBehaviour {

    public Text scoreField;
    public string facebookFriendName;
    public string facebookFriendId;
    private int levelId;
    private float score;

    public string lastSelectedFacebookId;

    bool filterReady;

    public GameObject container;

    [SerializeField]
    ChallengerCreatorButton button;

    void Start()
    {
       // Init(1, SocialManager.Instance.userHiscore.GetHiscore());
    }
    void Init(int levelId, int myScore)
    {
        foreach (Transform childTransform in container.transform)
            Destroy(childTransform.gameObject);
        scoreField.text = "Retá a algún gato por $" + myScore;

        CreateList();
    }
    public void InviteFriends()
    {
       // Events.OnFacebookInviteFriends();
    }
    public void CreateList()
    {
        foreach ( FacebookFriends.Friend data in SocialManager.Instance.facebookFriends.all)
        {
            ChallengerCreatorButton newButton = Instantiate(button) as ChallengerCreatorButton;
            newButton.transform.SetParent(container.transform);

            string facebookID = data.id;
            bool done = false;

            //si es un recien elegido...
            if (facebookID == lastSelectedFacebookId)
                done = true;
            else
            {
                foreach (ChallengersManager.PlayerData challengesMadeFBId in SocialManager.Instance.challengesManager.made)
                {
                    if (challengesMadeFBId.facebookID == facebookID)
                        done = true;
                }
            }
            newButton.Init(this, data.username, facebookID, done);
        }
    }


    public void Back()
    {
        Data.Instance.LoadLevel("04_Game");
    }
    public void Challenge(string _username, string _facebookID)
    {
       // SocialEvents.OnChallengeCreate(_username, _facebookID,  SocialManager.Instance.userHiscore.GetHiscore());
        lastSelectedFacebookId = facebookFriendId;
    }
    
}
