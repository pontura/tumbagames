using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSteal : Achievement {

	public override void OnInit () {
		this.type = types.STEAL;
		AchievementsEvents.OnPungComputed += OnPungComputed;
	}
	void OnPungComputed(int pungs)
	{
		MoodsManager mm = Data.Instance.moodsManager;
		int _moodID = mm.GetCurrentMoodID();
		int _seccionalID = mm.GetCurrentSeccional().id;

		//Debug.Log ("OnPungComputed moodID: " + _moodID + " secicon: " + _seccionalID + " score: " + pungs);
		if (moodID == 0) {
			if (pungs > pointsToBeReady) {
				Done (pungs);
			}
		} else if (_moodID == moodID && _seccionalID == seccionalID) {

			//Debug.Log ("OnCheckScore moodID: " + moodID + " secicon: " + seccionalID);
			//Debug.Log ("score: " + pungs + " tenias que hacer: " + pointsToBeReady);

			if (pungs > pointsToBeReady) {
				Done (pungs);
			}
		}
	}
	void Done(float score)
	{
		Ready ();
		Save ((int)score);
		AchievementsEvents.OnPungComputed -= OnPungComputed;
	}
}
