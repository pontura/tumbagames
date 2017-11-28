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
		hitsManager = GetComponent<CharacterHitsManager> ();
		OnStart ();
	}
	void Update () {
		OnUpdate ();
	}
	public void MoveTo(int horizontal, int vertical)
	{
		Vector3 pos = transform.localPosition;
		pos.x += horizontal * Time.deltaTime * speed;
		pos.z += vertical * Time.deltaTime * speed;
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
	public virtual void OnIdle() { }
	public virtual void OnAttack() { }
	public virtual void ReceiveHit(HitArea.types type,  int force) { }
	public void LookAt(bool left)
	{
		if(left)
			asset.transform.localScale = new Vector3 (1, 1, 1);
		else
			asset.transform.localScale = new Vector3 (-1, 1, 1);
	}
}
