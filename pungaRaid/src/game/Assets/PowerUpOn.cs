using UnityEngine;
using System.Collections;

public class PowerUpOn : MonoBehaviour {

    public string clip_init;
    public string clip_loop;
   // public int lives;
   // private int totalLives;

    void Start()
    {
        Events.OnHeroCrash += OnHeroCrash;
    }
    void OnDestroy()
    {
        Events.OnHeroCrash -= OnHeroCrash;
    }
    void OnHeroCrash()
    {
        Events.OnExplotion();
    }
    public void Init(int lives)
    {
        OnInit();
    }
    public virtual void OnInit() { }

}
