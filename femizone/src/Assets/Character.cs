using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	
	private InputManager inputManager;
	public Animator anim;
	public float speed = 10;
	public states state;
	public enum states
	{
		IDLE,
		WALK
	}
	void Start () {
		inputManager = GetComponent<InputManager> ();	
	}
	void Update () {
		if ((inputManager.HorizontalDirection != 0 || inputManager.VerticalDirection != 0)) {			
			if(state != states.WALK)
				Walk ();
		}
		else if (state != states.IDLE)
			Idle ();

		if (inputManager.HorizontalDirection < 0)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (inputManager.HorizontalDirection > 0)
			transform.localScale = new Vector3 (1, 1, 1);
		
		Vector3 pos = transform.localPosition;
		pos.x += inputManager.HorizontalDirection * Time.deltaTime * speed;
		pos.y += inputManager.VerticalDirection * Time.deltaTime * speed;
		transform.localPosition = pos;
	}
	void Walk()
	{
		state = states.WALK;
		anim.Play ("pungaRun");
	}
	void Idle()
	{
		state = states.IDLE;
		anim.Play ("pungaTemplate");
	}
}
