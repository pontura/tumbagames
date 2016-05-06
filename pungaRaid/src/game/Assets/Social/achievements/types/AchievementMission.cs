using UnityEngine;
using System.Collections;

public class AchievementMission : Achievement {

    public int mission;

	public void Init () {
        this.type = types.MISSION_COMPLETE;

        //if (mission < Data.Instance.userData.missionActive)
        //    Ready();
        //else
        //    AchievementsEvents.OnMissionComplete += OnMissionComplete;
	}
    void OnMissionComplete(int missionID)
    {
        Debug.Log("missionActive" + mission + " missionID: " + missionID);

        if (mission == missionID)
            Ready();
    }
}
