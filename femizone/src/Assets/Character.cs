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
	private Vector2 limits_Z;
	private int limit_to_walk;
	public WorldCamera worldCamera;

	public enum states
	{
		SLEEP,
		IDLE,
		WALK,
		HITTING,
		HITTED,
		DEAD,
		DEFENDING,
        STRESS,
        FREEZED
	}

	public virtual void OnStart() { }
	public virtual void OnUpdate() { }

	void Start () {	
		worldCamera = World.Instance.worldCamera;
		limits_Z = Data.Instance.settings.limits_z;
		limit_to_walk = Data.Instance.settings.limit_to_walk;
		hitsManager = GetComponent<CharacterHitsManager> ();
		OnStart ();        
        Events.OnCutsceneDone += OnCutsceneDone;
    }
    public virtual void OnDestroy()
    {
        Events.OnCutsceneDone -= OnCutsceneDone;
    }
    void OnCutsceneDone()
    {
        state = states.IDLE;
        anim.Play("idle");
    }
    
    void Update () {
        if (state == states.FREEZED)
            return;
        if (state == states.STRESS || state == states.DEAD)
			return;
		if (state == states.HITTED || state == states.DEFENDING)
			Retrocede ();
		else
			OnUpdate ();

		if (isVibrating) {
			Vector3 pos = transform.localPosition;
			vibratingDirection *= -1;
            vibratingDirection /= 1.02f;

            pos.x += vibratingDirection;
			transform.localPosition = pos;
		}
	}
	public void MoveTo(int horizontal, int vertical)
	{
        if (state == states.STRESS || state == states.DEAD || horizontal ==0 && vertical == 0)
			return;
		Vector3 pos = transform.localPosition;
		pos.x += horizontal * Time.deltaTime * speed;
		pos = CheckPositionPosible (pos);
		pos.z += vertical * Time.deltaTime * speed;

        if (pos.z > limits_Z[1] && vertical > 0)
            vertical = 0;
        else if (pos.z < limits_Z[0] && vertical < 0)
            vertical = 0;

        transform.Translate (new Vector3(horizontal* Time.deltaTime * speed, 0, vertical* Time.deltaTime * speed));
		//transform.localPosition = pos;
	}
	Vector3 CheckPositionPosible(Vector3 pos)
	{
		float limitX = worldCamera.transform.position.x;
		if (pos.x < limitX - limit_to_walk)
			pos.x = limitX - limit_to_walk;
		else
			if (pos.x > limitX + limit_to_walk)
				pos.x = limitX + limit_to_walk;
		return pos;			
	}
	public virtual void Walk()
	{
        if (state == states.STRESS || state == states.DEAD || state == states.HITTING)
			return;
		state = states.WALK;
		anim.Play ("walk");
	}
	public void Die()
	{
		if (state == states.DEAD)
			return;
		state = states.DEAD;
        anim.speed = 1;
        anim.Play ("death");        
		OnDie ();
	}
	public virtual void Idle()
	{
        if (state == states.STRESS)
            return;
        if (state == states.DEAD)
            return;
        state = states.IDLE;
		anim.Play ("idle");
		OnIdle();
	}
    public void AnimateSpecificAction(string animName)
    {
        anim.Play(animName);
    }
	public void Defense()
	{
        if (state == states.DEAD)
            return;
        state = states.DEFENDING;
		anim.Play ("defense");
	}
	public void OnSpecial1()
	{
        if (state == states.DEAD)
            return;
        state = states.HITTING;
		anim.Play ("special1");
	}
	public void Attack()
	{
        if (state == states.DEAD)
            return;
        state = states.HITTING;
		OnAttack ();
	}
	public void ReceiveHit(HitArea hitArea,  float force) 
	{
        if (state == states.DEAD)
            return;
		StartHit(hitArea);
		if (state == states.DEFENDING) {
			_hittedPower /= 4;
		}
        OnReceiveHit (hitArea,force);
	}
	public virtual void OnFire(bool isOver) { }
	public virtual void OnDie() { }
	public virtual void OnIdle() { }
	public virtual void OnAttack() { }
	public virtual void OnReceiveHit(HitArea hitArea,  float force) { }
	public void LookAt(bool left)
	{
		if(left)
			asset.transform.localScale = new Vector3 (1, 1, 1);
		else
			asset.transform.localScale = new Vector3 (-1, 1, 1);
	}


	float _hittedPower;
	float hittedDirection = 1;
	public void StartHit(HitArea hitArea) 
	{
        if (hitArea.character == null)
            return;
		if (hitArea.character.transform.position.x > transform.position.x)
			hittedDirection = -1;
		else
			hittedDirection = 1;
		_hittedPower = hitArea.character.stats.hittedPower;
	}
	void Retrocede()
	{
		Vector3 pos = transform.position;
		_hittedPower /= 1.15f;
		if (_hittedPower < 0)
			return;
		pos.x += (_hittedPower * Time.deltaTime ) * hittedDirection;
		transform.position = pos;
	}
	public virtual void OnFreeze()
	{
        if (state == states.DEAD)
            return;
        anim.speed = 0f;
        vibratingDirection = 0.5f;

        isVibrating = true;
		Invoke ("ResetFreeze", 0.25f);
	}
	bool isVibrating = false;
	void ResetFreeze()
	{
        isVibrating = false;
		anim.speed = 1f;
	}
	float vibratingDirection;
}
