using UnityEngine;
using System.Collections;

public static class AchievementsEvents
{
    public static System.Action<int, int> OnAreaPlayed = delegate { };
    public static System.Action<int, int, float> OnNewDistance = delegate { };
    public static System.Action<int, int, float> OnCheckMoney = delegate { };
    public static System.Action<int, int> OnUnlock = delegate { };
    public static System.Action<string> OnPowerUp = delegate { };
    public static System.Action<Enemy> OnDie = delegate { };
	public static System.Action<Enemy> OnSteal = delegate { };
	public static System.Action<Enemy> OnDash = delegate { };
	public static System.Action OnShoot = delegate { };
	public static System.Action OnPung = delegate { };
	public static System.Action OnPung_While_Gil = delegate { };
	public static System.Action OnDash_While_Gil = delegate { };
	public static System.Action OnCrash_While_Moto = delegate { };
	public static System.Action OnZero_Shoot_While_Chumbo = delegate { };

	public static System.Action<string> OnSpecialEnemyPung = delegate { };
    public static System.Action<Achievement> OnAchievementReady = delegate { };

	public static System.Action<PowerupManager.types> OnPowerupOff = delegate { };
	public static System.Action<int> OnPungComputed = delegate { };
	public static System.Action<AchievementActionWhilePowerup.actions, int> OnActionWhilePowerUPComputed = delegate { };
	public static System.Action<AchievementsMultipleManager.dashTypes> OnNewDashComputed = delegate { };
	public static System.Action<AchievementsMultipleManager.deadTypes> OnNewDeadComputed = delegate { };
	public static System.Action OnNewMultiplePowerUpComputed = delegate { };
	public static System.Action NewSecondKilled = delegate { };

}