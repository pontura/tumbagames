using UnityEngine;
using System.Collections;

public class SpecialItems : MonoBehaviour {

    private Hero hero;

	void Start () {
        hero = GetComponent<Character>().hero;
        if (Data.Instance.specialItems.type == SpecialItemsManager.types.CASCO)
            OnSetSpecialItem(Data.Instance.specialItems.id, true);
        else
            hero.casco.gameObject.SetActive(false);
        Events.OnSetSpecialItem += OnSetSpecialItem;
	}
    void OnDestroy()
    {
        Events.OnSetSpecialItem -= OnSetSpecialItem;
    }
    void OnSetSpecialItem(int id, bool active)
    {
        if (id < 100) 
            OnSpecialCasco(id, active);
        else
            OnSpecialTransport(id, active);
    }
    void OnSpecialCasco(int id, bool active)
    {
        if (active)
        {
            hero.casco.gameObject.SetActive(true);
            hero.casco.sprite = Resources.Load("helmets/" + id, typeof(Sprite)) as Sprite;
        }
        else
            hero.casco.gameObject.SetActive(false);
    }
    void OnSpecialTransport(int id, bool active)
    {
        if (active)
        {
            hero.transport.gameObject.SetActive(true);
            hero.transport.sprite = Resources.Load("transports/" + id, typeof(Sprite)) as Sprite;
        }
        else
            hero.transport.gameObject.SetActive(false);
    }
}
