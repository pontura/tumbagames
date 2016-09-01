using UnityEngine;
using System.Collections;

public class Seccionales : MonoBehaviour {

    public GameObject seccional1;
    public GameObject seccional2;
    
    public GameObject seccionalesContainer;

    private GameObject container;

    void Start()
    {
        seccionalesContainer.SetActive(false);
        Events.UnlockSeccional += UnlockSeccional;
    }
    void OnDestroy()
    {
        Events.UnlockSeccional -= UnlockSeccional;
    }   
    public void Init(int moodID)
    {
        switch (moodID)
        {
            case 1: container = seccional1; break;
            default: container = seccional2; break;
        }

        seccionalesContainer.SetActive(true);

        seccional1.SetActive(false);
        seccional2.SetActive(false);

        container.SetActive(true);

        int id = 0;
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
        {
            bool unlocked = false;
            TextsMoods.Data data = Data.Instance.moodsManager.data.data[moodID-1];

            unlocked = data.seccional[id].unlocked;

            string title = data.seccional[id].title;
            id++;
            button.Init(unlocked, title, moodID, id);            
        }
    }
    void UnlockSeccional(int moodID, int seccionalID)
    {
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
            if (button.seccionalID == seccionalID && button.moodID == moodID)
                button.Unlock();
    }
    public void Clicked(int id)
    {
        Data.Instance.moodsManager.SetCurrentSeccional(id);
        GetComponent<MoodPopup>().Open();
    }
    public void Back()
    {
        seccionalesContainer.SetActive(false);
    }
}
