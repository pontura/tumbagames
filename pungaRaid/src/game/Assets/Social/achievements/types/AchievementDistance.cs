using UnityEngine;
using System.Collections;

public class AchievementDistance : Achievement {

	public override void OnInit () {
        this.type = types.DISTANCE;
        AchievementsEvents.OnNewDistance += OnNewDistance;
    }
	void OnNewDistance(int _moodID, int _seccionalID, float distanceTraveled)
    {
		if (_moodID == moodID && _seccionalID == seccionalID) {
			if (distanceTraveled > pointsToBeReady) {
				Ready ();
				Save ((int)distanceTraveled);
				AchievementsEvents.OnNewDistance -= OnNewDistance;
			}
		}
    }
}
