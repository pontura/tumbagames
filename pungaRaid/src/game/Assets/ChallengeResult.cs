using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChallengeResult : MonoBehaviour {

    public GameObject panel;
    public Text title;

    public Text buttonField;

    public ProfilePicture profile1;
    public ProfilePicture profile2;
    public Text score1;
    public Text score2;

	void Start()
    {
        panel.SetActive(false);
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
    }
    void OnHeroDie()
    {
        //muestra el summar directamente:
        if (!SocialManager.Instance.challengeData.isOn) return;

        panel.SetActive(true);

        ChallengeData challengeData = SocialManager.Instance.challengeData;

        profile1.setPicture(SocialManager.Instance.userData.facebookID);
        profile1.setPicture(challengeData.facebookID);

        int myScore = (int)Game.Instance.gameManager.score;
        int oponent_score = (int)challengeData.score;

        score1.text  = "$" + myScore;
        score2.text  = "$" + oponent_score;

        string winner = SocialManager.Instance.userData.facebookID;
        int winnerScore = myScore;
        if(myScore >oponent_score)
        {
            title.text = "Ganaste GATO!"; 
        }
        else if(myScore < oponent_score)
        {
            title.text = "La tenes adentro!";
            winner = challengeData.facebookID;
            winnerScore = oponent_score;
        }
        else
        {
            title.text = "Empate!";
        }
        //OnChallengeClose(string objectID, string op_facebookID, string winner, float newScore)
        SocialEvents.OnChallengeClose(challengeData.objectID, challengeData.facebookID, winner, winnerScore);

    }
    public void Ready()
    {
        Data.Instance.LoadLevel("02_Main");
    }
}
