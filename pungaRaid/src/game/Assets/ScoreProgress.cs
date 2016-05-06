using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreProgress : MonoBehaviour {

    [SerializeField]
    Text label;

    void Start()
    {
        Events.OnRefreshScore += OnRefreshScore;        
    }
    void OnDestroy()
    {
        Events.OnRefreshScore -= OnRefreshScore;
    }
    public void OnRefreshScore(int score)
    {
        label.text = "$" + score.ToString();
    }
}
