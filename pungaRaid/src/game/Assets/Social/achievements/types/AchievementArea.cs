using UnityEngine;
using System.Collections;

public class AchievementArea : Achievement {

   // public int mission;

	public void Init () {
        this.type = types.AREA;

        //if (mission < Data.Instance.userData.missionActive)
        //    Ready();
        //else
        //    AchievementsEvents.OnMissionComplete += OnMissionComplete;
	}
    void OnMissionComplete(int missionID)
    {
       // Debug.Log("missionActive" + mission + " missionID: " + missionID);

      //  if (mission == missionID)
            Ready();
    }
}
