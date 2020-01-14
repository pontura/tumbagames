using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {
	
	[HideInInspector]
	public Enemy enemy;

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
		DEFENDING,
		STOP_IA
	}

	public void Init (Enemy _enemy) {
		this.enemy = _enemy;
		OnInit ();
	}
	public virtual void OnInit() {}

    public bool CheckForDefense()
	{
		if (Random.Range (0, 10) < 3) {
			enemy.Defense ();
			Invoke ("Idle", 1f);
			return true;
		} else
			return false;
	}
	public virtual void ReceiveHit()
	{
		state = states.HITTED;
		Invoke ("Idle", 0.5f);
	}
    public void Idle()
	{
		CancelInvoke ();
		enemy.Idle ();
		state = states.IDLE;
	}
	void Update() {
        OnUpdated();
        if (state == states.STOP_IA)
			return;
		if (enemy == null || enemy.state == Character.states.DEFENDING || enemy.state == Character.states.HITTED ||  enemy.state == Character.states.SLEEP || enemy.state == Character.states.DEAD) 
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
    public virtual void OnUpdated() { }
    void Fight()
	{
        if (enemy.stats.attacks.Count > 0)
        {
            state = states.READY_FOR_FIGHT;
            enemy.Idle();
            Invoke("READY_FOR_FIGHT", GetRandom(enemy.stats.time_to_Punch));
        }   
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
		recalculateTime = 0;
		LookToTarget ();

		if (destination.x > enemy.transform.position.x)
			destination.x -= 3f;
		else
			destination.x += 3f;
		
		state = states.MOVEING;
		enemy.Walk ();
	}
	public virtual void LookToTarget()
	{
		destination = World.Instance.heroesManager.CheckIfHeroIsClose (enemy);
	//	destination += enemy.stats.offset;
		Hero hero = World.Instance.heroesManager.GetClosestHero (enemy.transform);
		if (hero == null)
			return;
		if (hero.transform.position.x < enemy.transform.position.x)
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
		Vector3 pos = enemy.transform.position;
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

		enemy.transform.position = pos;	
	}

	public virtual void READY_FOR_FIGHT()
	{
		if (state != states.READY_FOR_FIGHT)
			return;
		LookToTarget ();

		enemy.Attack();
		Invoke ("Idle", enemy.enemyAttackManager.attackStyle.timeToReset);
    }

	float GetRandom(Vector2 v)
	{
		return Random.Range(v.x,v.y);
	}

	public void StopIA()
	{
		CancelInvoke ();
		state = states.STOP_IA;
	}

    public virtual void OnDie()
    {
        Destroy(enemy.gameObject);
    }
}
