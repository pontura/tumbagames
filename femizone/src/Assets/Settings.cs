using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	public int totalLife = 100;
	public Vector2 limits_z;
	public int LevelsWidth = 100;
	public int limit_to_walk = 12;
	public Color[] colors;
    public int cutsceneFinalID;
    public int pixelSize = 14;

    void Start()
    {
        pixelSize = PlayerPrefs.GetInt("pixelSize", 14);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangePixelSize(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangePixelSize(-1);
    }
    void ChangePixelSize(int value)
    {       
        pixelSize += value;
        PlayerPrefs.SetInt("pixelSize", pixelSize);
        Events.PixelSizeChange();
    }

}
