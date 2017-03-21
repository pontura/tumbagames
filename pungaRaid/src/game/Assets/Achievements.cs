﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour {

	public AchievementButton megachumbo ;
	public AchievementButton norco ;
	public AchievementButton fortuna ;
	public AchievementButton centro ;
	public AchievementButton puerto ;
	public AchievementButton belgrano ;
	public AchievementButton gauchito ;
	public AchievementButton mamerto ;
	public AchievementButton raticiclo ;
	public AchievementButton caca ;
	public AchievementButton skate ;
	public AchievementButton zabeca ;
    public AchievementButton ratis;
    public AchievementButton nisman;

    void Start()
	{
		SetAchievements ();
	}
	public void Close()
	{
		Data.Instance.LoadLevel ("02_Main");
	}
	public void Selected(Achievement achievement)
	{
		List<Achievement> achm = AchievementsManager.Instance.GetAchievementsByListID (achievement.listID);
		Events.OnShowAchievementList (achm);
	}
	void SetAchievements()
	{
		foreach (Achievement ach in AchievementsManager.Instance.achievements) {
			switch(ach.type)
			{
			case Achievement.types.UNLOCK:
			case Achievement.types.DISTANCE:
			case Achievement.types.MONEY:
				if (ach.seccionalID == 1 && ach.moodID == 1) belgrano.Init (this, ach, ach.ready);
				if (ach.seccionalID == 2 && ach.moodID == 1) norco.Init (this, ach, ach.ready);
				if (ach.seccionalID == 3 && ach.moodID == 1) zabeca.Init (this, ach, ach.ready);
				if (ach.seccionalID == 1 && ach.moodID == 2) mamerto.Init (this, ach, ach.ready);
				if (ach.seccionalID == 2 && ach.moodID == 2) centro.Init (this, ach, ach.ready);
				if (ach.seccionalID == 3 && ach.moodID == 2) puerto.Init (this, ach, ach.ready);
					break;

            case Achievement.types.DEAD:
                if (ach.data == "DEAD_ESCUDO")
                    ratis.Init(this, ach, ach.ready);
                break;
            case Achievement.types.NISMAN:
                if (ach.data == "NISMAN")
                    nisman.Init(this, ach, ach.ready);
                break;

            case Achievement.types.POWERUP:
                if (ach.data == "CHUMBO")
			      megachumbo.Init (this, ach, ach.ready);
				if (ach.data == "SKATE")
					skate.Init (this, ach, ach.ready);
				if (ach.data == "MOTO")
					raticiclo.Init (this, ach, ach.ready);
				if (ach.data == "GIL")
					gauchito.Init (this, ach, ach.ready);
				if (ach.data == "SORETE")
					caca.Init (this, ach, ach.ready);
				if (ach.data == "RICKYFORT")
					fortuna.Init (this, ach, ach.ready);
				break;
			}
		}
	}
}
