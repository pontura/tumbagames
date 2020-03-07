using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    float delayToRun = 0.4f;
    float lastTimeWalk;
    int offsetMoveX = 14;
    Hero hero;
    float normalSpeed;
    public types type;
    public enum types
    {
        NORMAL,
        RUN,
        AUTOMATIC
    }

    public void Init(Hero hero)
    {
        normalSpeed = hero.speed;
        this.hero = hero;
    }
    Vector3 forcePos;
    public void ForceScreenCenter()
    {
        forcePos = new Vector3(World.Instance.newXLimit, transform.position.y, -6);
        print("forcePos: " + forcePos);
        type = types.AUTOMATIC;
        hero.speed = normalSpeed;
    }
    private void Update()
    {            
        if (type != types.AUTOMATIC)
            return;
        float distance = Vector3.Distance(transform.position, forcePos);
        print("distance: " + distance + " transform.position, forcePos " + transform.position + " " +  forcePos);
        if (Mathf.Abs(distance) > 1)
        {
            int _z = 0;
            int _x = 0;
            if (transform.position.z < forcePos.z)
                _z = 1;
            else
                _z = -1;
            if (transform.position.x < forcePos.x)
                _x = 1;
            else
                _x = 0;
            ChekToMove(_x, _z);
        }
        else
        {
            type = types.NORMAL;
            hero.AnimateSpecificAction("idle");
        }
    }
    public void ChangeType(types type)
    {
        if (hero.state == Character.states.DEAD)
            return;
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
        if (hero.state == Hero.states.DEAD)
            return;
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
