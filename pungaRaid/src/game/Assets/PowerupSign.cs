using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PowerupSign : MonoBehaviour {

    private Animator anim;
    public Text title;
    public Text desc;

    public void Init(PowerupManager.types type)
    {
        if(Data.Instance.musicManager.volume==1)
            Events.OnMusicVolumeChanged(0.2f);
        anim = GetComponent<Animator>();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        Invoke("onComplete", 0.1f);

        anim.Play("powerupSign", 0, 0);

        switch (type)
        {
            case PowerupManager.types.CHUMBO: 
                title.text = "MEGA-CHUMBO";
                desc.text = "TREMENDO CHUMBO + 1 AGUANTE";
                break;
            case PowerupManager.types.GIL: 
                title.text = "GIL-POWA";
                desc.text = "PODER DE EXTRA PUNGUEO + 1 AGUANTE";
                break;
            case PowerupManager.types.MOTO: 
                title.text = "RATI-CICLO";
                desc.text = "APLASTA COBANIS Y CIVILES + 1 AGUANTE";
                break;
        }
        Time.timeScale = 0.05f;
    }
    void onComplete()
    {
        if (Data.Instance.musicManager.volume == 0.2f)
            Events.OnMusicVolumeChanged(1f);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
