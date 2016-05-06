using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject shadowAsset;
    private GameObject shadow;
    public int laneId;
    public Animator anim;
    
    public float distance;
    public bool isPooled;
    private bool isActivated;

    public int offsetToBeOff = 5;

    void Start()
    {
        if (shadowAsset != null)
        {
            shadow = Instantiate(shadowAsset);            
            shadow.transform.SetParent(transform);
            shadow.transform.localScale = Vector3.one;
            shadow.transform.localPosition = Vector3.zero;
        }
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
    }
   
    public void Init(EnemySettings settings, int laneId)
    {
        if (shadow != null) shadow.SetActive(true);
        isPooled = false;
        isActivated = false;
        this.laneId = laneId;
        Enemy_Init(settings, laneId);
    }
    void Update()
    {
        if (isPooled) return;

        Vector3 pos = transform.localPosition;

        if (pos.x + offsetToBeOff  < Game.Instance.gameManager.distance)
        {
            Pool();
        }
        else if (pos.x < Game.Instance.gameManager.distance + 20)
        {
            //isActivated = solo cuando entra dentro del area activa
            if (!isActivated)
            {
                isActivated = true;
                Enemy_Activate();
            }
            Enemy_Update(pos);
        }
    }
    public void Pool()
    {
        Data.Instance.enemiesManager.Pool(this);
        isPooled = true;
        Enemy_Pooled();
    }
    public void Crashed()
    {
        OnCrashed();        
    }
    public void Explote()
    {
        OnExplote();
        if (shadow != null) shadow.SetActive(false);
    }
    public virtual void OnHeroDie() { }
    public virtual void OnSecondaryCollision(Collider2D other) { }
    public virtual void Enemy_Pooled() { }
    public virtual void Enemy_Init(EnemySettings settings, int laneId) { }
    public virtual void Enemy_Activate() { }
    public virtual void Enemy_Update(Vector3 pos)  {  }
    public virtual void OnCrashed() { }
    public virtual void OnExplote() { }
}
