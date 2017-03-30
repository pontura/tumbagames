using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {

	public Image bar;

	public Text field1;
	public Text field2;

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
    public AchievementButton futbol;

    void Start()
	{
		SetAchievements ();
		int totalRangos = AchievementsManager.Instance.rangos.Count;

		float myAchDone = AchievementsManager.Instance.GetTotalReady();
		float all = AchievementsManager.Instance.achievements.Count;

		float percent = (myAchDone / all) * totalRangos;
		float fillAmount = (myAchDone / all);
		bar.fillAmount = fillAmount;

		int id = 0;
		string rangoReal = "";
		foreach (string rango in AchievementsManager.Instance.rangos) {
			if (id <= percent)
				rangoReal = rango;
			id++;
		}

		field1.text = "Rango: " + rangoReal;
		field2.text = "Delitos: " + myAchDone + "/" + all;
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

			case Achievement.types.MULTIPLE:
				if (ach.listID == "RATIS")
                    ratis.Init(this, ach, ach.ready);
				else if (ach.listID == "FUTBOL")
					futbol.Init(this, ach, ach.ready);
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
