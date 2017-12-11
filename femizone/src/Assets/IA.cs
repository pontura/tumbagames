using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {

	Enemy enemy;
	public states state;
	public Vector3 destination;
	public enum states
	{
		SLEEP,	
		IDLE,
		LOOKING_FOR_TARGET,
		READY_FOR_FIGHT,
		MOVEING,
		HITTED,
		DEFENDING
	}

	void Start () {
		enemy = GetComponent<Character> ().GetComponent<Enemy>();
	}
	public void ReceiveHit()
	{
		state = states.HITTED;
		Invoke ("Idle", 0.5f);
	}
	void Idle()
	{
		CancelInvoke ();
		enemy.Idle ();
		state = states.IDLE;
	}
	void Update() {
		
		if (enemy.state == Character.states.HITTED ||  enemy.state == Character.states.SLEEP || enemy.state == Character.states.DEAD) 
			return;
		if (state == states.IDLE) {
			Vector3 newPos = World.Instance.heroesManager.CheckIfHeroIsClose (enemy);
			if (newPos == Vector3.zero)
				Fight ();
			else
				LookTarget ();
		} else if (state == states.MOVEING)
			Move ();
	}
	void Fight()
	{
		state = states.READY_FOR_FIGHT;
		enemy.Idle ();
		Invoke ("READY_FOR_FIGHT",GetRandom(enemy.stats.time_to_Punch));
	}
	void LookTarget()
	{
		state = states.LOOKING_FOR_TARGET;
		enemy.Idle ();
		Invoke ("LOOKING_FOR_TARGET",GetRandom(enemy.stats.time_to_GoTo_Target));
	}


	float recalculateDelay = 0.8f;
	float recalculateTime;
	void LOOKING_FOR_TARGET()
	{
		//if (state != states.LOOKING_FOR_TARGET)
		//	return;
		
		recalculateTime = 0;
		LookToTarget ();

		if (destination.x > transform.position.x)
			destination.x -= 3f;
		else
			destination.x += 3f;
		
		state = states.MOVEING;
		enemy.Walk ();
	}
	void LookToTarget()
	{
		destination = World.Instance.heroesManager.CheckIfHeroIsClose (enemy);

		Hero hero = World.Instance.heroesManager.GetClosestHero (enemy);

		if (hero.transform.position.x < transform.position.x)
			enemy.LookAt (true);
		else
			enemy.LookAt (false);
	}
	void Move()
	{
		recalculateTime += Time.deltaTime;
		if (recalculateTime > recalculateDelay) {
			LookToTarget ();
			LOOKING_FOR_TARGET ();
			return;
		}
		Vector3 pos = transform.position;
		float _x = Mathf.Abs(pos.x - destination.x);
		float _z = Mathf.Abs(pos.z - destination.z);
		if (_x < 0.2f && _z < 0.2f) {
			Fight ();
			return;
		}
		float speed = enemy.stats.speed * Time.deltaTime;
		if (pos.x < destination.x)
			pos.x += speed;
		else if (pos.x > destination.x)
			pos.x -= speed;
		if (pos.z < destination.z)
			pos.z += speed;
		else if (pos.z > destination.z)
			pos.z -= speed;

		transform.position = pos;	
	}

	void READY_FOR_FIGHT()
	{
		if (state != states.READY_FOR_FIGHT)
			return;
		LookToTarget ();
		enemy.GetComponent<Enemy>().Attack();
		Invoke ("Idle", 1f);
	}

	float GetRandom(Vector2 v)
	{
		return Random.Range(v.x,v.y);
	}
}
