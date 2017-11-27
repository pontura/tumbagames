using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {

	Character character;
	public states state;

	public enum states
	{
		SLEEP,
		LOOKING_FOR_TARGET,
		READY_FOR_FIGHT
	}

	void Start () {
		character = GetComponent<Character> ();
	}

	void Update() {
		
		if (character.state == Character.states.SLEEP || character.state == Character.states.DEAD) 
			return;
		if (state == states.LOOKING_FOR_TARGET)
			return;
		
		if (character.state == Character.states.IDLE)
			LookTarget ();
	}
	void LookTarget()
	{
		state = states.LOOKING_FOR_TARGET;
		Invoke ("GoToNearestTarget", Random.Range(character.stats.time_to_GoTo_Target.x,character.stats.time_to_GoTo_Target.y));
	}
	void GoToNearestTarget()
	{
		Vector3 newPos = World.Instance.heroesManager.CheckIfHeroIsClose (character.transform.position);
		float distance_to_nearest = Vector3.Distance (transform.position, newPos);
		print (distance_to_nearest);
		state = states.READY_FOR_FIGHT;
	}
}
