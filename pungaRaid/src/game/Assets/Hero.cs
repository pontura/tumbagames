using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    private Animator animator;
    public states state;
    public SpriteRenderer casco;
    public SpriteRenderer transport;
    public GameObject oops;

    public enum states
    {
        IDLE,
        RUN,
        CRASH, 
        JUMP,
        DASH,
        SORETE,
        CELEBRATE,
        DEAD,        
        WIN,
        CHUMBO_RUN,
        CHUMBO_FIRE

    }
    void Start()
    {
        Events.OnOooops += OnOooops;
        Events.OnPowerUp += OnPowerUp;
        Events.StartGame += StartGame;
        Events.OnHeroCrash += OnHeroCrash;
        Events.OnHeroCelebrate += OnHeroCelebrate;
        Events.OnLevelComplete += OnLevelComplete;
        Events.OnHeroDie += OnHeroDie;
        Events.OnEndingShot += OnEndingShot;
        animator = GetComponent<Animator>();
        Events.OnOooops(false);
    }
    void OnDestroy()
    {
        Events.OnOooops -= OnOooops;
        Events.OnPowerUp -= OnPowerUp;
        Events.StartGame -= StartGame;
        Events.OnHeroCrash -= OnHeroCrash;
        Events.OnHeroDie -= OnHeroDie;
        Events.OnHeroCelebrate -= OnHeroCelebrate;
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnEndingShot -= OnEndingShot;
    }
    void OnOooops(bool show)
    {
        if(state != states.DEAD)
            oops.SetActive(show);
    }
    void OnPowerUp(PowerupManager.types type)
    {
        if (Game.Instance.characterManager.character.powerupManager.type == PowerupManager.types.CHUMBO)
            ChumboRun();
        else
        {
            state = states.RUN;
            animator.Play("pungaRun", 0, 0);
        }
    }
    void StartGame()
    {
        Run();
    }
    void OnHeroCrash()
    {
        Crash();
    }
    void OnHeroCelebrate()
    {
        Celebrate();
    }
    void OnHeroDie()
    {
        if (state == states.DEAD) return;
        oops.SetActive(false);
        state = states.DEAD;
        animator.SetBool(state.ToString(), true);
        print("OnHeroDie");
        animator.Play("pungaDeath", 0, 0);
    }
    public void ResetState()
    {
        state = states.RUN;
    }
    public void OnHeroJump(string animName)
    {
      //  print("OnHeroJump Game.Instance.state " + Game.Instance.state + " state:" + state + " byPowerUp " + byPowerUp);

        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.JUMP) return;
        if (state == states.CRASH) return;
        state = states.JUMP;
        animator.Play(animName, 0, 0);
        Invoke("ResetJump", 1.4f);
        Events.OnVulnerability(true);
    }
    void ResetJump()
    {
        Events.OnVulnerability(false);
        EndAnimation();
    }
    public void OnHeroDash()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.DASH) return;
        if (state == states.CRASH) return;
        state = states.DASH;
        animator.SetBool(state.ToString(), true);
        animator.Play("pungaDash", 0, 0);
    }
    public void OnSorete()
    {
        if (Game.Instance.state != Game.states.PLAYING) return;
        if (state == states.CRASH) return;
        state = states.SORETE;
      //  animator.SetBool(state.ToString(), true);
        if(Game.Instance.characterManager.character.powerupManager.type == PowerupManager.types.CHUMBO)
            animator.Play("pungaShitMegachumbo", 0, 0);
        else
            animator.Play("pungaShit", 0, 0);

        Invoke("EndAnimation", 0.6f);
        Events.OnSoundFX("Shit");
    }
    public void EndAnimation()
    {
        switch (state)
        {
            case states.JUMP:           Run();          break;
            case states.SORETE:         Run();          break;
            case states.DASH:           Run();          break;
            case states.CRASH:          Run();          break;
            case states.CHUMBO_FIRE:    ChumboRun();    break;
        }
    }
    void Crash()
    {
        if (state == states.CRASH) return;
        state = states.CRASH;
        animator.Play("pungaCrash",0,0);
    }
    public void Run()
    {
        if (Data.Instance.specialItems.type == SpecialItemsManager.types.TRANSPORT)
        {
            state = states.RUN;
            animator.Play("pungaSkate", 0, 0);
        } else
        if (
            Game.Instance.characterManager.character.powerupManager.type == PowerupManager.types.NONE
            || Game.Instance.characterManager.character.powerupManager.type == PowerupManager.types.GIL
        )
        {
            state = states.RUN;
            animator.Play("pungaRun", 0, 0);
        }
        else if (Game.Instance.characterManager.character.powerupManager.type == PowerupManager.types.CHUMBO)
            ChumboRun();
    }
    public void ChumboRun()
    {
        state = states.CHUMBO_RUN;
        animator.Play("pungaRunMegachumbo", 0, 0);
    }
    public void ChumboFire()
    {
        state = states.CHUMBO_FIRE;
        animator.Play("pungaFireMegachumbo", 0, 0);
        Invoke("EndAnimation", 0.5f);
        Events.OnSoundFX("megachumbo");
    }
    void OnEndingShot()
    {
        int rand = Random.Range(1, 4);
        animator.Play("pungaShot" + rand, 0, 0);
    }
    void Celebrate()
    {

    }
    void OnLevelComplete()
    {

    }
    public void Dash()
    {

    }
    public void Die()
    {

    }
    public void ResetAnimation()
    {

    }
}
