using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public Transform bar;
	public ProgressBar progressBar;
	CharacterStats stats;


	public void Init(GameObject theAsset)
	{
		asset = Instantiate (theAsset);
		asset.transform.SetParent (transform);
		asset.transform.localPosition = Vector3.zero;
		asset.transform.localScale = Vector3.one;
		asset.transform.localEulerAngles = new Vector3 (45, 0, 0);

		anim = asset.GetComponent<Animator> ();
		stats = GetComponent<CharacterStats> ();

		foreach (HitArea ha in asset.gameObject.GetComponentsInChildren<HitArea>())
			ha.character = this;
		
		foreach (Transform go in asset.gameObject.GetComponentsInChildren<Transform>())
			if (go.name == "bar") {
				bar = go;
			}
		
		progressBar = UI.Instance.progressBarManager.CreateProgressBar (this);
	}
	public override void ReceiveHit(HitArea.types type, int force)
	{
		//if (state == states.HITTED)
		//	return;

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
