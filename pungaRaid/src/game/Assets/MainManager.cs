using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainManager : MonoBehaviour {

    public Text score;
    public Text username;
    public AvatarThumb avatarThumb;

    public Challenges challenges;
    public StoreManager storeManager;
    public Zones zones;
    public AchievementUI achievements;
    public Text achievementsCount;

    public Button mapButton;

	void Start () {
        Events.OnMusicChange("Raticity");

        Events.OnMoneyUpated += OnMoneyUpated;

        print(SocialManager.Instance.userData.logged);

        if (SocialManager.Instance.userData.logged)
            SocialManager.Instance.avatarImages.GetImageFor(SocialManager.Instance.userData.userID, OnLoaded);
        else
            avatarThumb.gameObject.SetActive(false);

        if (SocialManager.Instance.userData.logged)
            username.text = SocialManager.Instance.userData.username;

        SetScore(SocialManager.Instance.userHiscore.money); 

        Map();
        mapButton.Select();

        achievementsCount.text = "DELITOS: " + AchievementsManager.Instance.GetTotalReady() + "/" +  AchievementsManager.Instance.achievements.Count;
	}
    void OnLoaded(Texture2D t)
    {
        print("CARGO : " + gameObject.activeSelf + "  textura: "+  t);
        if (gameObject.activeSelf)
            avatarThumb.OnLoaded(t);
    }
    public void EditUser()
    {
        Data.Instance.LoadLevel("02_Registration");
    }
    void OnDestroy()
    {
        Events.OnMoneyUpated -= OnMoneyUpated;
    }
    void OnMoneyUpated(int newScore)
    {
        SetScore(newScore);
    }
    void SetScore(int money)
    {
        score.text = Utils.IntToMoney(money);
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
	public void OpenAlbum()
	{
		Data.Instance.LoadLevel ("10_Achivements");
	}
}
