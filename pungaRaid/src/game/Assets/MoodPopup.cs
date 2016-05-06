using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodPopup : MonoBehaviour {

    public Text title;
    public Text desc;
    public Text hiscore;

    public RankingMood ranking;

    public GameObject panel;

	void Start () {
        panel.SetActive(false);    
	}
    public void Open()
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOn",0,0);
        int moodID = Data.Instance.moodsManager.currentMood;
        TextsMoods.Data data =  Data.Instance.texts.moods.GetDataById(moodID);

        title.text = data.title;
        desc.text = data.text;
        hiscore.text = "$" + SocialManager.Instance.userHiscore.GetHiscore(data.id) + " guita";

        ranking.Init(moodID);
    }
    public void Go()
    {
        Data.Instance.LoadLevel("03_PreloadingGame");
    }
    public void Close()
    {
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOff", 0, 0);
        Invoke("CloseOff", 0.2f);
    }
    void CloseOff()
    {
        panel.SetActive(false);
    }
}
