using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

    public RankingUI ranking;
    public GameObject connect;

    void Start()
    {
        Events.OnMusicChange("Menu");
    }
}
