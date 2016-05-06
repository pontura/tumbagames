using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuletaPopup : MonoBehaviour {

    public Text desc;
    public Image icon;
    public Animator anim;

    void Start()
    {
        anim.gameObject.SetActive(false);
    }
	
	public void Open (Ruleta.Item item) {

        Data.Instance.moodsManager.UnlockMood(2);

        if (item.item > 0)
        {
            icon.sprite = Resources.Load("helmets/" + item.item, typeof(Sprite)) as Sprite;
        }
        else if (item.zone > 0)
        {
            icon.sprite = Resources.Load("zones/" + item.zone, typeof(Sprite)) as Sprite;
            Data.Instance.moodsManager.UnlockMood(item.zone);
        }
        Invoke("OpenDelay", 0.5f);
        desc.text = item.text;
        if (item.item != 0)
            Events.OnSetSpecialItem(item.item, true);
	}
    public void OpenDelay()
    {
        anim.gameObject.SetActive(true);
        anim.Play("PopupOn", 0, 0);
    }
    public void Map()
    {
        Data.Instance.LoadLevel("02_Main");
    }
}
