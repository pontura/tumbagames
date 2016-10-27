using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingButton : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text scoreField;
    public Text usernameField;
    public Image background;
    public Color firstColorBG;
    public GameObject coronita;

    public void Init(string facebookID, int score, string username, bool isFirst) {

        if (isFirst)
        {
            background.color = firstColorBG;
            coronita.SetActive(true);
        }
        else
            coronita.SetActive(false);

        if (scoreField)
        scoreField.text = Utils.IntToMoney(score);

        if (usernameField)
            usernameField.text = username.ToUpper();

        profilePicture.setPicture(facebookID);
	}
    public void IsYou()
    {
        if (scoreField) scoreField.color = Color.red;
        if (usernameField) usernameField.color = Color.red;
    }
}
