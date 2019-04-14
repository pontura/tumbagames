using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAsset : MonoBehaviour
{
    public Animation anim;
    System.Action OnLoaded;
    public void SetOn(System.Action OnLoaded)
    {
        this.OnLoaded = OnLoaded;
        anim.Play("logo_enter");
        gameObject.SetActive(true);
        Invoke("Delayed", 3);
    }
    void Delayed()
    {
        OnLoaded();
    }
    public void SetOff()
    {
        anim.Play("logo_end");
        Invoke("Delayed2", 2);
    }
    void Delayed2()
    {
        gameObject.SetActive(false);
    }
}
