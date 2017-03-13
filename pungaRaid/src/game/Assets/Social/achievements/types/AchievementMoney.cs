using UnityEngine;
using System.Collections;

public class AchievementMoney : Achievement {

    public int missionID;


	public override void OnInit () {
		this.type = types.MONEY;

		Debug.Log ("new achievemtnen mONEWY=");
        //if (pointsToBeReady < Data.Instance.userData.distanceTraveled)
        //    Ready();
        //else
		AchievementsEvents.OnCheckScore += OnCheckScore;
    }
	void OnCheckScore(int _moodID, int _seccionalID, float score)
    {
		if (_moodID == moodID && _seccionalID == seccionalID) {

			//Debug.Log ("OnCheckScore moodID: " + moodID + " secicon: " + seccionalID);
			//Debug.Log ("score: " + score + " tenias que hacer: " + pointsToBeReady);

			if (score > pointsToBeReady) {
				//Debug.Log ("AchievementMoney READY !!!!!!!!!!! ");
				Ready ();
				Save ((int)score);
				AchievementsEvents.OnCheckScore -= OnCheckScore;
			}
		}
    }
}
