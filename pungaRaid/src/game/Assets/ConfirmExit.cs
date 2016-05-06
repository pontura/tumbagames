using UnityEngine;
using System.Collections;

public class ConfirmExit : MonoBehaviour {

    public GameObject canvas;
    private string next;
    void Start()
    {
        canvas.SetActive(false);
    }
    public void Init(string next)
    {
        Events.OnSoundFX("warningPopUp");
        this.next = next;
        canvas.SetActive(true);
        Events.OnSoundFX("25_AreYouSureYouWantToQuit");
    }
    public void Close()
    {
        Events.OnSoundFX("backPress");
        canvas.SetActive(false);
        Events.OnGamePaused(false);
    }
    public void Restart()
    {
        Close();
        
        if (next == "03_LevelSelector")
        {
            Time.timeScale = 1;
            Data.Instance.LoadLevel("03_LevelSelector");
        }
        else
        {
            Events.OnGameRestart();
        }

    }
}
