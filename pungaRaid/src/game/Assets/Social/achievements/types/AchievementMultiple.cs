using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementMultiple : Achievement
{
	int time_killed = 0;

	public override void OnInit()
    {
		this.type = types.MULTIPLE;
        AchievementsEvents.OnNewDeadComputed += OnNewDeadComputed;
		AchievementsEvents.OnNewDashComputed += OnNewDashComputed;
		AchievementsEvents.NewSecondKilled += NewSecondKilled;
    }
	void Reset()
	{
		AchievementsEvents.OnNewDeadComputed -= OnNewDeadComputed;
		AchievementsEvents.OnNewDashComputed -= OnNewDashComputed;
		AchievementsEvents.NewSecondKilled -= NewSecondKilled;
	}
	void NewSecondKilled()
	{
		time_killed++;
		OnCheckIfMultipleAchievementsAreDone ();
	}
	void OnCheckIfMultipleAchievementsAreDone()
	{
		if (ready)
			Reset ();
		foreach (MultipleData mdata in multipleData) {
			
			if (mdata.type == types.DASH) {
				//Debug.Log (mdata.type + " pointsToBeReady:" + mdata.pointsToBeReady + " " + AchievementsManager.Instance.achievementsMultipleManager.dash_guy);
				if (mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.dash_guy && mdata.data == "guy")
					return;
				if (mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.dash_nisman && mdata.data == "nisman")
					return;
			} else if (mdata.type == types.DEAD) {
				if (mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.dead_by_escudo && mdata.data == "escudo")
					return;
				if (mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.dead_by_rati_jump && mdata.data == "jump")
					return;
				if (mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.dead_by_guy && mdata.data == "guy")
					return;
			} else if (mdata.type == types.TIMEKILLED) {
				if (mdata.pointsToBeReady > time_killed)
					return;
			}
		}
		// si no hizo return:
		Ready();
		SaveMultipleData ();
		Reset();
	}
	void OnNewDeadComputed(AchievementsMultipleManager.deadTypes type)
    {
		OnCheckIfMultipleAchievementsAreDone ();
    }
	void OnNewDashComputed(AchievementsMultipleManager.dashTypes type)
	{
		OnCheckIfMultipleAchievementsAreDone ();
	}
}