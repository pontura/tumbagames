using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaSound.RetroPixelPro;

public class WorldCamera : MonoBehaviour
{
    public int pixelSize;

    void Start()
    {
        int total = Screen.height + Screen.width;

        int pizelSizeByScreen = (total * 7) / (1920 + 1080);
        GetComponent<RetroPixelPro>().pixelSize = pizelSizeByScreen + pixelSize;
    }
    public void UpdatePosition(float _x)
    {
        Vector3 pos = transform.position;
        Vector3 newPos = pos;
        newPos.x += _x;
        transform.position = Vector3.Lerp(pos, newPos, 0.1f);
    }
}
