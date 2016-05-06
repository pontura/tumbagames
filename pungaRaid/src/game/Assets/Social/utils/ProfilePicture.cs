using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfilePicture : MonoBehaviour
{
    //public void Init(Hiscores.Hiscore data)
    //{
    //    setPicture(data.facebookID);
    //}
    public void SetDefaultPicture(Sprite defaultSprite)
    {
        GetComponent<Image>().sprite = defaultSprite;
    }
    public void setPicture(string facebookID)
    {
       // if (!this.gameObject.activeInHierarchy) return;
        Texture2D texture = SocialManager.Instance.facebookFriends.GetProfilePicture(facebookID);
        if (texture != null)
        {
            SetPicture(texture);
        }
        else
        {
            StartCoroutine(GetPicture(facebookID));
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
        }
        else
        {
            Debug.Log("ERROR trayendo imagen");
        }

    }
    void SetPicture(Texture2D texture)
    {
        GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, 128, 128), Vector2.zero);
    }
}
