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
		int attacksQty = enemy.stats.attacks.Count;
		AttackStyle attack = enemy.stats.attacks [Random.Range (0, attacksQty)];
		enemy.hitArea.type = attack.type;
		enemy.anim.Play (attack.animName);
	}
}
