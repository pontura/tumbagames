using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

	Enemy enemy;
	void Start()
	{
		enemy = GetComponent<Enemy> ();
	}
	public void Attack()
	{
		if (Random.Range (0, 10) < 5) {
			enemy.hitArea.type = HitArea.types.HIT_UPPER;
			enemy.anim.Play ("upper");
		} else {
			enemy.hitArea.type = HitArea.types.HIT_DOWN;
			enemy.anim.Play ("punch");
		}
	}
}
