using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MoodPopup : MonoBehaviour {

    public Text title;
    public Text desc;
    public Text hiscore;
    public Text priceField;
    public Text unlocked_levels_field;
    public GameObject hsicorePanel;

    public Image BannerContainer;

    public RankingMood ranking;
    public GameObject locker;

    public GameObject panel;

    private  int moodID ;
    private Seccional seccional;
	private int seccionalID;

	public Image medal;

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
		seccionalID = seccional.id;

        BannerContainer.sprite = Resources.Load("seccionales/" + moodID + "_" + seccional.id, typeof(Sprite)) as Sprite;

        if (seccional.unlocked)
        {
            locker.SetActive(false);

            int myHiscore = SocialManager.Instance.userHiscore.GetHiscore(moodID, seccional.id);
            

            if (myHiscore > 0)
            {
                hsicorePanel.SetActive(true);
                hiscore.text = Utils.IntToMoney(myHiscore);
            }
            else
                hsicorePanel.SetActive(false);
        }
        else
        {
            unlocked_levels_field.text = Data.Instance.texts.GetRandomText(Data.Instance.texts.unlocked_levels);
            hsicorePanel.SetActive(false);
            priceField.text = Utils.IntToMoney(seccional.price);
            locker.SetActive(true);
            Invoke("ComisarioOn", 0.5f);
        }

        title.text = seccional.title;
        desc.text = seccional.name;

       

        ranking.Init(moodID, seccional.id);

		string medalName = "";
		if (moodID == 1 && seccionalID == 1)
			medalName = "achivements_RECAMIER_1";
		else if (moodID == 1 && seccionalID == 2)
			medalName = "achivements_NORCO_1";
		else if (moodID == 1 && seccionalID == 3)
			medalName = "achivements_ZABECA_1";
		else if (moodID == 2 && seccionalID == 1)
			medalName = "achivements_MAMERTO_1";
		else if (moodID == 2 && seccionalID == 2)
			medalName = "achivements_DILDO_1";
		else if (moodID == 2 && seccionalID == 3)
			medalName = "achivements_PUERTO_1";
		
		medal.sprite = Resources.Load("achievements/" + medalName, typeof(Sprite)) as Sprite;
        
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
            Events.OnGenericPopup("No te hagas el vivo", "No tenés tantos belgranos para desbloquear este barrio");
        }
        else
        {
            SocialEvents.OnUpdateMoney(-seccional.price);
			AchievementsEvents.OnUnlock (moodID, seccional.id);
            Events.OnGenericPopup("¡Listo Pibe!", "Gracias por la colaboración. Que disfrutes el nuevo barrio");
            Events.UnlockSeccional(moodID, seccional.id);
            CloseOff();
        }
    }
	public void MedalClicked()
	{
		string listID = "";
		if (moodID == 1 && seccionalID == 1)
			listID = "RECAMIER";
		else if (moodID == 1 && seccionalID == 2)
			listID = "NORCOREANO";
		else if (moodID == 1 && seccionalID == 3)
			listID = "ZABECA";
		else if (moodID == 2 && seccionalID == 1)
			listID = "MAMERTO";
		else if (moodID == 2 && seccionalID == 2)
			listID = "CENTRAL";
		else if (moodID == 2 && seccionalID == 3)
			listID = "RETIRO";

		List<Achievement> achievementList = AchievementsManager.Instance.GetAchievementsByListID (listID);
		Events.OnShowAchievementList (achievementList);
	}
}
