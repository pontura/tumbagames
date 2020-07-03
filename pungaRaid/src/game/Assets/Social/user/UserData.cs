using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public string URL = "http://pontura.com/punga_raid/";
    public string setUserURL = "setUser.php";
    public string setUserURLUpload = "updateUser.php";
    public string imageURLUploader = "uploadPhoto.php";
    public string setUserDataURL = "setUserData.php";
    public string imagesURL = "users/";

    const string PREFAB_PATH = "UserData";
    public string userID;
    public string facebookID;
    public int profilePhotoID;
    public string username;
    [SerializeField] private int score;
    [SerializeField] private int lastScoreWon; //solo para hacer la animacion en el levelSelector

    public string path;
    public HiscoresByMissions hiscoresByMissions;
    public AvatarImages avatarImages;
    public ServerConnect serverConnect;
    public int playerID;
    public bool logged;

    System.Action OnReady;

    void Awake()
    {
        path = Application.persistentDataPath + "/";
      
        serverConnect = GetComponent<ServerConnect>();
        avatarImages = GetComponent<AvatarImages>();
        hiscoresByMissions = GetComponent<HiscoresByMissions>();
        score = PlayerPrefs.GetInt("score");
    }
   
    public void Init(System.Action OnReady)
    {
        this.OnReady = OnReady;
        LoadUser();
        hiscoresByMissions.Init();

        //Data.Instance.events.OnSaveScore += OnSaveScore;
    }
    void LoadUser()
    {
        playerID = PlayerPrefs.GetInt("playerID");
        userID = PlayerPrefs.GetString("userID");

        if (userID.Length<2)
        {
#if UNITY_ANDROID
			userID = SystemInfo.deviceUniqueIdentifier;
			SetUserID(userID);            
#else
            userID = SetRandomID();
            SetUserID(userID);
#endif
            logged = false;
        } else
        {
            logged = true;
            userID = PlayerPrefs.GetString("userID");
            username = PlayerPrefs.GetString("username");
            avatarImages.GetImageFor(userID, null);
        }
        Debug.Log("userID " + userID + " logged: " + logged);
        serverConnect.LoadUserData(userID, OnLoaded);
    }
    string SetRandomID()
    {
        string value = "";
#if UNITY_WEBGL
        value += "web_";
#else
        value += "exe_";
#endif

        for (int a= 0; a<20; a++)
        {
            value += Random.Range(0, 9).ToString();
        }
        return value;
    }
    void OnLoaded(ServerConnect.UserDataInServer dataLoaded)
    {

        if (dataLoaded != null && dataLoaded.username != "")
        {
            logged = true;
            userID = dataLoaded.userID;
            username = dataLoaded.username;
            score = dataLoaded.score;
            profilePhotoID = dataLoaded.profilePhotoID;
            int level_1_1 = dataLoaded.level_1_1;
            int level_1_2 = dataLoaded.level_1_2;
            int level_1_3 = dataLoaded.level_1_3;
            int level_2_1 = dataLoaded.level_2_1;
            int level_2_2 = dataLoaded.level_2_2;
            int level_2_3 = dataLoaded.level_2_3;
            Debug.Log("User data Ready userID: " + userID + "   username: " + username);
        }        
        OnReady();
    }
    public bool IsLogged()
    {
        if (logged)
            return true;
        else
            return false;
    }
    public void SetUserID(string userID)
    {
        this.userID = userID;
        //PlayerPrefs.SetString("userID", userID);
    }

    public void UserCreation()
    {
        logged = true;
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("userID", userID);
    }
    private Sprite LoadSprite(string path)
    {
        Debug.Log("Busca imagen en: " + path);
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            Debug.Log("Image exists in local");
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(300, 300);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }
    public void UpdateData()
    {
        print("UpdateData");
      //  Data.Instance.serverManager.LoadUserData(userID);
    }
   
    public int Score()
    {
        return score;
    }
    public int ScoreFormated()
    {
        return score;
    }
    public int GetLastScoreWon()
    {
        int a = lastScoreWon;
        lastScoreWon = 0;
        return a;
    }

    public void SaveUserDataToServer()
    {
        StartCoroutine(SaveUserDataC());
    }
    IEnumerator SaveUserDataC()
    {
        string hash = Utils.Md5Sum(SocialManager.Instance.userData.userID + score+ "pontura");
        string post_url = URL + setUserDataURL + "?userID=" + WWW.EscapeURL(SocialManager.Instance.userData.userID) + "&score=" + score + "&hash=" + hash;

        WWW www = new WWW(post_url);
        yield return www;

        if (www.error != null)
        {
            UsersEvents.OnPopup("There was an error: " + www.error);
        }
        else
        {
            string result = www.text;
            if (result == "exists")
            {
                UsersEvents.OnPopup("ya existe");
            }
            else
            {
                Debug.Log("UserData updated " + post_url);
            }
        }
    }
}
