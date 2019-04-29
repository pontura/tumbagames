using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgressBarManager : MonoBehaviour {

	public ProgressBar progressBarToInstantiate;
	public Transform container;
	public List<ProgressBar> all;

    void Start()
    {
        Events.GameOver += GameOver;
    }

    void OnDestroy()
    {
        Events.GameOver -= GameOver;
    }
    void GameOver()
    {
        foreach (ProgressBar pb in all)
        {
            if(pb != null )
                pb.gameObject.SetActive(false);
        }
    }
    public ProgressBar CreateProgressBar(Character character) {
		ProgressBar progressBar = Instantiate( progressBarToInstantiate);
		progressBar.transform.SetParent (container);
		progressBar.Init (character.GetComponent<Enemy>());
		all.Add (progressBar);
		return progressBar;
	}

}
