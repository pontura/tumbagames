using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
#if UNITY_ANDROID
    public GameObject registerButton;
    public GameObject startButton;

    void Start()
    {
        registerButton.SetActive(false);
        startButton.SetActive(false);
        SocialManager.Instance.userData.Init(OnReady);
    }
    public void Register()
    {
        Data.Instance.LoadLevel("02_Registration");
    }

    void OnReady()
    {
        Debug.Log("User data OnReady: " + SocialManager.Instance.userData.IsLogged());
        if (!SocialManager.Instance.userData.IsLogged())
        {
            registerButton.SetActive(true);
            startButton.SetActive(false);
        }
        else
        {
            registerButton.SetActive(false);
            startButton.SetActive(true);
        }
    }
#endif
}
