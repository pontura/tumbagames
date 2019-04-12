using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Boss1 : IA {

	public Boss1_states boss1_states;
	public float speed;
    public int rightPaths = 0;
	int totalRightPaths = 1;
	[HideInInspector]
	public float startPosX;
    public IAAlertDistance iaAlertDistance;

    public AnimationClip preAttack;

    public enum Boss1_states
    {
        IDLE,
        TO_LEFT,
        TO_RIGHT,
    }

    public override void OnInit() {
        iaAlertDistance = GetComponent<IAAlertDistance>();
        iaAlertDistance.state = global::IAAlertDistance.states.ON;
        GetComponent<Animator>().Play("idle");
        //enemy.Idle ();
    }
	void ToLeft()
	{
		boss1_states = Boss1_states.TO_LEFT;
		transform.localScale = new Vector3 (1, 1, 1);
		if (rightPaths >= totalRightPaths)
			End ();
		rightPaths++;
	}
	void ToRight()
	{
		boss1_states = Boss1_states.TO_RIGHT;
		transform.localScale = new Vector3 (-1, 1, 1);

	}
	void End()
	{
		boss1_states = Boss1_states.IDLE;
		enemy.Reset ();
	}
	
	public void IAAlertDistance()
	{
        print("ALERTA");
        GetComponent<Animator>().Play(preAttack.name);
        enemy.SetMoveOutsideScree(true);
		startPosX = enemy.transform.position.x;
        Invoke("AttackDelayed", 1);
	}
    void AttackDelayed()
    {
        StopIA();
        ToLeft();
        enemy.Attack();
    }
	public override void OnUpdated()
	{
		if (boss1_states == Boss1_states.IDLE)
			return;

       
		
		Vector3 pos = enemy.transform.position;

		if (boss1_states == Boss1_states.TO_LEFT)
			pos.x -= speed * Time.deltaTime;
		else if (boss1_states == Boss1_states.TO_RIGHT)
			pos.x += speed * Time.deltaTime;
		
		if (pos.x < startPosX - 30 && boss1_states == Boss1_states.TO_LEFT) {
			ToRight ();
		} else if (pos.x > startPosX + 25 && boss1_states == Boss1_states.TO_RIGHT) {
			ToLeft ();
		}

		enemy.transform.position = pos;

        print(boss1_states + " " +  pos);
    }

}
