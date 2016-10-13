using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

    public int vidas = 3;
    public GameObject[] vidasAssets;
    public Image progressBar;
    public float progress;
    public float totalLength = 1000;
    public Game game;
    public GameObject gameOver;

	void Start () {
        Events.OnHeroDie += OnHeroDie;
        Events.Restart += Restart;
	}
	
	void OnDestroy () {
        Events.OnHeroDie -= OnHeroDie;
        Events.Restart -= Restart;
	}
    void Restart()
    {
        progress = 0;
        progressBar.fillAmount = 0;
    }
    void OnHeroDie()
    {
        vidas--;
        int id = 1;
        foreach (GameObject vida in vidasAssets)
        {
            if (vidas < id)
                vida.SetActive(false);
            id++;
        }
        if (vidas == 0)
        {
            Events.GameOver();
            gameOver.SetActive(true);
        }
    }
    void Update()
    {
        if (game.state == Game.states.READY) return;
        progress = game.distance / totalLength;
        progressBar.fillAmount = progress;
        if (progress > 0.98f)
        {
            Events.OnLevelComplete();
            progressBar.fillAmount = 1;
        }
    }
}
