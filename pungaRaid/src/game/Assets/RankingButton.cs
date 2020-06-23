using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RankingButton : MonoBehaviour {

    public Text scoreField;
    public Text usernameField;
    public AvatarThumb avatarThumb;
    public Color firstColorBG;
    public GameObject coronita;
    Ranking.RankingData data;

    public void Init(Ranking.RankingData data, bool isFirst) {

        this.data = data;
        if (isFirst)
        {
            coronita.SetActive(true);
        }
        else
            coronita.SetActive(false);

        if (scoreField)
        scoreField.text = Utils.IntToMoney(data.score);

        if (usernameField)
            usernameField.text = data.username.ToUpper();

        if(data.facebookID != "")
            avatarThumb.SetFacebookPicture(data.facebookID);
        else
            avatarThumb.Init(data.userID);
	}
    public void Clicked()
    {
        Vector3 pos = Input.mousePosition;
        Events.OnAvatarSignal(pos, data.username);
    }
    public void IsYou()
    {
        if (scoreField) scoreField.color = Color.red;
        if (usernameField) usernameField.color = Color.red;
    }
}
