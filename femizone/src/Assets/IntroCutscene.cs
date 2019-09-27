using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public TextsManager.TextsContent allTexts;
    int introID = 0;
    int id = 0;
    bool isReady;
    public GameObject[] all;
    Text field;

    void Start()
    {
        foreach (GameObject go in all)
            go.SetActive(false);

        Data.Instance.sequenceData.AddIfNotExists("intro", 3);
        introID = Data.Instance.sequenceData.GetIdFor("intro");

        Data.Instance.sequenceData.AddIfNotExists("intro1", Data.Instance.textsManager.intro.intro1.Length);
        Data.Instance.sequenceData.AddIfNotExists("intro2", Data.Instance.textsManager.intro.intro2.Length);
        Data.Instance.sequenceData.AddIfNotExists("intro3", Data.Instance.textsManager.intro.intro3.Length);       

        all[introID].SetActive(true);       
        
        int textsID;
        if(introID == 0)
        {
            textsID = Data.Instance.sequenceData.GetIdFor("intro1");
            allTexts = Data.Instance.textsManager.intro.intro1[textsID];
        }            
        else if(introID == 1)
        {
            textsID = Data.Instance.sequenceData.GetIdFor("intro2");
            allTexts = Data.Instance.textsManager.intro.intro2[textsID];
        }            
        else
        {
            textsID = Data.Instance.sequenceData.GetIdFor("intro3");
            allTexts = Data.Instance.textsManager.intro.intro3[textsID];
        }
        field = all[introID].GetComponentInChildren<Text>();
        field.text = "";
        Events.OnKeyPress += OnKeyPress;
        Invoke("Loop", 2f);
    }
    void OnDestroy()
    {
        Events.OnKeyPress -= OnKeyPress;
    }
    void Loop()
    {
        string text = allTexts.texts[id];
        field.text = text;

        if(id == 0)
            isReady = true;

        id++;
        float delay = 1.2f + (text.Length * 0.1f);

        if (id > allTexts.texts.Length - 1)
            Invoke("WaitAndExit", delay);
        else
            Invoke("Loop", delay);

    }
    void Next()
    {
        CancelInvoke();
        Loop();
    }
    void OnKeyPress(int a)
    {
        if (!isReady)
            return;

        if (id < allTexts.texts.Length)
            Next();
        else
            Done();
    }
    void WaitAndExit()
    {
        field.text = "";
        Invoke("Done", 2);
    }
    void Done()
    {
        if (!isReady)
            return;

        CancelInvoke();
        Data.Instance.LoadScene("Game");
        isReady = false;
    }
}
