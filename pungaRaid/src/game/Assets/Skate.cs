using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : Enemy {
	
	public states state;
	public float speed;

	public enum states
	{
		IDLE,
		CRASHED
	}

	private string currentAnim;
	private BoxCollider2D collider2d;

	override public void Enemy_Activate()
	{
		collider2d = GetComponent<BoxCollider2D>();
		collider2d.enabled = true;
	}
	override public void Enemy_Init(EnemySettings settings, int laneId)
	{
		anim = GetComponentInChildren<Animator>();
		anim.Play("idle", 0, 0);

		if(settings.speed != 0)
			this.speed = settings.speed;
	}
	override public void Enemy_Pooled()
	{
		state = states.IDLE;
	}
	override public void Enemy_Update(Vector3 pos)
	{
		pos.x -= speed * Time.deltaTime;
		transform.localPosition = pos;
	}
	override public void OnCrashed()
	{
		if (state == states.CRASHED) return;
		state = states.CRASHED;
		anim.Play("pung");
	}
	override public void OnExplote()
	{
		if (state == states.CRASHED) return;
		state = states.CRASHED;

		Events.OnAddExplotion(laneId, (int)transform.localPosition.x);

		Pool();
	}
	bool moveing;
	override public void OnSecondaryCollision(Collider2D other)
	{
		print ("SKATE choca con::::: " + other);
		if (moveing) return; 
		moveing = true;
		int newLaneId = Game.Instance.gameManager.characterManager.lanes.GetNextFreeLane(laneId, distance);
		Lane newLane = Game.Instance.gameManager.characterManager.lanes.all[newLaneId];
		Game.Instance.gameManager.characterManager.lanes.changeEnemyLane(this, newLane);
		Invoke("LaneChanged", 0.5f);

	}
	void LaneChanged()
	{
		moveing = false;
	}
}
