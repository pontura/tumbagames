﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public Vector3 offset;
	public int life;
	public int force;
	public int speed;
	public int defense;
	public Vector2 time_to_GoTo_Target = new Vector2 (0.5f, 3);
	public Vector2 time_to_Punch = new Vector2 (0.2f, 2);
	public float hittedPower; // 5;

	public void ReceiveHit(int force)
	{
		life -= force;
	}

	public List<AttackStyle> attacks;

	public List<AttackStyle> receivedAttacks;

	public AttackStyle GetAttackByType(CharacterHitsManager.types type)
	{
		foreach (AttackStyle attack in attacks) {
			if (attack.type == type)
				return attack;
		}
		return null;
	}
}
