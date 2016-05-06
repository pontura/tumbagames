using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    
    [SerializeField]
    ScoreProgress scoreProgress;

    [SerializeField]
    GameObject menuButton;


   public void Init()
    {
        Events.OnLevelComplete += OnLevelComplete;
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
    }
    public void OnPauseButton()
    {
        Events.OnGamePaused(true);
    }
    void OnLevelComplete()
    {
        //scoreProgress.gameObject.SetActive(false);
        //menuButton.gameObject.SetActive(false);
    }
}
