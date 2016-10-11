using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private float speed = 5;
    public states state;
    private Game game;
    private bool right;
    private Animator anim;

    public enum states
    {
        RUNNING,
        JUMPING,
        IN_FLOOR
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
            Positionate(12);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            Positionate(-12);
        }

        anim.Play("run");
    }
    void Update()
    {
        if (state != states.IN_FLOOR)
        {
            if (!right && transform.localPosition.x > -5)
                Jump();
            if (right && transform.localPosition.x < 5)
                Jump();
            Positionate(Time.deltaTime * speed);
        }
        else
            Positionate(-(game.speed / game.speedInFloor));
    }
    void Jump()
    {
        if (state == states.JUMPING) return;
        anim.Play("jump");
        state = states.JUMPING;
        Invoke("Floor", 0.8f);
    }
    void Floor()
    {
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
