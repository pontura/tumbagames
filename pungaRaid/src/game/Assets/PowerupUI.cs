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

    void Start()
    {
        powerUpSign.gameObject.SetActive(false);
        panel.SetActive(false);
        Events.OnBarInit += OnBarInit;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
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
            case PowerupManager.types.CHUMBO: title.text = "MEGA-CHUMBO"; break;
            case PowerupManager.types.GIL: title.text = "GIL-POWA"; break;
            case PowerupManager.types.MOTO: title.text = "RATI-CICLO"; break;
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
        percent -= Time.deltaTime/10;
        if (percent < 0)
            SetOff();
    }
    void SetOff()
    {
        print("powerupUI setOff");
        OnHeroPowerUpOff();
        Events.OnHeroPowerUpOff();
    }
    void OnHeroPowerUpOff()
    {
        isOn = false;
        panel.SetActive(false);
    }
    void OnSetBar()
    {
        bar.GetComponent<Image>().fillAmount = percent;
    }
}
