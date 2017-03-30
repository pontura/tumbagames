using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementActionWhilePowerup : Achievement {

	public actions action;

	public enum actions
	{
		STEAL_WHILE_GIL,
		DASH_WHILE_GIL,
		CRASH_WHILE_MOTO
	}
	public override void OnInit () {
		this.type = types.ACTION_WHILE_POWERUP;
	}
	public override void SetAction(string _action)
	{
		Debug.Log("SetAction + ____________________________" + _action );
		switch(_action)
		{
		case "STEAL_WHILE_GIL":
			action = actions.STEAL_WHILE_GIL; break;
		case "DASH_WHILE_GIL":
			action = actions.DASH_WHILE_GIL; break;
		case "CRASH_WHILE_MOTO":
			action = actions.CRASH_WHILE_MOTO;
			break;
		}
		AchievementsEvents.OnActionWhilePowerUPComputed += OnActionWhilePowerUPComputed;
	}
	void OnActionWhilePowerUPComputed(actions _action, int pungs)
	{
		Debug.Log(IsInThisMood() + "____________________________" + action + "pun: " + pungs + " pointsToBeReady: " + pointsToBeReady );

		if (!IsInThisMood())
			return;
		if (_action != action)
			return;
		if (pungs >= pointsToBeReady)
			Done (pungs);
	}
	bool IsInThisMood()
	{
		MoodsManager mm = Data.Instance.moodsManager;
		int _moodID = mm.GetCurrentMoodID();
		int _seccionalID = mm.GetCurrentSeccional().id;
		if (moodID == 0)
			return true;
		if(_moodID == moodID && _seccionalID == seccionalID)
			return true;
		return false;
	}
	void Done(float score)
	{
		Ready ();
		SaveMultipleData ();
		AchievementsEvents.OnActionWhilePowerUPComputed -= OnActionWhilePowerUPComputed;
	}
}
