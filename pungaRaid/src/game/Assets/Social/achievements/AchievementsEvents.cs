using UnityEngine;
using System.Collections;

public static class AchievementsEvents
{
    public static System.Action<int> OnReady = delegate { };
    public static System.Action<int> OnMissionComplete = delegate { };
	public static System.Action<int, int, float> OnNewDistance = delegate { };
	public static System.Action<int, int, float> OnCheckScore = delegate { };
    public static System.Action<string> OnPowerUp = delegate { };
}