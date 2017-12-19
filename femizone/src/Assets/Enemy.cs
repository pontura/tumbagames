using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public Transform bar;
	public ProgressBar progressBar;
	public EnemyAttackManager enemyAttackManager;
	public IA ia;
	public HitArea hitArea;
	int totalLife;

	void Start()
	{
		totalLife = stats.life;
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

	public override void OnReceiveHit(HitArea hitArea, int force)
	{

		if(!progressBar.isOn)
			progressBar.Show ();

		if (ia.CheckForDefense () == true) {
			StartHit (hitArea);
			return;
		}
		
		ia.ReceiveHit ();

		string hitName = "";

		foreach (AttackStyle attackStyle in stats.receivedAttacks) {
			if (attackStyle.type == hitArea.type)
				hitName = attackStyle.animClip.name;
		}

		state = states.HITTED;
		anim.Play (hitName);
		Invoke ("Idle", 0.5f);
		stats.ReceiveHit (force);
		progressBar.SetProgress ((float)stats.life/totalLife);
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
