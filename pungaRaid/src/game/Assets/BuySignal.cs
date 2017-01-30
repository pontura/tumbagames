using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuySignal : MonoBehaviour {

    public Text field;
    public Image image;
    public GameObject panel;
    private Seccional seccional;
    int moodID;
    void Start()
    {
        panel.SetActive(false);
    }
    public void Init (Seccional seccional) {
        panel.SetActive(true);
        int money = SocialManager.Instance.userHiscore.money;
        field.text = Utils.IntToMoney(money) + " pesito... Es hora de ir ampliando el negocio... no, nene?... Que decí?... Te abrimo " + seccional.title + "? por $" + Utils.IntToMoney(seccional.price);
        
        this.moodID = seccional.moodID;

        this.seccional = seccional;

        image.sprite = Resources.Load("seccionales/" + moodID + "_" + seccional.id, typeof(Sprite)) as Sprite;
    }	
	public void Sobornar()
    {
        print("Sobornar    seccional:  " + seccional.id + " mood: " + moodID);
        SocialEvents.OnUpdateMoney(-seccional.price);
        Events.OnGenericPopup("¡Listo Pibe!", "Gracias por la colaboración. Que disfrutes " + seccional.title);
        Events.UnlockSeccional(moodID, seccional.id);
        Invoke("Delayed", 0.1f);
    }
    void Delayed()
    {
        Data.Instance.LoadLevel("02_Main");
    }
    public void No()
    {
        Close();
    }
    void Close()
    {
        panel.SetActive(false);
        GetComponent<Summary>().CheckedIfShowRuleta();
    }
}
