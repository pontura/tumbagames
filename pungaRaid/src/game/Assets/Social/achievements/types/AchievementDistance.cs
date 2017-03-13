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
		//	Debug.Log ("DISTANCE ACH_______ : " + distanceTraveled);
			if (distanceTraveled > pointsToBeReady) {
			//	Debug.Log ("DISTANCE READY !!!!!!!!!!! ");
				Ready ();
				Save ((int)distanceTraveled);
				AchievementsEvents.OnNewDistance -= OnNewDistance;
			}
		}
    }
}
