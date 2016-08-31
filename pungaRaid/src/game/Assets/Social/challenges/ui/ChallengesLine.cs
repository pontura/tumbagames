using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChallengesLine : MonoBehaviour
{
    public Color lostColor;
    public Color WinColor;

    public string objectID;
    public string facebookID;
    public string username;
    public float op_score;
    public int levelId;
    public Text usernameLabel;
    public Text scoreLabel;
    public Text result;
    public ProfilePicture profilePicture;
    public int id = 0;
    private Challenges challenges;
    public bool infoLoaded;

    public Button ok;
    public Button cancel;
    public GameObject RemindButton;
    public Challenges.PlayerData playerData;

    public void Init(Challenges challenges, int _id, Challenges.PlayerData playerData)
    {
        RemindButton.SetActive(false);
        this.playerData = playerData;
        this.challenges = challenges;
        this.id = _id;
        
        if (challenges.type == Challenges.types.MADE)
        {
            InactiveButtons();
            RemindButton.SetActive(true);
        }


        if (playerData.winner != null && playerData.winner.Length > 1)
        {
            if (playerData.winner == SocialManager.Instance.userData.facebookID)
            {
                result.text = "Gantaste";
                scoreLabel.text = "$" + playerData.score;
                result.color = WinColor;
            }
            else
            {
                result.text = "Perdiste!";
                scoreLabel.text = "$" + playerData.score2;
                result.color = lostColor;
            }
            InactiveButtons();
        }
        else
        {
            op_score = playerData.score;
            scoreLabel.text = "$" + playerData.score;
        }
        this.objectID = playerData.objectID;
        this.facebookID = playerData.facebookID;

        //username = Data.Instance.gameSettings.GetUsername(playerData.playerName);
        usernameLabel.text = username;
        

        profilePicture.setPicture(facebookID);
        infoLoaded = true;
    }
    void InactiveButtons()
    {
        RemindButton.SetActive(false);
        ok.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
    }
    public void Accept()
    {
        //Data.Instance.levels.currentLevel = levelId;
        challenges.Confirm(username, objectID, facebookID, op_score);
    }
    public void Cancel()
    {
        challenges.CancelChallenge(objectID);
        Destroy(gameObject);
    }
    public void Remind()
    {
        InactiveButtons();
      //  SocialManager.OnChallengeRemind(objectID, facebookID);
    }
}
