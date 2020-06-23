using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarThumb : MonoBehaviour
{
    public Image image;
    private Coroutine coroutine;
    bool destroyed;

    private void OnDestroy()
    {
        destroyed = true;
        StopAllCoroutines();
    }
    public void Init(string userID)
    {
        SocialManager.Instance.userData.avatarImages.GetImageFor(userID, OnLoaded);      
    }
    public void OnLoaded(Texture2D texture2d)
    {
        if (destroyed)
            return;

        if(image != null && texture2d != null)
            image.sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), new Vector2(0.5f, 0.5f));
    }
    public void Reset()
    {
        image.sprite = null;
    }

    //Facebook:
    public void SetFacebookPicture(string userID)
    {
            coroutine = StartCoroutine(GetPictureFromFacebook(userID));
    }
    IEnumerator GetPictureFromFacebook(string facebookID)
    {
        if (facebookID == "")
            yield break;

        WWW receivedData = new WWW("https" + "://graph.facebook.com/" + facebookID + "/picture?width=128&height=128");
        yield return receivedData;
        if (receivedData.error == null)
            SetPicture(receivedData.texture);
        else
            Debug.Log("ERROR trayendo imagen");

    }
    void SetPicture(Texture2D texture)
    {
        try
        {
            image.sprite = Sprite.Create(texture, new Rect(0, 0, 128, 128), Vector2.zero);
            image.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
        }
        catch
        {
            Debug.Log("otro tamanio de imagen de FB");
        }
    }
}
