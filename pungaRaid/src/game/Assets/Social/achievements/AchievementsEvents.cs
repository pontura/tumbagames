using UnityEngine;
using System.Collections;

public static class AchievementsEvents
{
	public static System.Action<int, int> OnAreaPlayed = delegate { };
	public static System.Action<int, int, float> OnNewDistance = delegate { };
	public static System.Action<int, int, float> OnCheckMoney = delegate { };
    public static System.Action<string> OnPowerUp = delegate { };
	public static System.Action<Achievement> OnAchievementReady = delegate { };
}