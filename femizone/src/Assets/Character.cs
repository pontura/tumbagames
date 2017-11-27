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
	public void Idle()
	{
		state = states.IDLE;
		anim.Play ("idle");
	}
	public virtual void ReceiveHit(HitArea.types type,  int force) { }
	public void LookAt(bool left)
	{
		if(left)
			asset.transform.localScale = new Vector3 (1, 1, 1);
		else
			asset.transform.localScale = new Vector3 (-1, 1, 1);
	}
}
