using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PowerdownManager : MonoBehaviour {

    public types type;

    public GameObject sorete;

    private Character character;

    public enum types
    {
        SORETE
    }
    void Start()
    {
        sorete.SetActive(false);
        character = GetComponent<Character>();
    }
    void OnDestroy()
    {
    }
    void OnPowerUp(types newType)
    {
        if (type != types.SORETE) return;

      //  Events.OnBarInit(newType);
        Events.OnSoundFX("PowerUpItem");
        
        switch (newType)
        {
            case types.SORETE: 
                //Moto(); 
                break;
        }
        
    }
}
