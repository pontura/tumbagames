using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public Transform bar;
	public ProgressBar progressBar;
	CharacterStats stats;

	public void Init()
	{
		progressBar = UI.Instance.progressBarManager.CreateProgressBar (this);
		stats = GetComponent<CharacterStats> ();
	}
	public override void ReceiveHit(HitArea.types type, int force)
	{
		if (state == states.HITTED)
			return;
		print ("ReceiveHit " + type);
		string hitName = "hit_punch";

		switch (type) {
		case HitArea.types.HIT_FRONT:
			hitName = "hit_punch";
			break;
		case HitArea.types.HIT_DOWN:
			hitName = "hit_punch";
			break;
		case HitArea.types.HIT_BACK:
			hitName = "hit_punch_back";
			break;
		case HitArea.types.HIT_UPPER:
			hitName = "hit_upper";
			break;
		}

		state = states.HITTED;
		anim.Play (hitName);
		Invoke ("Idle", 0.5f);
		stats.ReceiveHit (force);
		progressBar.SetProgress ((float)stats.life/10);
	}
}
