using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAAlertDistance : MonoBehaviour {
	
	public int distance_to_active;
	public states state;
	public enum states
	{
		ON,
		OFF
	}
	IA ia;
	void Start()
	{
		ia = GetComponent<IA> ();
	}
	void Update () {
		if (state == states.OFF)
			return;
		Vector3 dist = World.Instance.heroesManager.CheckIfHeroIsClose (ia.enemy);
		if (transform.position.x - dist.x <= distance_to_active) {
			SendMessage ("IAAlertDistance");
			state = states.OFF;
		}
	}
}
