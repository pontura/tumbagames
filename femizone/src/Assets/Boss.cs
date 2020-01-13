using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int rigidBodyMass;
    public float colliderRadius;
    void Start()
    {
        IA ia =  GetComponent<IA>();
        if (ia.enemy == null)
            return;
        Rigidbody rb = ia.enemy.GetComponent<Rigidbody>();
        rb.mass = rigidBodyMass;
        SphereCollider col = ia.enemy.GetComponent<SphereCollider>();
        col.radius = colliderRadius;
    }
}
