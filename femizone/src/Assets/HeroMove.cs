using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    float lastTimeWalk;
    int offsetMoveX = 10;
    Hero hero;
    float runValue = 3;
    float normalSpeed;
    public types type;
    public enum types
    {
        NORMAL,
        RUN
    }

    public void Init(Hero hero)
    {
        normalSpeed = hero.speed;
        this.hero = hero;
    }
    public void ChangeType(types type)
    {
        this.type = type;
        switch (type)
        {
            case types.NORMAL:
                hero.speed = normalSpeed;
                break;
            case types.RUN:
                hero.speed = normalSpeed + runValue;
                break;
        }
        print("speed = " +  hero.speed);
    }

    public void ChekToMove(int dirX, int dirY)
    {
        if (hero.state == Hero.states.IDLE)
            CheckType();
        if (dirX < 0 && (transform.localPosition.x < hero.worldCamera.transform.localPosition.x - offsetMoveX))
            dirX = 0;
        else if (dirX > 0 && (transform.localPosition.x > hero.worldCamera.transform.localPosition.x + offsetMoveX))
            dirX = 0;

        hero.MoveTo(dirX, dirY);
    }
    void CheckType()
    {
        float lastTimeWalkDiff = Time.time - lastTimeWalk;
        print(lastTimeWalkDiff);
        lastTimeWalk = Time.time;

        if (lastTimeWalkDiff < 0.2f)
            ChangeType(types.RUN);
        else
            ChangeType(types.NORMAL);
    }
    public void OnIdle()
    {
        ChangeType(types.NORMAL);
    }
}
