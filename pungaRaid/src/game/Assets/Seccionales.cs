using UnityEngine;
using System.Collections;

public class Seccionales : MonoBehaviour {

    public GameObject container;
    public GameObject seccionalesContainer;

    void Start()
    {
        seccionalesContainer.SetActive(false);
    }
    public void Init(int moodID)
    {
        seccionalesContainer.SetActive(true);

        int id = 0;
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
        {
            bool unlocked = false;
            TextsMoods.Data data = Data.Instance.moodsManager.data.data[moodID-1];

            unlocked = data.seccional[id].unlocked;

            string title = data.seccional[id].title;
            button.Init(unlocked, title);
            id++;
        }
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
