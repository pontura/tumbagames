using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgressBarManager : MonoBehaviour {

	public ProgressBar progressBarToInstantiate;
	public Transform container;
	public List<ProgressBar> all;

	public ProgressBar CreateProgressBar(Character character) {
		ProgressBar progressBar = Instantiate( progressBarToInstantiate);
		progressBar.transform.SetParent (container);
		progressBar.Init (character.GetComponent<Enemy>());
		all.Add (progressBar);
		return progressBar;
	}

}
