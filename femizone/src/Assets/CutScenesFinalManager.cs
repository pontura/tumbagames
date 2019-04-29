using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenesFinalManager : MonoBehaviour
{
    public GameObject[] all;

    void Start()
    {
        foreach (GameObject go in all)
            go.SetActive(false);

        Events.GameOver += GameOver;
    }
    void OnDestroy()
    {
        Events.GameOver -= GameOver;
    }
    void GameOver()
    {
        all[Data.Instance.settings.cutsceneFinalID].SetActive(true);
        Data.Instance.settings.cutsceneFinalID++;
        if (Data.Instance.settings.cutsceneFinalID >= all.Length)
            Data.Instance.settings.cutsceneFinalID = 0;
    }

}
