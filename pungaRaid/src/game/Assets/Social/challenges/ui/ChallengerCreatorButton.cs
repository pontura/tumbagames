using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChallengerCreatorButton : MonoBehaviour {

    public string facebookID;
    public Text usernameLabel;
    public ProfilePicture profilePicture;
    private ChallengerCreator creator;
    public GameObject SendButton;
    public GameObject Ready;

    private string playerName;

    public void Init(ChallengerCreator _creator, string playerName, string facebookID, bool done)
    {
        this.playerName = playerName;
        this.facebookID = facebookID;

        Ready.SetActive(false);
        this.creator = _creator;

        usernameLabel.text = playerName.ToUpper();
        profilePicture.setPicture(facebookID);

        if (done)
        {
            SendButton.SetActive(false);
            Ready.SetActive(true);

            //GetComponent<Button>().onClick.AddListener(() =>
            //{
            //    Remind();
            //});

        }
        else
        {
            SendButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                creator.Challenge(playerName, facebookID);
            });
        }
    }
    //void Remind()
    //{
    //    Ready.SetActive(false);
    //   // creator.Remind(playerName, facebookID);
    //}
}
