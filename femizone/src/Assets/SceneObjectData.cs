using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectData : MonoBehaviour {

    public types type;

    [HideInInspector]
    public Vector3 pos;
    public float scaleX = 1;
    public enum types
    {
        WARNES_MAN,
        OPUS,
        CEO,
        COP,
		COP_GUN,
		ENERGY,
		BOSS,
		WEAPON,
		FIRE,
		HIPSTER,
		BANCO,
		FAROL,
		ARBUSTO,
		MODERNO,
        CAR,
        CINTURONGA,
        EXHIBICIONISTA,
        TRAPPER,
        ARBOL,
        KKK,
        ESVASTICA,
        SCRUM_MASTER,
        RUGBIER
    }

    public float aditionalSpeed;
}
