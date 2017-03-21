using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDead : Achievement
{

	public override void OnInit()
    {
        this.type = types.DEAD;
        AchievementsEvents.OnNewDeadComputed += OnNewDeadComputed;
    }
	void OnNewDeadComputed(AchievementsMultipleManager.deadTypes type)
    {
        Debug.Log("AchievementDead : ___________________" + type);

        if (type.ToString() == data)
        {
            qty++;
            SaveInt(qty);
        }
        if (qty >= pointsToBeReady)
        {
            Ready();
            AchievementsEvents.OnNewDeadComputed -= OnNewDeadComputed;
        }
    }
}