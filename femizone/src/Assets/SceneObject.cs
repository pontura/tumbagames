using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

    [HideInInspector]
    public SceneObjectData data;

    public void Init(SceneObjectData data)
    {
        this.data = data;
        OnInit();
    }
    public virtual void OnInit()
    {

    }
}
