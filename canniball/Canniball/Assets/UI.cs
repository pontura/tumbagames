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

	void Start () {
        Events.OnHeroDie += OnHeroDie;
	}
	
	void OnDestroy () {
        Events.OnHeroDie -= OnHeroDie;
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
