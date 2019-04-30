using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGirls : MonoBehaviour
{
    void OnEnable()
    {
        Loop();
    }
    void Loop()
    {
        Invoke("Loop", 2);
        float girlsXPos = World.Instance.heroesManager.GetPercentPosition();
        if (girlsXPos == 0)
            return;
        print("loop " + girlsXPos + "  " + transform.position.x);

        Vector2 scale = transform.localScale;
        if (girlsXPos > transform.position.x)
            scale.x = -1;
        else
            scale.x = 1;

        transform.localScale = scale;
    }
    void OnDisable()
    {
         CancelInvoke();
    }
}
