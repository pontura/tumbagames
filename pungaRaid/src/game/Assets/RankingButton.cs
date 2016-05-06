using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingButton : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text scoreField;
    public Text usernameField;

	public void Init (string facebookID, int score, string username) {

        if (scoreField)
        scoreField.text = "$" + score.ToString();

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
