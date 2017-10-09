using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SceneObject {
	
	public Animator anim;
	public float speed = 10;
	public states state;
	public int HorizontalDirection;
	public CharacterStats stats;
	public CharacterHitsManager hitsManager;

	public enum states
	{
		IDLE,
		WALK,
		HITTING,
		HITTED
	}

	public virtual void OnStart() { }
	public virtual void OnUpdate() { }

	void Start () {	
		hitsManager = GetComponent<CharacterHitsManager> ();
		stats = GetComponent<CharacterStats> ();	
		OnStart ();
	}
	void Update () {
		OnUpdate ();
	}
	public void MoveTo(int horizontal, int vertical)
	{
		Vector3 pos = transform.localPosition;
		pos.x += horizontal * Time.deltaTime * speed;
		pos.z += vertical * Time.deltaTime * (speed*10);
		transform.localPosition = pos;
	}
	public void Walk()
	{
		if (state == states.HITTING)
			return;
		state = states.WALK;
		anim.Play ("walk");
	}
	public void Idle()
	{
		state = states.IDLE;
		anim.Play ("idle");
	}
	public void ReceiveHit(int force)
	{
		if (state == states.HITTED)
			return;
		state = states.HITTED;
		anim.Play ("hit_punch");
		Invoke ("Idle", 0.3f);
	}
}
