using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IADickPetrol : IA
{
    public AnimationClip attack1;
    public AnimationClip attack2;
    public HitPartSignal hitPartSignal;

    CharacterStats characterStats;
    public int id = 0;
    int totalLife;
    int totalParts;

    Vector3 pos;
    float offsetRun;
    float runDuration = 1f;
    public Vector3 originalPos;
    int forceIdle = 5;

    void Delayed()
    {
        originalPos = transform.parent.transform.position;
        enemy.anim.Play("intro");
        SetForce(forceIdle);
    }

    public override void OnInit()
    {
        characterStats = GetComponent<CharacterStats>();
        totalLife = characterStats.life;
        SetStatesByID();
        Invoke("Delayed", 1);
    }
    public override void ReceiveHit()
    {
        if (state == states.HITTED)
            return;
        CancelInvoke();
        state = states.HITTED;
        if (Random.Range(0, 10) < 5)
            Attack();
        else
            Invoke("Idle", 0.3f);
        float value = (float)characterStats.life * (float)totalParts / (float)totalLife;
        id = (int)(totalParts - value);
        SetStatesByID();
    }
    void SetStatesByID()
    {
        if (id >= totalParts)
            return;
        print("Boss " + id);
     
    }
    public override void OnDie()
    {
        enemy.anim.Play("death");
        StartCoroutine(DieDelayed());
    }
    IEnumerator DieDelayed()
    {
        yield return new WaitForSeconds(5);
        if (enemy != null)
            Destroy(enemy);
    }
    public override void READY_FOR_FIGHT()
    {
        if (state != states.READY_FOR_FIGHT)
            return;
        CancelInvoke();
        Run();
    }
    float lastTimeAttacked;
    float delayed = 18;
    void SetForce(int value)
    {
        enemy.hitArea.force = value;
    }
    void Run()
    {
       
        if (Time.time < (lastTimeAttacked + delayed))
            Idle();
        else
            Attack();
    }
    void Attack()
    {
        lastTimeAttacked = Time.time;
        print("Run");
        enemy.anim.Play("walk");
        CancelInvoke();
        pos = enemy.transform.position;
        enemy.transform.DOMove(originalPos, runDuration).OnComplete(OnSitu).SetEase(Ease.Linear);
    }
    void OnSitu()
    {
        enemy.anim.Play("pre_attack");
        Invoke("OnSituAttack", 2);
    }
    void OnSituAttack()
    {
        SetForce(30);
        enemy.anim.Play("attack_1");
        Invoke("Vulnerable", 5);
    }
    void Vulnerable()
    {
        SetForce(forceIdle);
        enemy.anim.Play("intro");
        Invoke("RunBack", 2);
    }
    void RunBack()
    {
        if (!enemy.isActiveAndEnabled)
            return;
        enemy.anim.Play("walk");
        enemy.transform.DOMove(pos, runDuration).OnComplete(Idle).SetEase(Ease.Linear);
    }
}