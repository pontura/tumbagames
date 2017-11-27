using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

	public Enemy enemy_to_instantiate;
	public Transform container;

	void Start () {
		Enemy enemy = Instantiate (enemy_to_instantiate);
		enemy.transform.SetParent (container);
		enemy.Init ();
	}
}
