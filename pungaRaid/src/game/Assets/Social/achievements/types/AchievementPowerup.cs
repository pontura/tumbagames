using UnityEngine;
using System.Collections;

public class AchievementPowerup : Achievement
{
    public string variable;

	public void Init () {
        variable = data + pointsToBeReady.ToString();
        this.type = types.POWERUP;
        
        if (PlayerPrefs.GetInt(variable) == 1)
            Ready();
        else
            AchievementsEvents.OnPowerUp += OnPowerUp;
	}
    void OnDestroy()
    {
        AchievementsEvents.OnPowerUp -= OnPowerUp;
    }
    void OnPowerUp(string type)
    {
        Debug.Log("AchievementPowerup OnPowerUp: " + type.ToString() + " data: " + data);
        if (type.ToString() == data)
        {
            Debug.Log("Achievement READY " + variable);
            PlayerPrefs.SetInt(variable, 1);
            Ready();
            OnDestroy();
        }
    }
}
