using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioSource audioSource;

	public AudioClip musicHard;
	public AudioClip musicCalm;
    public AudioClip musicDeath;

    public float volume;
       
	void Start()
	{
        audioSource = gameObject.AddComponent<AudioSource> ();
        audioSource.loop = true;

        OnStageClear ();
        OnChangeScene("Intro");
    }
	void OnMansPlaining(Character c, bool isOn)
	{
		if (!isOn && audioSource.clip != musicHard) {
			audioSource.clip = musicHard;		
			audioSource.Play ();
		}
	}
	void OnStageClear()
	{
        if (audioSource.clip != musicCalm)
        {
            audioSource.clip = musicCalm;
            audioSource.Play();
        }
	}
	public void Init () {

        OnMusicVolumeChanged(volume);

        Events.OnMansPlaining += OnMansPlaining;
        Events.OnStageClear += OnStageClear;
        Events.OnGamePaused += OnGamePaused;
        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnMusicOff += OnMusicOff;
        Events.GameOver += GameOver;
        Events.OnChangeScene += OnChangeScene;
        Events.AddHero += AddHero;
    }
    void OnDestroy()
    {
		Events.OnMansPlaining -= OnMansPlaining;
		Events.OnStageClear -= OnStageClear;
        Events.OnGamePaused -= OnGamePaused;
        Events.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        Events.OnMusicOff -= OnMusicOff;
        Events.GameOver -= GameOver;
        Events.OnChangeScene -= OnChangeScene;
        Events.AddHero -= AddHero;
    }
    void AddHero(int id)
    {
        if (audioSource.pitch == 1)
            return;

        audioSource.pitch = 1;
        audioSource.Play();
    }
    public void GameOver()
    {
        audioSource.clip = musicDeath;
        audioSource.Play();
       // audioSource.pitch = 1.5f;
    }
    void OnChangeScene(string sceneName)
    {
        switch(sceneName)
        {
            case "Intro":
            case "IntroCutscenes":
                audioSource.clip = musicCalm;
                audioSource.pitch = 0.2f;
                audioSource.Play();
                OnMusicVolumeChanged(1);
                break;
        }
        
    }
    public void OnMusicOff(bool off)
    {
		audioSource.Stop ();
        audioSource.pitch = 1;
    }
    void OnSoundsFadeTo(float to)
    {
        if (to > 0) to = volume;
       // TweenVolume tv = TweenVolume.Begin(gameObject, 1, to);
        //tv.PlayForward();
        //tv.onFinished.Clear();
    }
    void OnMusicVolumeChanged(float value)
    {
        audioSource.volume = value;
        volume = value;
    }
    void OnGamePaused(bool paused)
    {
        if(paused)
            audioSource.Stop();
        else
            audioSource.Play();
    }
    void stopAllSounds()
    {
        audioSource.Stop();
    }
}



