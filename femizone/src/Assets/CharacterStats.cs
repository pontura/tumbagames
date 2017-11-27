using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public int life;
	public int force;
	public int speed;
	public int defense;

	public void ReceiveHit(int force)
	{
		life -= force;
	}
}
