using UnityEngine;
using System.Collections;

using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


public class MainMenu : MonoBehaviour {

    public void Connect()
    {
        Events.OnLoginAdvisor();
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("02_Main");
	}
    public void OnSettings()
    {
        if (Time.timeScale == 0) return;
        Events.OnSettings();
    }
#if UNITY_STANDALONE
    private bool isDead;
    void Start()
    {
        Events.OnHeroDie += OnHeroDie;
    }
    void OnHeroDie()
    {
        isDead = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isDead)
            {
                SaveNew();
            }

            if (Data.Instance.currentLevel == "02_Intro")
            {
                Data.Instance.LoadLevel("03_PreloadingGame");
            }
        }
    }
    public void SaveNew()
    {
        isDead = false;
        string[] content = new string[] { "PR_" + Game.Instance.gameManager.score };
        File.WriteAllLines("C:\\tumbagames\\hiscores\\data.txt", content);
        Invoke("timeOut1", 2);
    }
    void timeOut1()
    {
        Process foo = new Process();
        foo.StartInfo.FileName = "openHiscores.bat";
        //  foo.StartInfo.Arguments = "put your arguments here";
        // foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        foo.Start();
        Invoke("timeOut2", 2);
    }
    void timeOut2()
    {
        Application.Quit();
    }
#endif
}
