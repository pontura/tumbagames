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
	public bool isOverPickable;
	Rigidbody rb;

	public void Init(GameObject theAsset)
	{		
		rb = GetComponent<Rigidbody> ();
		asset = Instantiate (theAsset);

		anim = asset.GetComponent<Animator> ();
		stats = asset.GetComponent<CharacterStats> ();
		totalLife = stats.life;
		enemyAttackManager = GetComponent<EnemyAttackManager> ();

		ia = asset.GetComponent<IA> ();
		ia.Init (this);

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
		Loop ();
        OnIdle();

    }
    public override void OnIdle()
    {
        if(stats.idle_clips.Count>0)
        {
            anim.Play(stats.idle_clips[Random.Range(0, stats.idle_clips.Count)].name);
        }
    }

    void Loop()
	{
		if(state != states.SLEEP)
			StartCoroutine (CancelPhysics());
		else
			rb.isKinematic = true;
		Invoke ("Loop", 0.5f);
	}
	public override void OnAttack ()
	{
        enemyAttackManager.Attack ();
        Invoke("Idle", enemyAttackManager.attackStyle.timeToReset);
    }
    void AttackDone()
    {
        ia.Idle();
    }
	public void Reset()
	{
		CancelInvoke ();
		StopAllCoroutines ();
		Die();
		Events.OnCharacterDie (this);
		GameObject.Destroy (progressBar.gameObject);
		GameObject.Destroy(this.gameObject);
		ia = null;
		bar = null;
		hitArea = null;
		enemyAttackManager = null;
		Events.OnMansPlaining (this, false);
	}
	public override void OnReceiveHit(HitArea hitArea, float force)
	{
        
        if (World.Instance.state == World.states.GAME_OVER)
            return;

		if (ia == null)
			return;
		
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
        if (hitName == "" && stats.receivedAttacks.Count > 0)
            hitName = stats.receivedAttacks[0].animClip.name;

        state = states.HITTED;
		anim.Play (hitName);
		Invoke ("Idle", 0.5f);
		stats.ReceiveHit (force);
		progressBar.SetProgress ((float)stats.life/totalLife);
		if (stats.life <= 0) {
			Die ();
			print (transform.localPosition);
			Events.OnCharacterDie (this);
			Destroy (progressBar.gameObject);
			Destroy (this.gameObject);
		}
	}
	IEnumerator CancelPhysics()
	{
		yield return new WaitForSeconds (0.4f);
		if (rb != null)
			rb.isKinematic = true;
		yield return new WaitForSeconds (0.1f);
		if (rb != null) 
			rb.isKinematic = false;
		
		Vector3 pos = transform.localPosition;
        if (!canMoveOutsideScreen)
        {
            if (transform.localPosition.z < -11)
                pos.z = -11;
            else if (transform.localPosition.z > 10f)
                pos.z = 10f;

            float limitX = 13;
            if (transform.localPosition.x > worldCamera.transform.localPosition.x + limitX)
                pos.x = worldCamera.transform.localPosition.x + limitX;
            else if (transform.localPosition.x < worldCamera.transform.localPosition.x - limitX)
                pos.x = worldCamera.transform.localPosition.x - limitX;
        }
		
		transform.localPosition = pos;				

	}
    public bool canMoveOutsideScreen;
    public void SetMoveOutsideScree(bool _canMoveOutsideScreen)
    {
        this.canMoveOutsideScreen = _canMoveOutsideScreen;
    }

}
