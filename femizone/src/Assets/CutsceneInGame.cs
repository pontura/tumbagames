using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneInGame : MonoBehaviour
{
    public types type;
    public enum types
    {
        GAME_OVER,
        RUGBIERS,
        FETO
    }
    public void Off()
    {
        GetComponent<Animator>().Play("off");
    }
}
