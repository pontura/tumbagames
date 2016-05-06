using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneButton : MonoBehaviour {

    public int id;
    public bool unlocked;
    public GameObject iconLock;
    public GameObject iconUnlock;
    public Text title;

    public void Init(bool unlocked, string titleString)
    {
        this.unlocked = unlocked;
        if (unlocked)
        {
            iconLock.SetActive(false);
            iconUnlock.SetActive(true);
            title.color = Color.yellow;
        }
        else
        {
            GetComponent<Button>().interactable = false;
            iconLock.SetActive(true);
            iconUnlock.SetActive(false);
            title.color = Color.black;
        }
        title.text = titleString;
    }
}
