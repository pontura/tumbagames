using UnityEngine;
using System.Collections;

public class FeedbackManager : MonoBehaviour {

    public GameObject feedbackSignal;

    [SerializeField]
    GameObject container;

	void Start () {
       // Events.OnPlayerHitWord += OnPlayerHitWord;
	}

    void OnDestroy()
    {
        //Events.OnPlayerHitWord -= OnPlayerHitWord;
    }

  
}
