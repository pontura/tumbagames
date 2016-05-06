using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    
    public GameObject button;
    public GameObject loadingAsset;
    public GameObject Privacy;
    public GameObject PrivacyButton;
    private string nextScene = "game";

    void Start()
    {
    }
    private bool privacyOpen;
    void Update()
    {
    }
    void OnDestroy()
    {
        Events.OnLoadSceneReady -= OnLoadSceneReady;
    }
    void OnLoadSceneReady()
    {
        loadingAsset.SetActive(false);
        button.SetActive(true);
    }
   
    public void GotoGame()
    {
        Events.OnSceneLoad(nextScene);
    }

}
