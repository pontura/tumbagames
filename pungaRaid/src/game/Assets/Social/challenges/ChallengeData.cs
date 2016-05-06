using UnityEngine;
using System.Collections;

public class ChallengeData : MonoBehaviour {

    public bool isOn;
    public string objectID;
    public string username;
    public string facebookID;
    public float score;

	void Start () {
	    SocialEvents.OnChallengeConfirm += OnChallengeConfirm;
	}

    void OnChallengeConfirm(string objectID, string username, string facebookID, float score)
    {
        isOn = true;

        this.objectID = objectID;
        this.username = username;
        this.facebookID = facebookID;
        this.score = score;

        Data.Instance.LoadLevel("06_Versus");
	}
}
