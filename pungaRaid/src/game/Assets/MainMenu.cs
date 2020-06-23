using UnityEngine;
using System.Collections;

using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void Connect()
    {
        Events.OnLoginAdvisor();
    }
   
    public void GotoGame () {
		print ("tutorialReady =  " + PlayerPrefs.GetString ("tutorialReady"));
		if (PlayerPrefs.GetString ("tutorialReady") == "true")
			Data.Instance.LoadLevel ("02_Main");
		else {
			Events.OnLoadCurrentAreas ();
			Data.Instance.LoadLevel ("03_PreloadingGame");
		}
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
        PlayerPrefs.SetString("tutorialReady", "true");
        Events.OnHeroDie += OnHeroDie;
    }
	void OnDestroy()
	{
		Events.OnHeroDie -= OnHeroDie;
	}
    void OnHeroDie()
    {
        isDead = true;
    }
	bool ended;
    void Update()
    {
		if (ended)
			return;
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {			
			print ("CLICK  isDead: " + isDead + " ended: " + ended);
            if (isDead)
            {
				ended = true;
                SaveNew();
            } else if (SceneManager.GetActiveScene().name == "02_Intro")
            {
                Data.Instance.LoadLevel("12_ArcadeMain");
            }
        }
    }
    public void SaveNew()
    {
		Events.OnLoadingPanel();
		isDead = false;
       
        string[] content = new string[] { "PR_" + Game.Instance.gameManager.score };
        File.WriteAllLines("C:\\tumbagames\\hiscores\\data.txt", content);
		Invoke("timeOut1", 1);
    }
    void timeOut1()
    {
		Events.OnPoolAllItemsInScene();
		Invoke("timeOut2", 1.5f);
    }
    void timeOut2()
    {	
		ended = false;	
		if (Game.Instance.gameManager.score > 1000) {
			Data.Instance.LoadLevel ("11_Hiscores");
		} else {
			Data.Instance.LoadLevel ("02_Intro");
		}
    }
#endif
}
