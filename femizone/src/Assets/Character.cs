using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : SceneObject {
	
	private InputManager inputManager;
	public Animator anim;
	public float speed = 10;
	public states state;
	public int HorizontalDirection;
	public enum states
	{
		IDLE,
		WALK,
		HIT
	}
	void Start () {
		inputManager = GetComponent<InputManager> ();	
		Events.OnCharacterHit += OnCharacterHit;
	}
	void Update () {
		if ((inputManager.HorizontalDirection != 0 || inputManager.VerticalDirection != 0)) {			
			if(state != states.WALK)
				Walk ();
		}
		else if (state == states.WALK)
			Idle ();

		if (inputManager.HorizontalDirection < 0)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (inputManager.HorizontalDirection > 0)
			transform.localScale = new Vector3 (1, 1, 1);
		
		Vector3 pos = transform.localPosition;
		pos.x += inputManager.HorizontalDirection * Time.deltaTime * speed;
		pos.z += inputManager.VerticalDirection * Time.deltaTime * (speed*10);
		transform.localPosition = pos;
	}
	void OnCharacterHit(int playerID, int hitID)
	{
		print ("pega playerID" + playerID + " hit: " + hitID);
		if(hitID==1)
			anim.Play ("punch_1");
		else if(hitID==2)
			anim.Play ("punch_2");
		state = states.HIT;
		Invoke ("Idle", 0.3f);
	}
	void Walk()
	{
		state = states.WALK;
		anim.Play ("walk");
	}
	void Idle()
	{
		state = states.IDLE;
		anim.Play ("idle");
	}
}
