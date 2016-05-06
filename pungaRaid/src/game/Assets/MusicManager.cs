using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioSource audioSource;
    public float volume;
    public bool disabled;
       
	public void Init () {

        volume = PlayerPrefs.GetFloat("MusicVol", 1);
        if (volume == 0) disabled = true;
        audioSource.loop = true;
        OnMusicVolumeChanged(volume);

        Events.OnGamePaused += OnGamePaused;
        Events.OnMusicVolumeChanged += OnMusicVolumeChanged;
        Events.OnMusicChange += OnMusicChange;
        Events.OnMusicOff += OnMusicOff;
	}
    void OnDestroy()
    {
        Events.OnGamePaused -= OnGamePaused;
        Events.OnMusicVolumeChanged -= OnMusicVolumeChanged;
        Events.OnMusicChange -= OnMusicChange;
        Events.OnMusicOff -= OnMusicOff;
    }
    public void OnMusicOff(bool off)
    {
        disabled = off;
        if (off) OnMusicVolumeChanged(0);
        else OnMusicVolumeChanged(0.2f);
    }
    void OnMusicChange(string soundName)
    {
        if (soundName == "") audioSource.Stop();
        if (audioSource.clip && audioSource.clip.name == soundName) return;
        audioSource.clip = Resources.Load("music/" + soundName) as AudioClip;
        audioSource.Play();

        if (soundName == "victoryMusic" || soundName == "gameOverTemp")
            audioSource.loop = false;
        else
            audioSource.loop = true;
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
        if (disabled) value = 0;

        audioSource.volume = value;
        volume = value;

        if (value == 0 || value == 1)
            PlayerPrefs.SetFloat("MusicVol", value);
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



