using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wearable : MonoBehaviour
{
    Animation anim;
    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void Attack(float duration)
    {
        anim.Play("attack");
        Invoke("Reset", duration);
    }
    void Reset()
    {
        anim.Play("idle");
    }
}
