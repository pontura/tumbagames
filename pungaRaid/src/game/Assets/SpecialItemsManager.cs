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

    private types lastType;
    private int last_id;

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
            {
                if(type == types.CASCO)
                {
                    lastType = type;
                    last_id = id;
                }
                type = types.TRANSPORT;
            }
            this.id = _id;
        } else
        {
            if(lastType == types.CASCO)
            {               
                Invoke("ReloadLastSpecialItem", 0.1f);
            }
            Events.OnResetSpeed();
            type = types.NONE;
            this.id = 0;
        }
    }
    void ReloadLastSpecialItem()
    {
        this.id = last_id;
        type = lastType;

        Events.OnSetSpecialItem(last_id, true);

        lastType = types.NONE;
        last_id = 0;
        
    }
}
