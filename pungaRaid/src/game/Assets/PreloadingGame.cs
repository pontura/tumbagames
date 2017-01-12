using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreloadingGame : MonoBehaviour {
    
    public Text field;

	void Start () {
        Invoke("StartGame", 5);
        Resources.UnloadUnusedAssets();
        field.text = Data.Instance.texts.GetRandomText(Data.Instance.texts.intros);
	}
    void StartGame()
    {
        Data.Instance.LoadLevel("04_Game");
    }
    void Update()
    {
        if(Data.Instance.musicManager.disabled) return;
        Data.Instance.musicManager.audioSource.volume -= Time.deltaTime/2;
        if (Data.Instance.musicManager.audioSource.volume <= 0)
        {
            Events.OnMusicChange("");
            Data.Instance.musicManager.audioSource.volume = 1;
        }
    }
    void OnDestroy()
    {
        Events.OnMusicChange("");
        if (Data.Instance.musicManager.disabled) return;
        Data.Instance.musicManager.audioSource.volume = 1;
    }
}
