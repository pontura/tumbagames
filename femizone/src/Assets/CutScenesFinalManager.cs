using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScenesFinalManager : MonoBehaviour
{
    public List<CutsceneInGame> gameOvers;
    public List<CutsceneInGame> bosses;
    public GameObject gameOverSignal;
    public Canvas canvasUI;
    public Text subtitles;
    states state;
    enum states
    {
        STARTING,
        ON,
        DONE
    }
    int id;
    int textsID;
    TextsManager.TextsContent allTexts;
    CutsceneInGame cutscene;
    CutsceneInGame.types type;

    void Start()
    {
        gameOverSignal.SetActive(false);
        Reset();
        Events.GameOver += GameOver;
        Events.OnCutscene += OnCutscene;
    }
    void OnDestroy()
    {
        Events.GameOver -= GameOver;
        Events.OnCutscene -= OnCutscene;
        Events.OnKeyPress -= OnKeyPress;
    }
    private void Reset()
    {
        canvasUI.enabled = false;
        foreach (CutsceneInGame go in gameOvers)
            go.gameObject.SetActive(false);

        foreach (CutsceneInGame go in bosses)
            go.gameObject.SetActive(false);
    }
    void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }
    void OnCutscene(CutsceneInGame.types type)
    {
        cutscene = GetCutsceneForBoss(type);
        this.type = type;
        if (type == CutsceneInGame.types.RUGBIERS)
        {
            textsID = Data.Instance.sequenceData.GetIdFor("rugbiers");
            allTexts = Data.Instance.textsManager.intro.rugbiers[textsID];
        }
        else if (type == CutsceneInGame.types.FETO)
        {
            textsID = Data.Instance.sequenceData.GetIdFor("feto");
            allTexts = Data.Instance.textsManager.intro.feto[textsID];
        }
        else if (type == CutsceneInGame.types.TRUMP)
        {
            textsID = Data.Instance.sequenceData.GetIdFor("trump");
            allTexts = Data.Instance.textsManager.intro.trump[textsID];
        }

        Invoke("DelayedStart", 3);
    }
    void DelayedStart()
    {         
        state = states.STARTING;

        SetTexts();
        id = 0;
        Reset();
        
        cutscene.gameObject.SetActive(true);

      
        Invoke("Delayed", 2);
    }
    void Delayed()
    {
        canvasUI.enabled = true;
        state = states.ON;
        Events.OnKeyPress += OnKeyPress;
    }
    void OnKeyPress(int a)
    {
        if (state == states.DONE || state == states.STARTING)
            return;

        id++;

        if (id < allTexts.texts.Length)
        {            
            SetTexts();
        }
        else
        {
            cutscene.Off();
            state = states.DONE;

            int timeToReset = 2;
            if (type == CutsceneInGame.types.RUGBIERS)
                timeToReset = 2;
            else if (type == CutsceneInGame.types.FETO)
                timeToReset = 4;

            Invoke("DelayedOff", timeToReset);
            Events.OnKeyPress -= OnKeyPress;
        }
       
    }
    void DelayedOff()
    {
        Reset();
        Events.OnCutsceneDone();
    }
    void SetTexts()
    {
        subtitles.text = allTexts.texts[id];
    }
    CutsceneInGame GetCutsceneForBoss(CutsceneInGame.types type)
    {
        foreach (CutsceneInGame cig in bosses)
            if (cig.type == type)
                return cig;
        return null;
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
        gameOvers[Data.Instance.settings.cutsceneFinalID].gameObject.SetActive(true);
        Data.Instance.settings.cutsceneFinalID++;
        if (Data.Instance.settings.cutsceneFinalID >= gameOvers.Count)
            Data.Instance.settings.cutsceneFinalID = 0;
    }

 }
