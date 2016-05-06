using UnityEngine;
using System.Collections;

public class Victim : Enemy {

    public float speed;
    public states state;

    public enum states
    {
        IDLE,
        WALKING,
        STOLEN,
        CRASHED,
        READY
    }
    private Clothes clothes;
    private string currentAnim;
    private BoxCollider2D collider;


    override public void Enemy_Activate()
    {
        if (state == states.READY) return;
        loopStealing = false;
        Walk();
        anim.GetComponentInChildren<Animator>();
        anim.SetBool("WALK", true);
        collider.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {
        collider = GetComponent<BoxCollider2D>();
        clothes = GetComponent<Clothes>();
        clothes.Restart();

        distance = Game.Instance.gameManager.distance;
        speed = settings.speed;
        Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

        if (speed < 0)
            scale.x *= -1;

        transform.localScale = scale;

        Walk();
    }
    override public void Enemy_Update(Vector3 pos)
    {
        if (Time.timeScale == 0) return;
        if (state == states.READY) return;
        if (state == states.CRASHED) return;
        if (state == states.STOLEN) return;

        pos.x -= speed;
        transform.localPosition = pos;
    }
    override public void Enemy_Pooled()
    {
        anim.SetBool("WALK", false);
        state = states.IDLE;
        loopStealing = false;
    }
    public void Walk()
    {
        state = states.WALKING;
        if (Random.Range(0, 100) < 33)
            currentAnim = "victimAWalk_phone";
        else if (Random.Range(0, 100) < 66)
            currentAnim = "victimAWalk_bag";
        else
            currentAnim = "victimAWalk_normal";

        anim.Play(currentAnim);
    }
    public bool loopStealing;
    public void StealLoop_Gil()
    {
        loopStealing = true;
        Steal();
    }
    public void StealLoopEnd_Gil()
    {
        loopStealing = false;
    }
    public void Steal()
    {
        if (Game.Instance.gameManager.state == GameManager.states.ENDING) return;
        if(loopStealing) 
        {
            Invoke("Steal", 0.5f);
            state = states.WALKING;
        }
        if (state == states.STOLEN) return;
        if (state == states.CRASHED) return;
        if (state == states.IDLE) return;
        state = states.STOLEN;
        if (currentAnim == "victimAWalk_phone")
            anim.Play("victimAPung_phone", 0, 0);
        else if (currentAnim == "victimAWalk_bag")
            anim.Play("victimAPung_bag", 0, 0);
        else if (currentAnim == "victimAWalk_normal")
            anim.Play("victimAPung_normal",0,0);

        int mnultiplayerStolen = clothes.Undress();

        Events.OnScoreAdd((Random.Range(5, 10) * 10) * mnultiplayerStolen);

        Events.OnAddCoins(laneId, transform.localPosition.x, mnultiplayerStolen*10);
    }
    override public void OnCrashed()
    {
        if (state == states.CRASHED) return;
    }
    override public void OnExplote()
    {
        if (state == states.CRASHED) return;
        currentAnim = "victimADashed";
        StealLoop_Gil();
        state = states.CRASHED;
        anim.Play("victimADashed");
        collider.enabled = false;
    }
    override public void OnHeroDie() 
    {
        Vector2 scale = transform.localScale;
        if (transform.position.x > Game.Instance.GetComponent<CharacterManager>().character.transform.position.x+5)
            scale.x = Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x)*-1;
        transform.localScale = scale;

        state = states.READY;
        anim.Play("victimATemplate");
    }
    
}
