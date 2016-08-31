using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SocialManager : MonoBehaviour
{
    const string PREFAB_PATH = "SocialManager";
    static SocialManager mInstance = null;

    [HideInInspector]
    public UserData userData;
    [HideInInspector]
    public LoginManager loginManager;
    [HideInInspector]
    public FacebookFriends facebookFriends;
    [HideInInspector]
    public Ranking ranking;
    [HideInInspector]
    public ChallengersManager challengesManager;
    [HideInInspector]
    public ChallengeData challengeData;
    [HideInInspector]
    public UserHiscore userHiscore;

    public static SocialManager Instance
    {
        get
        {
            mInstance = FindObjectOfType<SocialManager>();
            if (mInstance == null)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                mInstance = go.GetComponent<SocialManager>();
                go.transform.localPosition = new Vector3(0, 0, 0);
            }
            return mInstance;
        }
    }
    public void LoadLevel(string aLevelName)
    {
        Time.timeScale = 1;
        Application.LoadLevel(aLevelName);
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        loginManager = GetComponent<LoginManager>();
        facebookFriends = GetComponent<FacebookFriends>();
        userData = GetComponent<UserData>();
        ranking = GetComponent<Ranking>();
        challengesManager = GetComponent<ChallengersManager>();
        challengeData = GetComponent<ChallengeData>();
        userHiscore = GetComponent<UserHiscore>();
        userData.Init();

    }

    public void Reset()
    {

    }
}
