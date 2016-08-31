using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Challenges : MonoBehaviour {

    [Serializable]
    public class PlayerData
    {
        public string objectID;
        public string facebookID;
        public string playerName;        
        public float score;
        public int level;

        public float score2;
        public string winner;
        
    }
    public types type;
    public enum types
    {
        RECEIVED,
        MADE        
    }
    public GameObject container;

    [SerializeField]
    ChallengesLine challengesLine;

    private int buttonsSeparation = 89;
    public bool infoLoaded;

    public void Init()
    {
        gameObject.SetActive(true);
    }
    void Update()
    {
        if (infoLoaded) return;
        if (SocialManager.Instance == null) return;

        if(type == types.RECEIVED && SocialManager.Instance.challengesManager.received.Count > 0)
            ChallengesReceived();
        if (type == types.MADE && SocialManager.Instance.challengesManager.made.Count > 0)
            ChallengesMade();
    }
    public void Back()
    {
        gameObject.SetActive(false);
    }
    public void Switch()
    {
        if (type == types.MADE)
            ChallengesReceived();
        else
            ChallengesMade();
    }
    public void ChallengesMade()
    {
        infoLoaded = true;
        type = types.MADE;
        LoadData();
    }
    public void ChallengesReceived()
    {
        infoLoaded = true;
        type = types.RECEIVED;
        LoadData();
    }
    private void LoadData()
    {
        foreach (Transform childTransform in container.transform)
        {
            Destroy(childTransform.gameObject);
        }
        
        if (type == types.RECEIVED)
        {
            LoadChallenge(SocialManager.Instance.challengesManager.received);
        }
        else
        {
            LoadChallenge(SocialManager.Instance.challengesManager.made);
        }
    }
    void LoadChallenge(List<ChallengersManager.PlayerData> data)
    {
        int a = 0;
        foreach (ChallengersManager.PlayerData result in data)
        {

            string objectID = result.objectID;
            string facebookID = result.facebookID;
            string op_playerName = result.playerName;
            string playerName = result.playerName;        
            float score = result.score;
            float score2 = result.score2;
            string winner = result.winner;

            PlayerData playerData = new PlayerData();
            playerData.objectID = objectID;
            playerData.facebookID = facebookID;

            if (type == types.MADE)
                playerData.playerName = op_playerName;
            else
                playerData.playerName = playerName;

            playerData.score = score;
           // playerData.level = level;
            playerData.winner = winner;

            playerData.score2 = score2;

            ChallengesLine newButton = Instantiate(challengesLine) as ChallengesLine;
            newButton.transform.SetParent(container.transform);
            newButton.transform.localScale = Vector3.one;
            newButton.Init(this, a, playerData);
            a++;
        }
       
    }
    
    public void Confirm(string username, string objectID, string facebookID, float op_score)
    {
        SocialEvents.OnChallengeConfirm(objectID, username, facebookID, op_score);
    }
    public void CancelChallenge(string objectID)
    {
        SocialEvents.OnChallengeDelete(objectID);
    }
}
