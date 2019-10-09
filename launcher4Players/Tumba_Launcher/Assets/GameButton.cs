using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{

    public Animation anim;
    public void SetOn(bool isOn)
    {
        if (isOn)
            anim.Play("on");
        else
            anim.Play("off");
    }
}
