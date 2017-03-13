using System.Collections;
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
		belgrano.Init (this,AchievementsManager.Instance.achievements[0], true);

		foreach (Achievement ach in AchievementsManager.Instance.achievements) {
			switch(ach.type)
			{
			case Achievement.types.AREA:
				
				if (ach.seccionalID == 2 && ach.moodID == 1) norco.Init (this, ach, ach.ready);
				if (ach.seccionalID == 3 && ach.moodID == 1) zabeca.Init (this, ach, ach.ready);
				if (ach.seccionalID == 1 && ach.moodID == 2) mamerto.Init (this, ach, ach.ready);
				if (ach.seccionalID == 2 && ach.moodID == 2) centro.Init (this, ach, ach.ready);
				if (ach.seccionalID == 3 && ach.moodID == 2) puerto.Init (this, ach, ach.ready);
					break;

			case Achievement.types.POWERUP:
				if (ach.data == "CHUMBO")
					megachumbo.Init (this, ach, ach.ready);
				if (ach.data == "SKATE")
					skate.Init (this, ach, ach.ready);
				if (ach.data == "MOTO")
					raticiclo.Init (this, ach, ach.ready);
				if (ach.data == "GAUCHITO")
					gauchito.Init (this, ach, ach.ready);
				if (ach.data == "CACA")
					caca.Init (this, ach, ach.ready);
				if (ach.data == "MIAMI")
					fortuna.Init (this, ach, ach.ready);
				break;
			}
		}
	}
}
