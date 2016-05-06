using UnityEngine;
using System.Collections;

public class SpecialItemsManager : MonoBehaviour {

    public types type;
    public enum types
    {
        NONE,
        CASCO,
        TRANSPORT
    }
    public int id;
	void Start () {
        Events.OnSetSpecialItem += OnSetSpecialItem;
	}
    void OnDestroy()
    {
        Events.OnSetSpecialItem -= OnSetSpecialItem;
    }
    void OnSetSpecialItem(int _id, bool active)
    {
        if (active)
        {
            if (_id < 100)
                type = types.CASCO;
            else
                type = types.TRANSPORT;
            this.id = _id;
        } else
        {
            Events.OnResetSpeed();
            type = types.NONE;
            this.id = 0;
        }
    }
}
