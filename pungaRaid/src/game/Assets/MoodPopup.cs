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

    private  int moodID ;
    private Seccional seccional;

	void Start () {
        panel.SetActive(false);    
	}
    public void Open()
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        panel.GetComponent<Animator>().Play("PopupOn",0,0);
        moodID = Data.Instance.moodsManager.GetCurrentMoodID();
        seccional = Data.Instance.moodsManager.GetCurrentSeccional();

        if (seccional.unlocked)
            locker.SetActive(false);
        else
        {
            priceField.text = Utils.IntToMoney(seccional.price);
            locker.SetActive(true);
        }

        title.text = seccional.title;
        desc.text = seccional.name;
        hiscore.text = Utils.IntToMoney(seccional.price);

        ranking.Init(moodID, seccional.id);
    }
    public void Go()
    {
        Events.OnLoadCurrentAreas();
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
        if (SocialManager.Instance.userHiscore.money < seccional.price)
        {
            Events.OnGenericPopup("No te hagas el vivo", "No tenés tantos belgranos para desbloquear esto");
        }
        else
        {
            SocialEvents.OnUpdateMoney(-seccional.price);
            Events.OnGenericPopup("¡Listo Pibe!", "Ponete un texto gracioso...");
            Events.UnlockSeccional(moodID, seccional.id);
            CloseOff();
        }
    }
}
