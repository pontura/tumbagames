using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfilePicture : MonoBehaviour
{
    private Coroutine coroutine;
    
    void OnDestroy()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
    public void SetDefaultPicture(Sprite defaultSprite)
    {
        GetComponent<Image>().sprite = defaultSprite;
    }
    public void setPicture(string facebookID)
    {
        Texture2D texture = SocialManager.Instance.facebookFriends.GetProfilePicture(facebookID);
        if (texture != null)
        {
            SetPicture(texture);
        }
        else
        {
			print ("facebookID " + facebookID);
            coroutine = StartCoroutine(GetPicture(facebookID));
        }
    }
    IEnumerator GetPicture(string facebookID)
    {
        if (facebookID == "")
            yield break;

       // print("FACEBOOK - GetPicture " + facebookID);

        WWW receivedData = new WWW("https" + "://graph.facebook.com/" + facebookID + "/picture?width=128&height=128");
        yield return receivedData;
        if (receivedData.error == null)
        {
            SetPicture(receivedData.texture);
            SocialManager.Instance.facebookFriends.SetProfilePicture(facebookID, receivedData.texture);
        }
        else
        {
            Debug.Log("ERROR trayendo imagen");
        }

    }
    void SetPicture(Texture2D texture)
    {
        try
        {
            GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 128, 128), Vector2.zero);
        } catch
        {
            Debug.Log("otro tamanio de imagen de FB");
        }
    }
}
