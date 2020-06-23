using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataUI : MonoBehaviour
{
    public UserUIRegistrationPanel userRegistrationPanel;
    public UserUIRegisteredPanel userRegisteredPanel;
    public WebcamPhoto webcamPhoto;
    UserData userData;
    public UserRegistrationForm userRegistrationForm;
    public Text DebbugText;

    private void Start()
    {
        Init();
        UsersEvents.OnRegistartionDone += OnRegistartionDone;
        UsersEvents.OnUserUploadDone += OnUserUploadDone;
    }
    private void OnDestroy()
    {
        UsersEvents.OnRegistartionDone -= OnRegistartionDone;
        UsersEvents.OnUserUploadDone -= OnUserUploadDone;
    }
    void OnRegistartionDone()
    {
        Done();
    }
    void OnUserUploadDone()
    {
        Done();
    }
    public void Done()
    {
        Data.Instance.LoadLevel("02_Main");
    }
    public void Init()
    {
        userRegistrationForm.Init();
        userData = SocialManager.Instance.userData;
        userRegistrationPanel.gameObject.SetActive(false);
        userRegisteredPanel.gameObject.SetActive(false);
        Invoke("SetPanelsIfLogged", 0.1f);
        webcamPhoto = GetComponent<WebcamPhoto>();
    }
    
    void SetPanelsIfLogged()
    {
        if (userData.username == "")
        {
            userRegistrationPanel.gameObject.SetActive(true);
            userRegistrationPanel.Init(this, userData.username);

            userRegisteredPanel.gameObject.SetActive(false);
        } else
        {
            userRegisteredPanel.gameObject.SetActive(true);
            userRegisteredPanel.Init(this, userData.userID, userData.username);

            userRegistrationPanel.gameObject.SetActive(false);
        }
    }
    public void EditData()
    {
        userRegisteredPanel.gameObject.SetActive(false);
        userRegistrationPanel.gameObject.SetActive(true);
        userRegistrationPanel.Init(this, userData.username);
    }
    public void EditDone()
    {
        userRegisteredPanel.gameObject.SetActive(true);
        userRegistrationPanel.gameObject.SetActive(false);
        userRegistrationPanel.Init(this, userData.username);
    }
     public void OnSubmit(string username)
     {
         userRegistrationForm.SaveUser(username);
     }
     public void OnUpload(string username)
     {
         userRegistrationForm.UploadUser(username);
     }
}
