using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public states state;
    public enum states
    {
        IDLE,
        WALKING,
        RUNNING,
        JUMPING,
        DIE,
        READY
    }
    private Animator anim;
    void Start()
    {
        Events.OnHeroComido += OnHeroComido;
        Events.OnLevelComplete += OnLevelComplete;
        anim = GetComponent<Animator>();
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnHeroComido -= OnHeroComido;
    }
    void OnLevelComplete()
    {
        if (state == states.READY) return;
        state = states.READY;
        anim.Play("tryJump");
    }
    void OnHeroComido()
    {
        print("Comido");
        anim.Play("dead");
    }
    public void SetSpeed(float speed)
    {
        if (state == states.READY) return;
        if (state == states.DIE) return;
        if (state == states.JUMPING) return;
        if (speed == 0 ) Idle();
        else if (speed > 4) Run();
        else if (speed > 0) Walk();
    }
    public void Idle()
    {
        if (state == states.READY) return;
        if (state == states.IDLE) return;
        state = states.IDLE;
        anim.Play("idle");
    }
    public void Die()
    {
        if (state == states.DIE) return;
        Events.OnHeroDie();
        state = states.DIE;
        anim.Play("die");
    }
    public void Walk()
    {
        if (state == states.WALKING) return;
        state = states.WALKING;
        anim.Play("walk");
    }
    public void Run()
    {
        if (state == states.RUNNING) return;
        state = states.RUNNING;
        anim.Play("run");
    }
    public void Jump()
    {
        if (state == states.DIE) return;
        if (state == states.JUMPING) return;
        state = states.JUMPING;
        anim.Play("jump");
        Invoke("ResetJump", 0.8f);
    }
    void ResetJump()
    {
        if (state == states.READY) return;
        if (state == states.DIE) return;
        state = states.IDLE;
    }

     void OnTriggerEnter2D(Collider2D other) {
         if (state == states.JUMPING) return;
         Die();
    }
}
