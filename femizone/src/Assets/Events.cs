using UnityEngine;
using System.Collections;

public static class Events
{

    public static System.Action RefreshHiscores = delegate { };

    public static System.Action StartGame = delegate { };
    public static System.Action Restart = delegate { };

    public static System.Action PixelSizeChange = delegate { };

    public static System.Action OnCarCrashEnemy = delegate { };
    public static System.Action<int, int> OnAddScore = delegate { };
    public static System.Action<int> OnKeyPress = delegate { };
    public static System.Action<int, int> OnAxisChange = delegate { };

    public static System.Action<float, float> OnSaveVolumes = delegate { };
    public static System.Action<float> OnMusicVolumeChanged = delegate { };
    public static System.Action<bool> OnMusicOff = delegate { };
    public static System.Action<float> OnSoundsVolumeChanged = delegate { };
    public static System.Action<string> OnSoundFX = delegate { };
    public static System.Action<string> OnMusicChange = delegate { };

    public static System.Action<int, float> OnHeroHitted = delegate { };

    public static System.Action<string> OnChangeScene = delegate { };
    public static System.Action<int> AddHero = delegate { };

    public static System.Action<int> OnHeroDie = delegate { };
    public static System.Action OnHeroComido = delegate { };
    public static System.Action OnHeroCelebrate = delegate { };

    public static System.Action<bool> OnGamePaused = delegate { };
    public static System.Action GameOver = delegate { };
    public static System.Action<CutsceneInGame.types> OnCutscene = delegate { };

    public static System.Action<Hero, Powerup> GrabPowerUp = delegate { };

    public static System.Action<Character> OnCharacterDie = delegate { };

    public static System.Action<Character> OnJump = delegate { };
    public static System.Action<CharacterHitsManager.types, Character> OnAttack = delegate { };
    public static System.Action<CharacterHitsManager.types, Character> OnReceiveit = delegate { };
    public static System.Action<Character, bool> OnMansPlaining = delegate { };
    public static System.Action OnStageClear = delegate { };
    public static System.Action OnCutsceneDone = delegate { };
    public static System.Action OnInitFight = delegate { };
}
