using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPartSignal : MonoBehaviour
{
    public GameObject signal;
    public GameObject receiveHitArea;
    
    public void SetParentTo(GameObject container)
    {
        signal.transform.SetParent(container.transform);
        signal.transform.localPosition = Vector3.zero;
    }
}
