using UnityEngine;
using System.Collections;

public class AchievementMoney : Achievement {

    public int missionID;


	public override void OnInit () {
		this.type = types.MONEY;
		AchievementsEvents.OnCheckMoney += OnCheckMoney;
    }
	void OnCheckMoney(int _moodID, int _seccionalID, float score)
    {
		if (moodID == 0) {
			if (score > pointsToBeReady) {
				Done (score);
			}
		} else if (_moodID == moodID && _seccionalID == seccionalID) {

			//Debug.Log ("OnCheckScore moodID: " + moodID + " secicon: " + seccionalID);
			//Debug.Log ("score: " + score + " tenias que hacer: " + pointsToBeReady);

			if (score > pointsToBeReady) {
				Done (score);
			}
		}
    }
	void Done(float score)
	{
		Ready ();
		Save ((int)score);
		AchievementsEvents.OnCheckMoney -= OnCheckMoney;
	}
}
