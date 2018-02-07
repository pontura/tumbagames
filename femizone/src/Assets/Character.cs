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
	private int limit_to_walk;
	WorldCamera worldCamera;

	public enum states
	{
		SLEEP,
		IDLE,
		WALK,
		HITTING,
		HITTED,
		DEAD,
		DEFENDING
	}

	public virtual void OnStart() { }
	public virtual void OnUpdate() { }

	void Start () {	
		worldCamera = World.Instance.worldCamera;
		limits_Z = Data.Instance.settings.limits_z;
		limit_to_walk = Data.Instance.settings.limit_to_walk;
		hitsManager = GetComponent<CharacterHitsManager> ();
		OnStart ();
	}
	void Update () {
		if (state == states.DEAD)
			return;
		if (state == states.HITTED || state == states.DEFENDING)
			Retrocede ();
		else
			OnUpdate ();

		if (isVibrating) {
			Vector3 pos = transform.localPosition;
			vibratingDirection *= -1;
			pos.x += vibratingDirection;
			transform.localPosition = pos;
		}
	}
	public void MoveTo(int horizontal, int vertical)
	{
		Vector3 pos = transform.localPosition;
		pos.x += horizontal * Time.deltaTime * speed;
		pos = CheckPositionPosible (pos);
		pos.z += vertical * Time.deltaTime * speed;

		if (pos.z > limits_Z [1])
			pos.z = limits_Z [1];
		else
		if (pos.z < limits_Z [0])
			pos.z = limits_Z [0];
		
		transform.localPosition = pos;
	}
	Vector3 CheckPositionPosible(Vector3 pos)
	{
		float limitX = worldCamera.transform.position.x;
		if (pos.x < limitX - limit_to_walk)
			pos.x = limitX - limit_to_walk;
		else
			if (pos.x > limitX + limit_to_walk)
				pos.x = limitX + limit_to_walk;
		return pos;			
	}
	public virtual void Walk()
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
		anim.Play ("death");
		OnDie ();
	}
	public virtual void Idle()
	{
		state = states.IDLE;
		anim.Play ("idle");
		OnIdle();
	}
	public void Defense()
	{
		state = states.DEFENDING;
		anim.Play ("defense");
	}
	public void Attack()
	{
		state = states.HITTING;
		OnAttack ();
	}
	public void ReceiveHit(HitArea hitArea,  int force) 
	{ 
		StartHit(hitArea);
		OnReceiveHit (hitArea,force);
	}
	public virtual void OnDie() { }
	public virtual void OnIdle() { }
	public virtual void OnAttack() { }
	public virtual void OnReceiveHit(HitArea hitArea,  int force) { }
	public void LookAt(bool left)
	{
		if(left)
			asset.transform.localScale = new Vector3 (1, 1, 1);
		else
			asset.transform.localScale = new Vector3 (-1, 1, 1);
	}


	float _hittedPower;
	float hittedDirection = 1;
	public void StartHit(HitArea hitArea) 
	{ 
		if (hitArea.character.transform.position.x > transform.position.x)
			hittedDirection = -1;
		else
			hittedDirection = 1;
		_hittedPower = hitArea.character.stats.hittedPower;
	}
	void Retrocede()
	{
		Vector3 pos = transform.position;
		_hittedPower /= 1.15f;
		if (_hittedPower < 0)
			return;
		pos.x += (_hittedPower * Time.deltaTime ) * hittedDirection;
		transform.position = pos;
	}
	public virtual void OnFreeze()
	{
		anim.speed = 0f;
		isVibrating = true;
		Invoke ("ResetFreeze", 0.22f);
	}
	bool isVibrating = false;
	void ResetFreeze()
	{
		isVibrating = false;
		anim.speed = 1f;
	}
	float vibratingDirection = 0.3f;
}
