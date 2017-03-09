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
		Events.OnShowAchievementSignal (achievement.image, achievement.title);
	}
	void SetAchievements()
	{
		belgrano.Init (this,AchievementsManager.Instance.achievements[0], true);

		foreach (Achievement ach in AchievementsManager.Instance.achievements) {
			print("____________   " + ach.ready);
			switch(ach.type)
			{
			case Achievement.types.AREA:
				
				if (ach.id == 1) norco.Init (this, ach, ach.ready);
				if (ach.id == 2) zabeca.Init (this, ach, ach.ready);
				if (ach.id == 3) mamerto.Init (this, ach, ach.ready);
				if (ach.id == 4) centro.Init (this, ach, ach.ready);
				if (ach.id == 5) puerto.Init (this, ach, ach.ready);
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
