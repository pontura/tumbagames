using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlpacaSound.RetroPixelPro;

public class WorldCamera : MonoBehaviour
{
    public int factor = 1;

    void Start()
    {
        Events.PixelSizeChange += PixelSizeChange;
        PixelSizeChange();
    }
    void OnDestroy()
    {
        Events.PixelSizeChange -= PixelSizeChange;
    }
    void PixelSizeChange()
    {
        GetComponent<RetroPixelPro>().pixelSize = Data.Instance.settings.pixelSize/factor;
    }
    public void UpdatePosition(float _x)
    {
        Vector3 pos = transform.position;
        Vector3 newPos = pos;
        newPos.x += _x;
        transform.position = Vector3.Lerp(pos, newPos, 0.1f);
    }
}
