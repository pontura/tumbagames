using UnityEngine;
using System.Collections;

public static class Events {

    //The game:
    public static System.Action StartGame = delegate { };
    public static System.Action Restart = delegate { };
    public static System.Action OnLevelComplete = delegate { };    

    public static System.Action<float, float> OnSaveVolumes = delegate { };
    public static System.Action<float> OnMusicVolumeChanged = delegate { };
    public static System.Action<bool> OnMusicOff = delegate { };
    public static System.Action<float> OnSoundsVolumeChanged = delegate { };
    public static System.Action<string> OnSoundFX = delegate { };
    public static System.Action<string> OnMusicChange = delegate { };

	public static System.Action<int, int> OnHeroHitted = delegate { };
    public static System.Action OnHeroDie = delegate { };
    public static System.Action OnHeroComido = delegate { };
    public static System.Action OnHeroCelebrate = delegate { };

    public static System.Action<bool> OnGamePaused = delegate { };
    public static System.Action GameOver = delegate { };

	public static System.Action<Hero, Powerup> GrabPowerUp = delegate { };

	public static System.Action<Character> OnCharacterDie = delegate { };

}
