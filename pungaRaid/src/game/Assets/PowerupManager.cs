using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PowerupManager : MonoBehaviour {

    public types type;

    public GameObject powerUpsContainer;
    public GameObject powerUpGil;

    public PowerUpOn powerUp1;

    private PowerUpOn powerUp;
    private Character character;

    public GameObject powerUpPArticles;

    public enum types
    {
        NONE,
        MOTO,
        CHUMBO,
        GIL
    }
    void Start()
    {
        powerUpGil.SetActive(false);
        character = GetComponent<Character>();
        Events.OnPowerUp += OnPowerUp;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.OnPowerUp -= OnPowerUp;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
        Events.OnHeroDie -= OnHeroDie;
    }
    void OnPowerUp(types newType)
    {
        if (type != types.NONE) return;

        Events.OnBarInit(newType);
        Events.OnSoundFX("PowerUpItem");
                
        switch (newType)
        {
            case types.MOTO: Moto(); break;
            case types.CHUMBO: Chumbo(); break;
            case types.GIL: Gil(); break;
        }
        PlayParticles();
    }
    void PlayParticles()
    {
        powerUpPArticles.SetActive(true);
        foreach (ParticleSystem ps in powerUpPArticles.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
    }
    void Chumbo()
    {
        type = types.CHUMBO;
        character.PowerupActivated(type);
    }
    void Gil()
    {
        Events.OnSoundFXLoop("GilPowa");
        powerUpGil.SetActive(true);
        type = types.GIL;
        character.PowerupActivated(type);
    }
    void Moto()
    {
        type = types.MOTO;
        powerUp = Instantiate(powerUp1) as PowerUpOn;
        powerUp.transform.SetParent(powerUpsContainer.transform);
        powerUp.transform.localScale = Vector3.one;
        powerUp.transform.localPosition = Vector3.zero;
        powerUp.Init(10);

        character.OnSetHeroState(false);
        Events.OnChangeSpeed(2, true);
    }
    void OnHeroDie()
    {
        powerUpGil.SetActive(false);
    }
    void OnHeroPowerUpOff()
    {
        PlayParticles();
        character.Idle();

        Debug.Log("OnHeroPowerUpOff termino : " + type);

        Events.OnSoundFX("PowerUpOff");

        if (type == types.MOTO)
        {
            Events.OnSoundFXLoop("");
            character.OnSetHeroState(true);
            if (powerUp != null)
                Destroy(powerUp.gameObject);
            Events.OnResetSpeed();
        }
        else if (type == types.GIL)
        {
            Events.OnSoundFXLoop("");
            powerUpGil.SetActive(false);           
        }
        character.hero.ResetState();
        character.Jump("pungaJump");
        type = types.NONE;
    }
}
