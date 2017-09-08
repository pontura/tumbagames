using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SceneObject {
	
	public Animator anim;
	public float speed = 10;
	public states state;
	public int HorizontalDirection;
	public CharacterStats stats;

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
	public void OnCharacterHit(int playerID, int hitID)
	{
		if (state == states.HITTING)
			return;
		
		if(hitID==1)
			anim.Play ("punch_1");
		else if(hitID==2)
			anim.Play ("punch_2");
		state = states.HITTING;
		Invoke ("Idle", 0.3f);
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
