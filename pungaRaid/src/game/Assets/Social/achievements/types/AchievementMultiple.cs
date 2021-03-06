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
		AchievementsEvents.NewSecondKilled += NewSecondKilled;
		AchievementsEvents.OnNewMultiplePowerUpComputed += OnCheckIfMultipleAchievementsAreDone;

		Events.OnGameStart += OnGameStart;
    }
	void Reset()
	{
		AchievementsEvents.OnNewDeadComputed -= OnNewDeadComputed;
		AchievementsEvents.NewSecondKilled -= NewSecondKilled;
		AchievementsEvents.OnNewMultiplePowerUpComputed -= OnCheckIfMultipleAchievementsAreDone;
		Events.OnGameStart -= OnGameStart;
	}

	void OnGameStart()
	{
		time_killed = 0;
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
		if (moodID != 0) {
			MoodsManager mm = Data.Instance.moodsManager;
			int _moodID = mm.GetCurrentMoodID();
			int _seccionalID = mm.GetCurrentSeccional().id;
			if (_moodID != moodID || _seccionalID != seccionalID)
				return;
		}
		foreach (MultipleData mdata in multipleData) {			
			if (mdata.type == types.FUTBOL) {
				string specialCloth = Data.Instance.clothItemsManager.ClothesWearing;
				//Debug.Log (mdata.type + " pointsToBeReady:" + specialCloth + " mdata.futbol_ " + mdata.pointsToBeReady);

				if (mdata.pointsToBeReady == 1 && specialCloth !="Boca") return;
				else if (mdata.pointsToBeReady == 2 && specialCloth !="River") return;
				else if (mdata.pointsToBeReady == 3 && specialCloth !="Arsenal") return;
				else if (mdata.pointsToBeReady == 4 && specialCloth !="Argentinos") return;				 

			} else if (mdata.type == types.DASH) {
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
			} else if (mdata.type == types.POWERUP) {
				if (mdata.data == "sorete" && mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.soreteQty)
					return;
			} else if (mdata.type == types.SHOOT) {
				if (mdata.data == "megachumbo" && mdata.pointsToBeReady > AchievementsManager.Instance.achievementsMultipleManager.shootsQty)
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