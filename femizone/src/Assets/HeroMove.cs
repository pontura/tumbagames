using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    float delayToRun = 0.4f;
    float lastTimeWalk;
    int offsetMoveX = 10;
    Hero hero;
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
                hero.speed = normalSpeed + (normalSpeed / 2);
                break;
        }
    }
    public void ResetMove()
    {
        lastTimeWalk -= 1;
    }
    public void ChekToMove(int dirX, int dirY)
    {
        if (hero.state == Hero.states.IDLE)
        {
            CheckType();
            CheckIfRecenter();
        }

        if (dirX < 0 && (transform.localPosition.x < hero.worldCamera.transform.localPosition.x - offsetMoveX))
            dirX = 0;
        else if (dirX > 0 && (transform.localPosition.x > hero.worldCamera.transform.localPosition.x + offsetMoveX))
            dirX = 0;

        hero.MoveTo(dirX, dirY);
    }
    void CheckIfRecenter()
    {
        if (transform.localPosition.x < hero.worldCamera.transform.localPosition.x - offsetMoveX)
            transform.localPosition = new Vector3(hero.worldCamera.transform.localPosition.x - offsetMoveX, transform.localPosition.y, transform.localPosition.z);
        else if (transform.localPosition.x > hero.worldCamera.transform.localPosition.x + offsetMoveX)
            transform.localPosition = new Vector3(hero.worldCamera.transform.localPosition.x + offsetMoveX, transform.localPosition.y, transform.localPosition.z);
    }
    void CheckType()
    {
        float lastTimeWalkDiff = Time.time - lastTimeWalk;
        lastTimeWalk = Time.time;

        if (lastTimeWalkDiff < delayToRun)
            ChangeType(types.RUN);
        else
            ChangeType(types.NORMAL);
    }
    public void OnIdle()
    {
        ChangeType(types.NORMAL);
    }
}
