using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameButton game1;
    public GameButton game2;
    int id = 1;

    void Start()
    {
        SetActive();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Next();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || 
            Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X)
            )
        {
            Clicked();
        }
    }
    void Next()
    {
        if (id == 1)
            id = 2;
        else
            id = 1;
        SetActive();
    }
    void SetActive()
    {
        game1.SetOn(false);
        game2.SetOn(false);
        if (id == 1)
            game1.SetOn(true);
        else
            game2.SetOn(true);
    }
    void Clicked()
    {
        if (id == 1)
            ExecuteProgramm("C:/builds/mad_rollers/MR.exe");
        else
            ExecuteProgramm("C:/builds/perraForce/Perra Force.exe");

        Application.Quit();
    }
    void ExecuteProgramm(string url)
    {
        string urlReal = string.Format("file://{0}", url);
        print(url);
        System.Diagnostics.Process.Start(urlReal);
    }
}
