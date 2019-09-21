using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAsset : MonoBehaviour
{
    public Animation anim;
    System.Action OnLoaded;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioSource audioSource;

    public void SetOn(System.Action OnLoaded)
    {
        this.OnLoaded = OnLoaded;
        anim.Play("logo_enter");
        gameObject.SetActive(true);
        Invoke("Delayed", 3);
        Invoke("Delayed", 3);
        audioSource.clip = clip1;
        audioSource.Play();
    }
    void Delayed()
    {
        OnLoaded();
    }
    public void SetOff()
    {
        anim.Play("logo_end");
        Invoke("Delayed2", 2);
        audioSource.clip = clip2;
        audioSource.Play();
    }
    void Delayed2()
    {
        gameObject.SetActive(false);
    }
}
