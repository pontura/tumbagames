using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneButton : MonoBehaviour {

    public int id;
    public int moodID;
    public int seccionalID;
    public bool unlocked;
    public GameObject iconLock;
    public GameObject iconUnlock;
    public Text title;

    public void Init(bool unlocked, string titleString, int moodID, int seccionalID)
    {
        this.moodID = moodID;
        this.seccionalID = seccionalID;
        this.unlocked = unlocked;
        if (unlocked)
            Unlock();
        else
            Lock();
        title.text = titleString;
    }
    public void Unlock()
    {
        iconLock.SetActive(false);
        iconUnlock.SetActive(true);
        title.color = Color.yellow;
    }
    public void Lock()
    {
        iconLock.SetActive(true);
        iconUnlock.SetActive(false);
        title.color = Color.black;
    }
}
