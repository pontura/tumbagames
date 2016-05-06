using UnityEngine;
using System.Collections;

public static class Events {

    public static System.Action OnSettings = delegate { };
    public static System.Action OnLoginAdvisor = delegate { };

    public static System.Action<int, float> OnNewHiscore = delegate { };
    public static System.Action<GameObject> OnUIClicked = delegate { };

    public static System.Action OnRanking = delegate { };  
    public static System.Action OnChallenges = delegate { }; 

    public static System.Action<string> OnPreloadScene = delegate { };
    public static System.Action<string> OnSceneLoad = delegate { };
    public static System.Action OnLoadSceneReady = delegate { };
    public static System.Action OnSceneReset = delegate { };
    public static System.Action OnTransition = delegate { };
    public static System.Action OnTransitionReady = delegate { };
    public static System.Action OnPoolAllItemsInScene = delegate { };
    public static System.Action<PowerupManager.types> OnPowerUp = delegate { };
    public static System.Action<PowerdownManager.types> OnPowerDown = delegate { };
    public static System.Action<PowerupManager.types> OnPowerUpShoot = delegate { };
    public static System.Action OnHeroPowerUpOff = delegate { };
    public static System.Action OnSpecialItemOff = delegate { };
    public static System.Action<bool> OnVulnerability = delegate { };
    public static System.Action<bool> OnOooops = delegate { };
    

    //The game:
    public static System.Action StartGame = delegate { };
    public static System.Action OnGameOver = delegate { };
    public static System.Action OnLevelComplete = delegate { };
    public static System.Action<SwipeDetector.directions> OnSwipe = delegate { };

    //laneID, distance, mnultiplayerStolen
    public static System.Action<int, float, int> OnAddCoins = delegate { };
    public static System.Action OnEndingShot = delegate { };
    

    public static System.Action<float, float> OnSaveVolumes = delegate { };
    public static System.Action<float> OnMusicVolumeChanged = delegate { };
    public static System.Action<bool> OnMusicOff = delegate { };
    public static System.Action<float> OnSoundsVolumeChanged = delegate { };
    public static System.Action<bool> OnCapsChanged = delegate { };
    public static System.Action<string> OnVoice = delegate { };
    public static System.Action<string> OnSoundFX = delegate { };
    public static System.Action<string> OnSoundFXLoop = delegate { };
    
    public static System.Action<string> OnMusicChange = delegate { };

    public static System.Action<bool> OnGamePaused = delegate { };
    public static System.Action OnGameRestart = delegate { };
    public static System.Action<float, bool> OnChangeSpeed = delegate { };
    public static System.Action OnResetSpeed = delegate { };

    public static System.Action OnChangeLane = delegate { };
    public static System.Action OnChangeLaneComplete = delegate { };

    public static System.Action OnHeroDie = delegate { };
    public static System.Action OnHeroCrash = delegate { };
    public static System.Action OnHeroCelebrate = delegate { };
    
    public static System.Action OnExplotion = delegate { };
    //laneID, distance
    public static System.Action<int, int> OnAddExplotion = delegate { };

    public static System.Action<int> OnScoreAdd = delegate { };
    public static System.Action<int> OnRefreshScore = delegate { };
    public static System.Action OnStartCountDown = delegate { };
    public static System.Action<PowerupManager.types> OnBarInit = delegate { };
    public static System.Action OnBarReady = delegate { };

    public static System.Action<float> OnCombo = delegate { };


    //zones:
    public static System.Action<int> OnUnlockZone = delegate { };
    public static System.Action OnLoadingPanel = delegate { };

    public static System.Action<int, bool> OnSetSpecialItem = delegate { };
    
    
    

}
