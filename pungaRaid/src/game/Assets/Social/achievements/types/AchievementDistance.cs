using UnityEngine;
using System.Collections;

public class AchievementDistance : Achievement {

    public int missionID;

	public void Init () {
        this.type = types.DISTANCE;

        //if (pointsToBeReady < Data.Instance.userData.distanceTraveled)
        //    Ready();
        //else
        //    AchievementsEvents.OnNewDistance += OnNewDistance;
    }
    void OnNewDistance(int distanceTraveled)
    {
        if (distanceTraveled > pointsToBeReady)
        {
            Ready();
            AchievementsEvents.OnNewDistance -= OnNewDistance;
        }
    }
}
