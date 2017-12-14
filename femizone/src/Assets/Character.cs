using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SceneObject {

	public GameObject asset;
	public Animator anim;
	public float speed = 10;
	public states state;
	public int HorizontalDirection;
	public CharacterStats stats;
	public CharacterHitsManager hitsManager;
	private Vector2 limits_Z;

	public enum states
	{
		SLEEP,
		IDLE,
		WALK,
		HITTING,
		HITTED,
		DEAD
	}

	public virtual void OnStart() { }
	public virtual void OnUpdate() { }

	void Start () {	
		limits_Z = Data.Instance.settings.limits_z;
		hitsManager = GetComponent<CharacterHitsManager> ();
		OnStart ();
	}
	void Update () {
		if (state == states.HITTED)
			Hitted ();
		else
			OnUpdate ();
	}
	public void MoveTo(int horizontal, int vertical)
	{
		Vector3 pos = transform.localPosition;
		pos.x += horizontal * Time.deltaTime * speed;
		pos.z += vertical * Time.deltaTime * speed;

		if (pos.z > limits_Z [1])
			pos.z = limits_Z [1];
		else
		if (pos.z < limits_Z [0])
			pos.z = limits_Z [0];
		
		transform.localPosition = pos;
	}
	public void Walk()
	{
		if (state == states.HITTING)
			return;
		state = states.WALK;
		anim.Play ("walk");
	}
	public void Die()
	{
		if (state == states.DEAD)
			return;
		state = states.DEAD;
	}
	public void Idle()
	{
		state = states.IDLE;
		anim.Play ("idle");
		OnIdle();
	}
	public void Attack()
	{
		state = states.HITTING;
		OnAttack ();
	}
	public void ReceiveHit(HitArea hitArea,  int force) 
	{ 
		StartHit(hitArea);
		OnReceiveHit (hitArea.type,force);
	}

	public virtual void OnIdle() { }
	public virtual void OnAttack() { }
	public virtual void OnReceiveHit(CharacterHitsManager.types type,  int force) { }
	public void LookAt(bool left)
	{
		if(left)
			asset.transform.localScale = new Vector3 (1, 1, 1);
		else
			asset.transform.localScale = new Vector3 (-1, 1, 1);
	}


	float _hittedPower;
	float hittedDirection = 1;
	void StartHit(HitArea hitArea) 
	{ 
		if (hitArea.character.transform.position.x > transform.position.x)
			hittedDirection = -1;
		else
			hittedDirection = 1;
		_hittedPower = hitArea.character.stats.hittedPower;
	}
	void Hitted()
	{
		Vector3 pos = transform.position;
		_hittedPower /= 1.15f;
		if (_hittedPower < 0)
			return;
		pos.x += (_hittedPower * Time.deltaTime ) * hittedDirection;
		transform.position = pos;
	}
}
