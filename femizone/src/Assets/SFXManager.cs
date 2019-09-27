using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFXManager : MonoBehaviour {

    public bool isOff;
    public AudioClip gunShot;
    public AudioClip jump;
    public AudioClip whip;
    public AudioClip carCrash;

    public GameObject sfxContainer;
    public AudioClips punch;
	public AudioClips kick;
	public AudioClips balls_hit;
	public AudioClips punches_hit;
	public AudioClips hits;
	public AudioClips mansplaining;
    public AudioclipsUI audioclipsUI;

    [Serializable]
    public class AudioclipsUI
    {
        public AudioClip insertCoin;
        public AudioClip gameOver;
    }

        [Serializable]
	public class AudioClips
	{
		public AudioClip[] hero;
		public AudioClip[] macho;
		public AudioClip[] poli;
		public AudioClip[] pussy;
	}

    public AudioSource uiAudioSource;
    public AudioSource AudioS_punches;
	public AudioSource AudioS_kicks;
	public AudioSource AudioS_hit_balls;
	public AudioSource AudioS_hit_punches;
    public GameObject mansplaining_container;

	public List<ClipByCharacter> clipsByCharacter;
	[Serializable]
	public class ClipByCharacter
	{
		public AudioSource audioSource;
		public Character character;
	}
	int distanceToManPlanning = 26;

	void Start () {
        Events.OnJump += OnJump;
        Events.OnAttack += OnAttack;
		Events.OnReceiveit += OnReceiveit;
		Events.OnMansPlaining += OnMansPlaining;
        Events.GameOver += GameOver;
        Events.AddHero += AddHero;
        Events.OnCarCrashEnemy += OnCarCrashEnemy;
        Events.Restart += Restart;
        Loop();

    }
	void OnDestroy()
	{
        Events.OnJump -= OnJump;
        Events.OnAttack -= OnAttack;
		Events.OnReceiveit -= OnReceiveit;
		Events.OnMansPlaining -= OnMansPlaining;
        Events.GameOver -= GameOver;
        Events.AddHero -= AddHero;        
        Events.OnCarCrashEnemy -= OnCarCrashEnemy;
        Events.Restart -= Restart;
    }
    void Restart()
    {
        isOff = false;
    }
    void OnCarCrashEnemy()
    {
        uiAudioSource.clip = carCrash;
        uiAudioSource.Play();
    }
    void AddHero(int id)
    {
        uiAudioSource.clip = audioclipsUI.insertCoin;
        uiAudioSource.Play();
    }
    void Loop()
    {
        Invoke("Loop", 1);
        AdjustVolume();
    }
    void GameOver()
    {
        isOff = true;
        foreach (AudioSource aus in sfxContainer.GetComponents<AudioSource>())
        {
            aus.Stop();
        }
        foreach (ClipByCharacter c in clipsByCharacter)
        {
            c.audioSource.Stop();
            Destroy(c.audioSource);
        }
        clipsByCharacter.Clear();

        uiAudioSource.clip = audioclipsUI.gameOver;
        uiAudioSource.Play();
    }

    void AdjustVolume()
	{
		if (clipsByCharacter.Count == 0)
			return;
		float dist = 1;
		float pos_cam_x = World.Instance.worldCamera.transform.localPosition.x;
		if (clipsByCharacter [0].character == null) {
			clipsByCharacter.RemoveAt (0);
			return;
		}
		float pos_char = clipsByCharacter [0].character.transform.position.x;
		dist = distanceToManPlanning - (pos_char - pos_cam_x-5);
		dist /= distanceToManPlanning*1.5f;

		if (dist < 0)
			dist = 0;
		else if (dist > 1)
			dist = 1;

		foreach (ClipByCharacter clip in clipsByCharacter) {
			clip.audioSource.volume = dist;
		}
	}
	int rand = 0;
	void OnMansPlaining(Character character, bool isOn)
	{
		if (isOn) {
			ClipByCharacter clipByCharacter = new ClipByCharacter ();
			AudioClip[] arr = GetArrClip (mansplaining, character.stats.type);
			rand++;
			if (rand > arr.Length - 1)
				rand = 0;
			AudioClip audioClip = arr [rand];
			clipByCharacter.audioSource = sfxContainer.AddComponent<AudioSource> ();
			clipByCharacter.audioSource.clip = audioClip;
			clipByCharacter.audioSource.loop = true;
			clipByCharacter.audioSource.Play ();
			clipByCharacter.character = character;
			clipsByCharacter.Add (clipByCharacter);
		} else {
			ClipByCharacter cToRemove = null;
			foreach (ClipByCharacter c in clipsByCharacter) {
				if (c.character.stats.type == character.stats.type)
					cToRemove = c;
			}
			if (cToRemove != null) {				
				cToRemove.audioSource.Stop ();
				clipsByCharacter.Remove (cToRemove);
				Destroy (cToRemove.audioSource);
			}
		}
	}
	void OnReceiveit(CharacterHitsManager.types type, Character character)
	{
        if (isOff)
            return;
        AudioClip[] arr;
		AudioClip audioClip;
		switch (type) {
        case CharacterHitsManager.types.MELEE:
        case CharacterHitsManager.types.GUN_FIRE:
        case CharacterHitsManager.types.CINTURONGA:

        case CharacterHitsManager.types.HIT_BACK:
		case CharacterHitsManager.types.HIT_FORWARD:
		case CharacterHitsManager.types.HIT_UPPER:
        case CharacterHitsManager.types.HIT_JUMP:
            arr = GetArrClip (punches_hit, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_hit_punches.clip = audioClip;
			AudioS_hit_punches.Play ();
			break;

		case CharacterHitsManager.types.KICK_BACK:
		case CharacterHitsManager.types.KICK_DOWN:
		case CharacterHitsManager.types.KICK_FOWARD:
        case CharacterHitsManager.types.KICK_JUMP:
            arr = GetArrClip (balls_hit, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_hit_balls.clip = audioClip;
			AudioS_hit_balls.Play ();
			break;

		}

	}
    void OnJump(Character character)
    {
        AudioS_punches.clip = jump;
        AudioS_punches.Play();
    }
    void OnAttack(CharacterHitsManager.types type, Character character)
	{
        if (isOff)
            return;
		AudioClip[] arr;
		AudioClip audioClip;
		switch (type) {
            case CharacterHitsManager.types.CINTURONGA:
            case CharacterHitsManager.types.MELEE:
                AudioS_punches.clip = whip;
                AudioS_punches.Play();
                break;
        case CharacterHitsManager.types.HIT_BACK:
		case CharacterHitsManager.types.HIT_FORWARD:
		case CharacterHitsManager.types.HIT_UPPER:
        case CharacterHitsManager.types.HIT_JUMP:
            arr = GetArrClip (punch, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_punches.clip = audioClip;
			AudioS_punches.Play ();
			break;
        case CharacterHitsManager.types.GUN_FIRE:
            audioClip = gunShot;
            AudioS_punches.clip = audioClip;
            AudioS_punches.Play();
            break;
        case CharacterHitsManager.types.KICK_BACK:
		case CharacterHitsManager.types.KICK_DOWN:
		case CharacterHitsManager.types.KICK_FOWARD:
        case CharacterHitsManager.types.KICK_JUMP:
                arr = GetArrClip (kick, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_kicks.clip = audioClip;
			AudioS_kicks.Play ();
			break;

		}

	}
	AudioClip[] GetArrClip(AudioClips arr, CharacterStats.types type)
	{
		switch (type) {
		case CharacterStats.types.HERO:
			return arr.hero;
			break;
		case CharacterStats.types.MACHO:
			return arr.macho;
			break;
		case CharacterStats.types.POLI:
			return arr.poli;
			break;
			default:
			return arr.pussy;
			break;
		}
	}
	AudioClip GetRandom(AudioClip[] all)
	{
		return all[UnityEngine.Random.Range(0, all.Length-1)  ];
	}

}
