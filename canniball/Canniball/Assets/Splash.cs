using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Invoke("StartPlaying", 0.1f);
    }
    void StartPlaying()
    {
        Data.Instance.LoadScene("Intro");
    }
}
