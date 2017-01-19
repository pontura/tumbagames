using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneButton : MonoBehaviour {

    public int id;
    public int moodID;
    public int seccionalID;
    public bool unlocked;
    public bool canBePaid;
    public GameObject iconLock;
    public GameObject iconUnlock;
    private Seccionales seccionales;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        SetState();
    }
    public void Init(bool unlocked, Seccionales seccionales, bool canBePaid)
    {
        this.canBePaid = canBePaid;
        this.seccionales = seccionales;
        this.unlocked = unlocked;
       
        Invoke("SetState", 0.1f);
    }
    void SetState()
    {
        if (unlocked)
            Unlock();
        else if (canBePaid)
            CanBePaid();
        else
            Lock();
    }
    public void Unlock()
    {
        anim.Play("unlocked");
    }
    void Lock()
    {
        anim.Play("off");
    }
    void CanBePaid()
    {
        anim.Play("on");
    }
    public void Clicked()
    {
        seccionales.Clicked(this);
    }
}
