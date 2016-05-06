using UnityEngine;
using System.Collections;

public class CoinsParticlesManager : MonoBehaviour {

    private LevelsManager levelsManager;

	void Start () {
       levelsManager = GetComponent<LevelsManager>();
       Events.OnAddCoins += OnAddCoins;
	}
    void OnDestroy()
    {
        Events.OnAddCoins -= OnAddCoins;
    }
    void OnAddCoins(int laneID, float distance, int mnultiplayerStolen)
    {
      //  print("OnAddCoins  lane: " + laneID + " distance: " + distance);
        EnemySettings settings = new EnemySettings();
        settings.qty = mnultiplayerStolen;
        levelsManager.lanes.AddObjectToLane("CoinParticles", laneID, (int)distance, settings);
    }
}
