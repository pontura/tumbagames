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
        allTexts = Data.Instance.textsManager.intro.all[UnityEngine.Random.Range(0, Data.Instance.textsManager.intro.all.Length)];
        Events.OnKeyPress += OnKeyPress;
        Invoke("Loop", 2);
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
        float delay = 2 + (text.Length * 0.25f);

        if (id > allTexts.texts.Length-1)
            Invoke("WaitAndExit", delay);
        else
            Invoke("Loop", delay);
        
    }
    void OnKeyPress(int a)
    {
        if (!isReady)
            return;
        
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
