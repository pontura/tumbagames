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
    private Seccionales seccionales;

    public void Init(bool unlocked, Seccionales seccionales)
    {
        this.seccionales = seccionales;
        this.unlocked = unlocked;
        if (unlocked)
            Unlock();
        else
            Lock();
    }
    public void Unlock()
    {
        iconLock.SetActive(false);
        iconUnlock.SetActive(true);
    }
    public void Lock()
    {
        iconLock.SetActive(true);
        iconUnlock.SetActive(false);
    }
    public void Clicked()
    {
        seccionales.Clicked(this);
    }
}
