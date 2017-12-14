using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public Transform bar;
	public ProgressBar progressBar;
	public EnemyAttackManager enemyAttackManager;
	public IA ia;
	public HitArea hitArea;

	void Start()
	{
		enemyAttackManager = GetComponent<EnemyAttackManager> ();
		ia = GetComponent<IA> ();
	}

	public void Init(GameObject theAsset)
	{		
		asset = Instantiate (theAsset);

		anim = asset.GetComponent<Animator> ();
		stats = asset.GetComponent<CharacterStats> ();

		asset.transform.SetParent (transform);
		asset.transform.localPosition = stats.offset;
		asset.transform.localScale = Vector3.one;
		asset.transform.localEulerAngles = new Vector3 (30, 0, 0);	

		foreach (HitArea ha in asset.gameObject.GetComponentsInChildren<HitArea>()) {
			if (ha.name == "HitArea")
				hitArea = ha;
			ha.character = this;
		}
		
		foreach (Transform go in asset.gameObject.GetComponentsInChildren<Transform>())
			if (go.name == "bar") {
				bar = go;
			}
		
		progressBar = UI.Instance.progressBarManager.CreateProgressBar (this);
		progressBar.Hide ();
	}

	public override void OnReceiveHit(CharacterHitsManager.types type, int force)
	{
		
		if(!progressBar.isOn)
			progressBar.Show ();
		
		ia.ReceiveHit ();
		//if (state == states.HITTED)
		//	return;

		string hitName = "";

		print ("recive: " + type);
		foreach (AttackStyle attackStyle in stats.receivedAttacks) {
			print (attackStyle.type + " - " + attackStyle.animClip.name);
			if (attackStyle.type == type)
				hitName = attackStyle.animClip.name;
		}
		print ("hace: " + hitName);

		state = states.HITTED;
		anim.Play (hitName);
		Invoke ("Idle", 0.5f);
		stats.ReceiveHit (force);
		progressBar.SetProgress ((float)stats.life/10);
		if (stats.life <= 0) {
			Die ();
			Destroy (progressBar.gameObject);
			Destroy (this.gameObject);
		}
	}
	public override void OnAttack() 
	{
		enemyAttackManager.Attack ();
	}
}
