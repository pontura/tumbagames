using UnityEngine;
using System.Collections;

public class AchievementUnlock : Achievement {

    public int missionID;


	public override void OnInit () {
		this.type = types.UNLOCK;
		AchievementsEvents.OnUnlock += OnUnlock;
    }
	void OnUnlock(int _moodID, int _seccionalID)
    {
		if (_moodID == moodID && _seccionalID == seccionalID) {
			Ready ();
			Save (1);
			AchievementsEvents.OnUnlock -= OnUnlock;
		}
    }
}
