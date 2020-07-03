using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserUIRegistrationPanel : MonoBehaviour
{
    public RawImage image;
    public AspectRatioFitter aspectRatioFilter;

    public AvatarThumb avatarThumb;
    public InputField field;
    UserDataUI userDataUI;
    public GameObject PhotoPanel;
    public GameObject PhotoTakenPanel;
    public GameObject[] hideOnScreenshot;
    bool userExists;
    public GameObject buttonsProfilePicture;

    public void Init(UserDataUI userDataUI, string _username)
    {
        PhotoPanel.SetActive(false);
        this.userDataUI = userDataUI;

        if (_username != "")
        {
            field.text = _username;
            userExists = true;
        }
        ShowEditPanel();
        UsersEvents.OnUserUploadDone += OnUserUploadDone;
    }
    void OnEnable()
    {
        if(SocialManager.Instance.userData.username != "")
            field.text = SocialManager.Instance.userData.username;
    }
    void OnUserUploadDone()
    {
        avatarThumb.Reset();
    }
    void OnDestroy()
    {
        UsersEvents.OnUserUploadDone -= OnUserUploadDone;
    }
    void ShowNewPhoto()
    {
        PhotoPanel.SetActive(true);
        PhotoTakenPanel.SetActive(false);
        userDataUI.webcamPhoto.InitWebcam(image, aspectRatioFilter);
    }
    void ShowEditPanel()
    {
        PhotoPanel.SetActive(false);
        PhotoTakenPanel.SetActive(true);
        avatarThumb.Init( SocialManager.Instance.userData.userID, SocialManager.Instance.userData.profilePhotoID);
        SocialManager.Instance.userData.avatarImages.GetImageFor(SocialManager.Instance.userData.userID, OnLoaded);
    }
    public void OnLoaded(Texture2D texture2d)
    {
        if (texture2d != null)
            buttonsProfilePicture.SetActive(false);
    }
    public void TakeSnapshot()
    {
        SocialManager.Instance.userData.avatarImages.ResetAvatar(SocialManager.Instance.userData.userID);
        foreach (GameObject go in hideOnScreenshot)
            go.SetActive(false);
        userDataUI.webcamPhoto.TakeSnapshot(OnPhotoTaken);
        StartCoroutine(SaveLocal(Screen.width, Screen.height));
       // StartCoroutine(SaveLocal(800, 600));
    }
    public IEnumerator SaveLocal(int width, int height) {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.LoadRawTextureData(texture.GetRawTextureData());
        texture.Apply();
        SocialManager.Instance.userData.avatarImages.SetImageFor(SocialManager.Instance.userData.userID, texture);
        avatarThumb.OnLoaded(texture);
    }
    void OnPhotoTaken()
    {
        foreach (GameObject go in hideOnScreenshot)
            go.SetActive(true);

        ShowEditPanel();
        userDataUI.userRegistrationForm.SavePhoto();        
    }
    public void ClickedNewPhoto()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
        }
        else
        {
            ShowNewPhoto();
        }
    }
    bool submited;
    public void OnSubmit()
    {
        if (userExists)
        {
            userDataUI.OnUpload(field.text);
        }
        else if(!submited)
        {
            submited = true;
         //   if (SocialManager.Instance.userData.sprite == null)
         //       userDataUI.DebbugText.text = "Falta la foto!";
         //   else 
            if (field.text == "")
                UsersEvents.OnPopup( "Falta un nombre de usuario" );
            else
                userDataUI.OnSubmit(field.text);
        }
    }
    public void Back()
    {
        ShowEditPanel();
    }
    int thumbID;
    public void ChangeThumb(bool next)
    {
        if (next)
            thumbID++;
        else
            thumbID--;

        if (thumbID < 0)
            thumbID = Data.Instance.profilePictures.all.Length - 1;
        else if (thumbID > Data.Instance.profilePictures.all.Length - 1)
            thumbID = 0;

        SocialManager.Instance.userData.profilePhotoID = thumbID;

        avatarThumb.SetProfileDefaultPhoto(thumbID);
    }
    public void Cancel()
    {
        Data.Instance.LoadLevel("02_Main");
    }
}
