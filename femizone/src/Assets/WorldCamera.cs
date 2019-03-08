using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaSound.RetroPixelPro;

public class WorldCamera : MonoBehaviour
{

    void Start()
    {
        int total = Screen.height + Screen.width;

        int pizelSize = (total * 7) / (1920 + 1080);
        print("(" + total + "*7 /3000) = " + pizelSize);
        GetComponent<RetroPixelPro>().pixelSize = pizelSize;
    }
    public void UpdatePosition(float _x)
    {
        Vector3 pos = transform.position;
        Vector3 newPos = pos;
        newPos.x += _x;
        transform.position = Vector3.Lerp(pos, newPos, 0.1f);
    }
}
