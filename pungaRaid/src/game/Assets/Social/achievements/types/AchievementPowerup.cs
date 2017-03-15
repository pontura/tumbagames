using UnityEngine;
using System.Collections;

public class AchievementPowerup : Achievement
{

	public override void OnInit () {
		this.type = types.POWERUP;
		AchievementsEvents.OnPowerUp += OnPowerUp;
	}
	void OnPowerUp(string type)
	{
		Debug.Log ("___________________" + type);

		if (type.ToString () == data) {
			qty++;
			SaveInt (qty);
		}
		if( qty >= pointsToBeReady)
		{
			Ready();
			AchievementsEvents.OnPowerUp -= OnPowerUp;
		}
	}
}
