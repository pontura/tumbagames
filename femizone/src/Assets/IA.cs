using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {

	Character character;
	public states state;
	public Vector3 destination;
	public enum states
	{
		SLEEP,
		LOOKING_FOR_TARGET,
		READY_FOR_FIGHT,
		MOVEING
	}

	void Start () {
		character = GetComponent<Character> ();
	}

	void Update() {
		
		if (character.state == Character.states.SLEEP || character.state == Character.states.DEAD) 
			return;
		if (state == states.LOOKING_FOR_TARGET)
			return;
		if (state == states.MOVEING)
			Move();
		if (character.state == Character.states.IDLE)
			LookTarget ();
	}
	void LookTarget()
	{
		state = states.LOOKING_FOR_TARGET;
		Invoke ("GoToNearestTarget",GetRandom(character.stats.time_to_GoTo_Target));
	}
	float GetRandom(Vector2 v)
	{
		return Random.Range(v.x,v.y);
	}
	void GoToNearestTarget()
	{
		Vector3 newPos = World.Instance.heroesManager.CheckIfHeroIsClose (character);
		if (newPos == Vector3.zero)
			Fight ();
		else
			MoveTo (newPos);
	}
	void MoveTo(Vector3 _destination)
	{
		if (destination.x > transform.position.x) {
			character.asset.transform.localScale = new Vector3 (-1, 1, 1);
			_destination.x -= 3f;
		} else {
			character.asset.transform.localScale = new Vector3 (1, 1, 1);
			_destination.x += 3f;
		}
		this.destination = _destination;
		state = states.MOVEING;
		character.Walk ();
	}
	void Move()
	{
		Vector3 pos = transform.position;
		if (Vector3.Distance (pos, destination) < 0.5f) {
			GoToNearestTarget ();
			return;
		}
		if (pos.x < destination.x)
			pos.x += character.stats.speed * Time.deltaTime;
		else if (pos.x > destination.x)
			pos.x -= character.stats.speed * Time.deltaTime;
		if (pos.z < destination.z)
			pos.z += character.stats.speed * Time.deltaTime;
		else if (pos.z > destination.z)
			pos.z -= character.stats.speed * Time.deltaTime;

		transform.position = pos;	
	}
	void Fight()
	{
		state = states.READY_FOR_FIGHT;
		character.Idle ();
	}
}
