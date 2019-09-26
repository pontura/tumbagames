using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenesFinalManager : MonoBehaviour
{
    public GameObject[] all;
    public GameObject gameOverSignal;

    void Start()
    {
        gameOverSignal.SetActive(false);
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
      //  Events.OnMusicVolumeChanged(0.2f);
        StartCoroutine(GameOverRoutine());
    }
    IEnumerator GameOverRoutine()
    {
        if (Data.Instance.settings.cutsceneFinalID == 3)
            Data.Instance.GetComponent<MusicManager>().ForceDancers();
        else if (Data.Instance.settings.cutsceneFinalID == 2)
            Data.Instance.GetComponent<MusicManager>().ForceLuisAguile();
        else if (Data.Instance.settings.cutsceneFinalID == 0)
            Data.Instance.GetComponent<MusicManager>().ForceHiphop();
        gameOverSignal.SetActive(true);
        yield return new WaitForSeconds(2.5f);
       // Events.OnMusicVolumeChanged(0.7f);
        gameOverSignal.SetActive(false);
        all[Data.Instance.settings.cutsceneFinalID].SetActive(true);
        Data.Instance.settings.cutsceneFinalID++;
        if (Data.Instance.settings.cutsceneFinalID >= all.Length)
            Data.Instance.settings.cutsceneFinalID = 0;
    }

 }
