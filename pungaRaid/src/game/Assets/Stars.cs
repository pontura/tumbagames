using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stars : MonoBehaviour {

    public Image star1;
    public Image star2;
    public Image star3;

    private Animator animator;
    private int stars;

    public void Init(int stars)
    {
        this.stars = stars;
        animator = GetComponent<Animator>();
        if (stars == 0)
        {
            SetOff(star1);
            SetOff(star2);
            SetOff(star3);
            if(animator)
              animator.speed = 0;
        }
        if (stars == 1)
        {
            SetOff(star2);
            SetOff(star3);
        }
        else if (stars == 2)
        {
            SetOff(star3);
        }
    }
    void SetOff(Image star)
    {
        star.enabled = false;
    }
    public void Star1Arrived()
    {
        if (stars <1)
        animator.speed = 0;
        Events.OnSoundFX("star001");
    }
    public void Star2Arrived()
    {
        if (stars < 2)
        animator.speed = 0;
        Events.OnSoundFX("star002");
    }
    public void Star3Arrived()
    {
        animator.speed = 0;
        Events.OnSoundFX("star003");
    }
}
