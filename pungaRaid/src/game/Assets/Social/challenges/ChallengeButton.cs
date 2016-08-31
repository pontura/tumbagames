using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChallengeButton : MonoBehaviour {

    public ProfilePicture profilePicture;
    public Text field;
    public string facebookID;
    public string username;

    //public void Init (ChallengesUI master,  string facebookID, string username, bool challenged) {
    //    if (challenged) field.text = "LISTO: ";
    //    field.text += username.ToString();
    //    profilePicture.setPicture(facebookID);

    //    this.facebookID = facebookID;
    //    this.username = username;

    //}
    //public void  Clicked() { 
    //    SocialEvents.OnChallengeCreate(username, facebookID, 43); 
    //}
}
