using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

    void Start()
    {
        Cursor.visible = false;
#if UNITY_STANDALONE
        Screen.fullScreen = true;        
#endif
        Invoke("GotoGame", 7);
    }
	public void GotoGame () {
        Data.Instance.LoadLevel("02_Intro");
	}
    public void OnSettings()
    {
        Events.OnSettings();
    }
}
