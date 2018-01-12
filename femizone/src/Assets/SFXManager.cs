﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SFXManager : MonoBehaviour {

	public AudioClips punch;
	public AudioClips kick;
	public AudioClips balls_hit;
	public AudioClips punches_hit;
	public AudioClips hits;
	public AudioClips mansplaining;

	[Serializable]
	public class AudioClips
	{
		public AudioClip[] hero;
		public AudioClip[] macho;
		public AudioClip[] poli;
		public AudioClip[] pussy;
	}

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


	void Start () {
		Events.OnAttack += OnAttack;
		Events.OnReceiveit += OnReceiveit;
		Events.OnMansPlaining += OnMansPlaining;
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
			clipByCharacter.audioSource = gameObject.AddComponent<AudioSource> ();
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

		AudioClip[] arr;
		AudioClip audioClip;
		switch (type) {

		case CharacterHitsManager.types.HIT_BACK:
		case CharacterHitsManager.types.HIT_FORWARD:
		case CharacterHitsManager.types.HIT_UPPER:
			arr = GetArrClip (punches_hit, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_hit_punches.clip = audioClip;
			AudioS_hit_punches.Play ();
			break;

		case CharacterHitsManager.types.KICK_BACK:
		case CharacterHitsManager.types.KICK_DOWN:
		case CharacterHitsManager.types.KICK_FOWARD:
			arr = GetArrClip (balls_hit, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_hit_balls.clip = audioClip;
			AudioS_hit_balls.Play ();
			break;

		}

	}
	void OnAttack(CharacterHitsManager.types type, Character character)
	{

		AudioClip[] arr;
		AudioClip audioClip;
		switch (type) {

		case CharacterHitsManager.types.HIT_BACK:
		case CharacterHitsManager.types.HIT_FORWARD:
		case CharacterHitsManager.types.HIT_UPPER:
			arr = GetArrClip (punch, character.stats.type);
			audioClip = GetRandom (arr);
			AudioS_punches.clip = audioClip;
			AudioS_punches.Play ();
			break;

		case CharacterHitsManager.types.KICK_BACK:
		case CharacterHitsManager.types.KICK_DOWN:
		case CharacterHitsManager.types.KICK_FOWARD:
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