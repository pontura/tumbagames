using UnityEngine;
using System.Collections;

public class AchievementArea : Achievement {

	public override void OnInit () {
        this.type = types.AREA;
		AchievementsEvents.OnAreaPlayed += OnAreaPlayed;
	}
	void OnAreaPlayed(int _moodID, int _seccionalID)
	{
		if (_moodID == moodID && _seccionalID == seccionalID) {
			Ready ();
			Save (1);
			AchievementsEvents.OnAreaPlayed -= OnAreaPlayed;
		}
    }
}
