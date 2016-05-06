using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Vs : MonoBehaviour {

    public ProfilePicture profile1;
    public ProfilePicture profile2;

    public Text username1;
    public Text username2;
    public Text score2;

	void Start () {
        Invoke("Delay", 0.1f);
        
	}
    void Delay()
    {
        profile1.setPicture(SocialManager.Instance.userData.facebookID);
        profile2.setPicture(SocialManager.Instance.challengeData.facebookID);

        username1.text = SocialManager.Instance.userData.username;
        username2.text = SocialManager.Instance.challengeData.username;

        score2.text = "$" + SocialManager.Instance.challengeData.score;

        Invoke("PreloadingGame", 3);
    }
    void PreloadingGame()
    {
        Data.Instance.LoadLevel("03_PreloadingGame");
    }
}
