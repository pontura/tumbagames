using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioSource loopAudioSource;
    public float volume;

    public void Init()
    {
        volume = PlayerPrefs.GetFloat("SFXVol", 1);

        OnSoundsVolumeChanged(volume);

        Events.OnSoundFX += OnSoundFX;
        Events.OnSoundsVolumeChanged += OnSoundsVolumeChanged;        
        Events.OnHeroDie += OnHeroDie;
    }
    void OnHeroDie()
    {
    }
    void OnDestroy()
    {
        Events.OnSoundFX -= OnSoundFX;
        Events.OnSoundsVolumeChanged -= OnSoundsVolumeChanged;
        Events.OnHeroDie -= OnHeroDie;
        if (loopAudioSource)
        {
            loopAudioSource = null;
            loopAudioSource.Stop();
        }
    }
    void OnSoundsVolumeChanged(float value)
    {
        audioSource.volume = value;
        volume = value;

        if (value == 0 || value == 1)
            PlayerPrefs.SetFloat("SFXVol", value);
    }
    void OnSoundFX(string soundName)
    {
        if (soundName == "")
        {
            audioSource.Stop();
            return;
        }
        if (volume == 0) return;
        audioSource.PlayOneShot(Resources.Load("sound/" + soundName) as AudioClip);

    }
}
