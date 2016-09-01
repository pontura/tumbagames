using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    
    [SerializeField]
    ScoreProgress scoreProgress;

    [SerializeField]
    GameObject menuButton;

    public Animator anim;

    void Awake()
    {
        anim.gameObject.SetActive(true);
        anim.Play("startGameRedPanel", 0, 0);
    }
   public void Init()
    {
        Events.OnLevelComplete += OnLevelComplete;
        
        anim.Play("startGameRedPanel",0,0);
        Invoke("ResetRedPanel", 1);
    }
   void ResetRedPanel()
   {
       anim.gameObject.SetActive(false);
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
