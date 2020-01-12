using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IA_MutipleParts : IA
{
    public AnimationClip[] idles;
    public AnimationClip[] receiveHitAnims;
    public GameObject[] parts;
    public GameObject[] deadParts;
    public HitPartSignal hitPartSignal;

    CharacterStats characterStats;
    public int id = 0;
    int totalLife;
    int totalParts;

    Vector3 origin;
    float offsetRun;
    float runDuration = 1.5f;

    public override void OnInit()
    {
        totalParts = parts.Length;
        characterStats = GetComponent<CharacterStats>();
        totalLife = characterStats.life;
        SetStatesByID();
    }
    public override void ReceiveHit()
    {
        base.ReceiveHit();
        float value = (float)characterStats.life * (float)totalParts / (float)totalLife;
        id = (int)(totalParts - value);
        SetStatesByID();
    }
    void SetStatesByID()
    {
        
        if (id > 0 && id<totalParts+1)
        {
            parts[id - 1].SetActive(false);
            deadParts[id - 1].SetActive(true);
        }
        if (id >= totalParts)
            return;
        print("Boss " + id);
        characterStats.idle_clips[0] = idles[id];
        foreach (AttackStyle ats in characterStats.receivedAttacks)
            ats.animClip = receiveHitAnims[id];
        hitPartSignal.SetParentTo(parts[id]);
    }
    public override void OnDie()
    {
        enemy.anim.Play("death");
        StartCoroutine(DieDelayed());
    }
    IEnumerator DieDelayed()
    {
        yield return new WaitForSeconds(5);
        if(enemy != null)
        Destroy(enemy);
    }
    public override void READY_FOR_FIGHT()
    {
        if (state != states.READY_FOR_FIGHT)
            return;
        CancelInvoke();
        LookToTarget();
        enemy.Attack();
        Invoke("Run", 1.25f);
    }
    
    void Run()
    {
        enemy.anim.Play("attack");
        CancelInvoke();
        if (transform.localScale.x < 0)
            offsetRun = -25;
        else
            offsetRun = 25;
        origin = transform.position;
        Vector3 dest = transform.position;
        dest.x -= offsetRun;
        this.transform.DOMove(dest, runDuration).OnComplete(RunBack).SetEase(Ease.Linear);
    }
    void RunBack()
    {
        enemy.anim.Play("attack");
        transform.position = new Vector3(origin.x+offsetRun, origin.y, origin.z);
        this.transform.DOMove(origin, runDuration).OnComplete(Idle).SetEase(Ease.Linear);
    }
}
