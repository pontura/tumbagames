﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

    SceneObjectData data;

    public void Init(SceneObjectData data)
    {
        this.data = data;
    }
}
