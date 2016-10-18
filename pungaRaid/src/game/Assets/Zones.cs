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
            //if(Data.Instance.moodsManager.IsMoodUnlocked( button.id))
            //    unlocked = true;

            //string title = Data.Instance.moodsManager.data.GetDataById(button.id).title;
          //  button.Init(unlocked, title, id, 0);
            id++;
        }
    }
    //public void Clicked(int id)
    //{
    //    if (!Data.Instance.moodsManager.data.GetDataById(id).unlocked)
    //    {
    //        Events.OnGenericPopup("Ande vas?", "Primero tenés que desbloquearlo, gato");
    //        return;
    //    }
    //    GetComponent<Seccionales>().Init(id);
    //    Data.Instance.moodsManager.SetCurrentMood(id);
    //}
}
