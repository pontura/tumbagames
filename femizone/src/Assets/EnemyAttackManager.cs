using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour {

	Enemy enemy;
    public AttackStyle attackStyle;

    void Start()
	{
		enemy = GetComponent<Enemy> ();
	}
	public void Attack()
	{
		int attacksQty = enemy.stats.attacks.Count;

        attackStyle = enemy.stats.attacks [Random.Range (0, attacksQty)];
		enemy.hitArea.SetType(attackStyle.type, attackStyle.force);
		enemy.anim.Play (attackStyle.animClip.name);


		Events.OnAttack (attackStyle.type, enemy);
	}
}
