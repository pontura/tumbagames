using UnityEngine;
using System.Collections;

public class LoadingPanel : MonoBehaviour {

    public GameObject panel;

	void Start () {
        Events.OnLoadingPanel += OnLoadingPanel;
        panel.SetActive(false);
	}
    void OnDestroy()
    {
        Events.OnLoadingPanel -= OnLoadingPanel;
    }
	
	// Update is called once per frame
    void OnLoadingPanel()
    {
        panel.SetActive(true);
	}
}
