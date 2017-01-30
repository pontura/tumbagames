using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerupUI : MonoBehaviour {

    public GameObject panel;
    public Text title;

    public PowerupSign powerUpSign;

    public GameObject bar;
    private float percent;
    public bool isOn;
    private int TimeToReset;
    public GameObject PowerUpRickyFort_UI;

    void Start()
    {
        powerUpSign.gameObject.SetActive(false);
        panel.SetActive(false);
        Events.OnBarInit += OnBarInit;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
        PowerUpRickyFort_UI.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnBarInit -= OnBarInit;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
    }
    void OnBarInit(PowerupManager.types type)
    {
        switch (type)
        {
            case PowerupManager.types.CHUMBO: title.text = "MEGA-CHUMBO"; TimeToReset = 10; break;
            case PowerupManager.types.GIL: title.text = "GIL-POWA"; TimeToReset = 10; break;
            case PowerupManager.types.MOTO: title.text = "RATI-CICLO"; TimeToReset = 10; break;
            case PowerupManager.types.RICKYFORT:
                PowerUpRickyFort_UI.SetActive(true); title.text = "MIAMI MODE"; TimeToReset = 14; break;
        }
        panel.SetActive(true);
        isOn = true;
        percent = 1;
        powerUpSign.gameObject.SetActive(true);
        powerUpSign.Init(type);
        
    }
    void Update()
    {
        if (!isOn) return;
        if (Game.Instance.gameManager.state == GameManager.states.ENDING) return;

        OnSetBar();
        percent -= Time.deltaTime/ TimeToReset;
        if (percent < 0)
            SetOff();
    }
    void SetOff()
    {
       // print("powerupUI setOff");
        OnHeroPowerUpOff();
        Events.OnHeroPowerUpOff();
    }
    void OnHeroPowerUpOff()
    {
        isOn = false;
        panel.SetActive(false);
        PowerUpRickyFort_UI.SetActive(false);
    }
    void OnSetBar()
    {
        bar.GetComponent<Image>().fillAmount = percent;
    }
}
