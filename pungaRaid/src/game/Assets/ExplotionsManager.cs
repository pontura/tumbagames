using UnityEngine;
using System.Collections;

public class ExplotionsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Events.OnAddExplotion += OnAddExplotion;
	}
    void OnDestroy()
    {
        Events.OnAddExplotion -= OnAddExplotion;
    }

    void OnAddExplotion(int laneID, int distance)
    {
        Lanes lanes = Game.Instance.GetComponent<LevelsManager>().lanes;
        EnemySettings settings = new EnemySettings();
        lanes.AddObjectToLane("ExplosionRatis", laneID, distance, settings);
    }
}
