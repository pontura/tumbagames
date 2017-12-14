using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

	public GameObject warnesMan;
	public GameObject ceoMan;
	public GameObject opusMan;

	public Enemy enemy_to_instantiate;
	public Transform container;

	void Start () {
		Enemy enemy = Instantiate (enemy_to_instantiate);
		enemy.transform.SetParent (container);
		enemy.Init (warnesMan);
		enemy.transform.localPosition = Vector3.zero;

		enemy = Instantiate (enemy_to_instantiate);
		enemy.transform.SetParent (container);
		enemy.Init (ceoMan);
		enemy.transform.localPosition = new Vector3 (4, 0, 0);

		enemy = Instantiate (enemy_to_instantiate);
		enemy.transform.SetParent (container);
		enemy.Init (opusMan);
		enemy.transform.localPosition = new Vector3 (8, 0, 0);

	}
}
