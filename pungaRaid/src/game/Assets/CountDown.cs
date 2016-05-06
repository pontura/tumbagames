using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour {

    [SerializeField]
    Text field;
    private int num = 3;

    void Awake()
    {
        field.text = "";
        Events.OnStartCountDown += OnStartCountDown;
    }
    void OnDestroy()
    {
        Events.OnStartCountDown -= OnStartCountDown;
    }
    void OnStartCountDown()
    {
        Invoke("waitToSay", 0.5f);
    }

    void waitToSay()
    {
        Events.OnSoundFX("20_Three Two One Go");
        field.text = num.ToString();
        Invoke("nextNum", 0.5f);
        GetComponent<Animation>()["CountDown"].normalizedTime = 0;
        GetComponent<Animation>().Play("CountDown"); 
    }

    void nextNum()
    {
        GetComponent<Animation>()["CountDown"].normalizedTime = 0;
        GetComponent<Animation>().Play("CountDown");
        num--;
        field.text = num.ToString();
        if (num <= 0)
        {
            Events.StartGame();
            Destroy(gameObject);
            field = null;
        }
        else
        {
            Invoke("nextNum", 0.5f);
        }
    }
}
