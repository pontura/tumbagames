using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoodPopup : MonoBehaviour {

    public Text title;
    public Text desc;
    public Text hiscore;
    public Text priceField;

    public Image BannerContainer;

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

        BannerContainer.sprite = Resources.Load("seccionales/" + moodID + "_" + seccional.id, typeof(Sprite)) as Sprite;

        if (seccional.unlocked)
            locker.SetActive(false);
        else
        {
            priceField.text = Utils.IntToMoney(seccional.price);
            locker.SetActive(true);
            Invoke("ComisarioOn", 0.5f);
        }

        title.text = seccional.title;
        desc.text = seccional.name;
        hiscore.text = Utils.IntToMoney(seccional.price);

        ranking.Init(moodID, seccional.id);
        
    }
    void ComisarioOn()
    {
        panel.GetComponent<Animator>().Play("comisario", 0, 0);
    }
    public void Go()
    {
        Events.OnLoadCurrentAreas();
        Data.Instance.LoadLevel("03_PreloadingGame");
    }
    public void Close()
    {
        CloseOff();
    }
    void CloseOff()
    {
        panel.SetActive(false);
    }
    public void Unlock()
    {
        print("unlok seccional.price: " + seccional.price);
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
