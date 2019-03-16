using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttackStyle {

	public CharacterHitsManager.types type;
	public AnimationClip animClip;
	public int force;
	public float timeToReset = 0.2f;

}
