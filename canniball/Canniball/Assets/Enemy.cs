using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private float speed = 5;
    public states state;
    private Game game;
    private bool right;
    private Animator anim;
    public GameObject carne;

    public enum states
    {
        RUNNING,
        JUMPING,
        IN_FLOOR,
        COMIO
    }
    void Start()
    {
        carne.SetActive(false);
        int id = 1;
        string url = "";
        int level = Data.Instance.GetComponent<LevelsData>().level;
        int character = Random.Range(1,4);
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            url = "";
            switch (id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    url = "enemies/" + level + "_" + character + "/corriendo000" + id;
                    break;
                case 7:
                    url = "enemies/" + level + "_" + character + "/takle";
                    break;
                case 8:
                    url = "enemies/" + level + "_" + character + "/floor";
                    break;
                    
            }
            if (url != "")
            {
                print(url);
                sr.sprite = Resources.Load<Sprite>(url) as Sprite; 
            }
            id++;
        }
        Invoke("ActivateAnimation", 0.1f);
    }
    void ActivateAnimation()
    {
        GetComponent<Animator>().enabled = true;
    }
    public void Init(Game game, bool right)
    {
        anim = GetComponent<Animator>();
        print(right);
        this.right = right;
        this.game = game;
        state = states.RUNNING;

        if (right)
        {
            transform.localScale = new Vector2(-1, 1);
            speed = -6;
            Positionate(14);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            Positionate(-14);
        }

        anim.Play("run");
    }
    void Update()
    {
        if (game.state == Game.states.READY) return;

        if (game.state == Game.states.COMIDO)
        {
            if (state != states.RUNNING)
            {
                anim.Play("run");
                state = states.RUNNING;
            }
            transform.localScale = new Vector2(1, 1);
            speed = 6;
            right = false;
            Positionate(Time.deltaTime * speed);
        }
        else if (state != states.IN_FLOOR)
        {
            if (game.state == Game.states.DEAD)
            {
                if (transform.localPosition.x < 1 && transform.localPosition.x > -1)
                {
                    carne.SetActive(true);
                    Events.OnHeroComido();
                    state = states.COMIO;
                    anim.Play("run");
                }
            }
            else
            {
                if (!right && transform.localPosition.x > -5)
                    Jump();
                if (right && transform.localPosition.x < 5)
                    Jump();
            }
            Positionate(Time.deltaTime * speed);
        }
        else
            Positionate(-(game.speed / game.speedInFloor));
    }
    void Jump()
    {
        if (state == states.JUMPING) return;
        Events.OnSoundFX("takle");
        anim.Play("jump");
        state = states.JUMPING;
        Invoke("Floor", 0.8f);
    }
    void Floor()
    {
        if (game.state == Game.states.COMIDO || game.state == Game.states.DEAD) return;
        anim.Play("floor");
        state = states.IN_FLOOR;
    }
    void Positionate(float _x)
    {
        Vector3 pos = transform.localPosition;
        pos.x += _x;
        pos.y = 0;
        transform.localPosition = pos;
    }
}
