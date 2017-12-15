﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectData : MonoBehaviour {

    public types type;

    [HideInInspector]
    public Vector3 pos;
    public enum types
    {
        WARNES_MAN,
        OPUS,
        CEO,
        COP
    }
}