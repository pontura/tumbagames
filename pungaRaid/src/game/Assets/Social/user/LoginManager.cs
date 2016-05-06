using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class LoginManager : MonoBehaviour {

    private string id;
    private string facebookID;
    private string username;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }
    void SetInit()
    {
        if (FB.IsLoggedIn) {
            Debug.Log ("FB is logged in");
            FB.API("/me?fields=name", HttpMethod.GET, DisplayUsername);
            SocialManager.Instance.facebookFriends.GetFriends();
        } else {
            Debug.Log ("FB is not logged in");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {

        List<string> permissions = new List<string> ();
        permissions.Add ("public_profile");

        FB.LogInWithReadPermissions (permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null) {
            Debug.Log (result.Error);
        } else {
            SetInit();
        }
    }   
 
    void DisplayUsername(IResult result)
    {
        facebookID = result.ResultDictionary["id"].ToString();
        username = result.ResultDictionary["name"].ToString();
        Debug.Log("facebookID: " + facebookID + " username:" + username  );
        CheckIfUserExists(facebookID);
    }

    protected void CheckIfUserExists(string facebookID)
    {
        Debug.Log("CheckIfUserExists: " + facebookID + username);


        string url = SocialManager.Instance.FIREBASE + "/users.json?orderBy=\"facebookID\"&equalTo=\"" + facebookID + "\"";

        Debug.Log(url);

        HTTP.Request someRequest = new HTTP.Request("get", url);
        someRequest.Send((request) =>
        {
            Hashtable decoded = (Hashtable)JSON.JsonDecode(request.response.Text);

            if (decoded == null)
            {
                
                Debug.Log("no existe el user or malformed response ):");
                return;
            }
            else if (decoded.Count == 0)
            {
                AddNewUser(facebookID);
            }
            else
            {
                SocialEvents.OnUserReady(username, facebookID);
            }            
        });
    }
    protected void AddNewUser(string facebookID)
    {
        Debug.Log("AddNewUser" + facebookID + username);

        Hashtable data = new Hashtable();

        data.Add("playerName", username);
        data.Add("facebookID", facebookID);

        HTTP.Request theRequest = new HTTP.Request("post", SocialManager.Instance.FIREBASE + "/users.json", data);
        theRequest.Send((request) =>
        {
            Hashtable jsonObj = (Hashtable)JSON.JsonDecode(request.response.Text);
            if (jsonObj == null)
            {
                Debug.LogError("server returned null or malformed response ):");
            }
            else
            {
                Debug.Log("nuevo usuario!!");
                SocialEvents.OnUserReady(username, facebookID);
            }
        });
    }
}
