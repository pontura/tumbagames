using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainManager : MonoBehaviour {

    public Text score;
    public Text username;
    public ProfilePicture profilePicture;

    public Challenges challenges;
    public StoreManager storeManager;
    public Zones zones;
    public AchievementUI achievements;

    public Button mapButton;

	void Start () {
        Events.OnMusicChange("Raticity");
        score.text = "$" + SocialManager.Instance.userHiscore.totalScore;

        if (SocialManager.Instance.userData.logged)
            profilePicture.setPicture(SocialManager.Instance.userData.facebookID);
        else
            profilePicture.gameObject.SetActive(false);

        if (SocialManager.Instance.userData.logged)
            username.text = SocialManager.Instance.userData.username;

        Map();
        mapButton.Select();
	}
    public void Clicked(int id)
    {
        switch(id)
        {
            case 1: Map(); break;
            case 2: Store(); break;
            case 3: Challenges(); break;
            case 4: Achievements(); break;
        }
    }
    public void Map()
    {
        Reset();
        zones.gameObject.SetActive(true);
        zones.Init();
    }
    public void Store()
    {
        if (!SocialManager.Instance.userData.logged)
        {
            Events.OnLoginAdvisor();
            return;
        }
        Reset();
        storeManager.gameObject.SetActive(true);
        storeManager.Init();
    }
	public void Challenges () {
        if (!SocialManager.Instance.userData.logged)
        {
            Events.OnLoginAdvisor();
            return;
        }
        Reset();
        challenges.gameObject.SetActive(true);
        challenges.Init();
	}
    public void Achievements()
    {
        Reset();
        achievements.gameObject.SetActive(true);
        achievements.Init();
    }
    void Reset()
    {
        zones.gameObject.SetActive(false);
        challenges.gameObject.SetActive(false);
        storeManager.gameObject.SetActive(false);
        achievements.gameObject.SetActive(false);
    }
}
