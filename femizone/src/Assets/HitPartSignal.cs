using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPartSignal : MonoBehaviour
{
    public GameObject signal;
    public GameObject receiveHitArea;

    void Start()
    {
        Loop();
    }
    
    void Loop()
    {
        if(receiveHitArea.activeSelf)
        {
            signal.SetActive(true);
        }
        else
        {
            signal.SetActive(false);
        }
        Invoke("Loop", 0.25f);
    }
    public void SetParentTo(GameObject container)
    {
        signal.transform.SetParent(container.transform);
        signal.transform.localPosition = Vector3.zero;
    }
}
