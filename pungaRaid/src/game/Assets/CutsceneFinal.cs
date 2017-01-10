using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneFinal : MonoBehaviour {

    public GameObject panel;
    private Animation anim;
    public Text field;

    void Start()
    {
        anim = panel.GetComponent<Animation>();
        Events.OnHeroDie += OnHeroDie;
        Events.OnPoolAllItemsInScene += OnPoolAllItemsInScene;
        panel.SetActive(false);
    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
        Events.OnPoolAllItemsInScene -= OnPoolAllItemsInScene;
    }

    void OnHeroDie()
    {       
        Invoke("SetOn", Random.Range(3, 8));
    }
    void SetOn()
    {
        panel.SetActive(true);
        anim.Play("on");
        field.text = Data.Instance.texts.GetRandomText(Data.Instance.texts.cutscene_final);        
        Invoke("SetOff", 7);
    }
    void SetOff()
    {
        anim.Play("off");
        Invoke("SetOn", Random.Range(3, 5));
    }
    void OnPoolAllItemsInScene()
    {
        panel.SetActive(false);
    }
}
