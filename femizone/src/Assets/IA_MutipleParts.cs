using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
