using UnityEngine;
using System.Collections;

public class LevelsData : MonoBehaviour {

    public int level = 1;

	void Start () {
        Events.OnLevelComplete += OnLevelComplete;
	}
	
    void OnLevelComplete()
    {
        level++;
	}
}
