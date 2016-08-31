using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodPopup : MonoBehaviour {

    public Text title;
    public Text desc;
    public Text hiscore;
    public Text priceField;

    public RankingMood ranking;
    public GameObject locker;

    public GameObject panel;

	void Start () {
        panel.SetActive(false);    
	}
    public void Open()
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOn",0,0);
        int moodID = Data.Instance.moodsManager.GetCurrentMoodID();
        Seccional seccional = Data.Instance.moodsManager.GetCurrentSeccional();

        if (seccional.unlocked)
            locker.SetActive(false);
        else
        {
            priceField.text = "$" + seccional.price + "pe";
            locker.SetActive(true);
        }

        title.text = seccional.title;
        desc.text = seccional.name;
        hiscore.text = "$" + seccional.price + "pe";

        ranking.Init(moodID, seccional.id);
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
    public void Unlock()
    {

    }
}
