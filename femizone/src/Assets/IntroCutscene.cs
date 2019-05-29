using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public Text field;
    public TextsManager.TextsContent allTexts;
    int id = 0;
    bool isReady;

    void Start()
    {
        field.text = "";
        allTexts = Data.Instance.textsManager.GetNextIntro();
        Events.OnKeyPress += OnKeyPress;
        Invoke("Loop", 1.5f);
    }
    void OnDestroy()
    {
        Events.OnKeyPress -= OnKeyPress;
    }
    void Loop()
    { 
        string text = allTexts.texts[id];
        field.text = text;

        if (id > 0)
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
