using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    int posX;
    void Start()
    {
        posX = Data.Instance.gameData.CharacterXPosition;
    }
    public void UpdatePosition(float _x)
    {
        Vector3 pos =  transform.localPosition;
        pos.x = _x + posX;
        transform.localPosition = pos;
	}
}
