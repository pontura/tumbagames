using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Zones : MonoBehaviour {

    public GameObject container;    

	public void Init() {
        int id = 1;
        foreach (ZoneButton button in container.GetComponentsInChildren<ZoneButton>())
        {
            bool unlocked = false;
            if(Data.Instance.moodsManager.IsMoodUnlocked( button.id))
                unlocked = true;

            //string title = Data.Instance.texts.moods.GetDataById(button.id).title;
            button.Init(unlocked, "");
            id++;
        }
    }
    public void Clicked(int id)
    {
        GetComponent<Seccionales>().Init(id);
    }
}
