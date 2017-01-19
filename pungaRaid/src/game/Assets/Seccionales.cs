using UnityEngine;
using System.Collections;

public class Seccionales : MonoBehaviour {

    //public GameObject seccional1;
    //public GameObject seccional2;
    public GameObject container;

    void Start()
    {
        Events.UnlockSeccional += UnlockSeccional;
        Init();
    }
     
    void Init()
    {
        //switch (moodID)
        //{
        //    case 1: container = seccional1; break;
        //    default: container = seccional2; break;
        //}

        //seccional1.SetActive(false);
        //seccional2.SetActive(false);

        //container.SetActive(true);

        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
        {
            print(button.seccionalID);
            bool unlocked = false;

            int moodID = button.moodID - 1;

            TextsMoods.Data data = Data.Instance.moodsManager.data.data[moodID];

            unlocked = data.seccional[button.seccionalID-1].unlocked;

            int price = data.seccional[button.seccionalID - 1].price;
            bool canBePaid = false;
            if (price < SocialManager.Instance.userHiscore.money)
                canBePaid = true;

            button.Init(unlocked, this, canBePaid);            
        }
    }
    void OnDestroy()
    {
        Events.UnlockSeccional -= UnlockSeccional;
    }  
    void UnlockSeccional(int moodID, int seccionalID)
    {
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
            if (button.seccionalID == seccionalID && button.moodID == moodID)
                button.Unlock();
    }
    public void Clicked(ZoneButton zoneButton)
    {
        Data.Instance.moodsManager.SetCurrentMood(zoneButton.moodID);
        Data.Instance.moodsManager.SetCurrentSeccional(zoneButton.seccionalID);
        GetComponent<MoodPopup>().Open();
    }
    //public void Back()
    //{
    //    seccionalesContainer.SetActive(false);
    //}
}
