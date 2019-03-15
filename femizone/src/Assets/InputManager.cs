using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{

    public int HorizontalDirection;
    public int VerticalDirection;
    Hero hero;

    public List<int> combo;

    bool justTurnedHorizontal;
    bool justForwardHorizontal;
    int newHorizontalDirection;
    int direction;

    void Start()
    {
        hero = GetComponent<Hero>();
    }

    float timeOut = 0.1f;
    void Update()
    {
        if (hero.state == Character.states.DEAD)
            return;

        direction = (int)hero.transform.localScale.x;

        hero.OnUpdateByInput(HorizontalDirection, VerticalDirection);


        if (Input.GetAxis("Vertical" + hero.id) == -1)
            VerticalDirection = -1;
        else if (Input.GetAxis("Vertical" + hero.id) == 1)
            VerticalDirection = 1;
        else
            VerticalDirection = 0;

        if (Input.GetButtonDown("Defense" + hero.id))
        {
            hero.Defense();
        }
        else if (Input.GetButtonUp("Defense" + hero.id))
        {
            hero.Idle();
        }
        else if (Input.GetButtonDown("Hit" + hero.id))
        {
            if (CheckForSpecialAttack(1))
            {
                hero.hitsManager.SetOn(CharacterHitsManager.types.SPECIAL);
            }
            else if (hero.weaponPickable != null)
                hero.OnPick();
            else if (hero.weapons.HasWeapon())
            {
                hero.weapons.Use();
            }
            else if (justTurnedHorizontal)
                hero.hitsManager.SetOn(CharacterHitsManager.types.HIT_BACK);
            else if (justForwardHorizontal)
                hero.hitsManager.SetOn(CharacterHitsManager.types.HIT_UPPER);
            else
                hero.hitsManager.SetOn(CharacterHitsManager.types.HIT_FORWARD);
            newHorizontalDirection = 0;
        }
        else
     if (Input.GetButtonDown("Kick" + hero.id))
        {
            if (CheckForSpecialAttack(2))
            {
                hero.hitsManager.SetOn(CharacterHitsManager.types.SPECIAL);
            }
            else if (justTurnedHorizontal)
                hero.hitsManager.SetOn(CharacterHitsManager.types.KICK_BACK);
            else if (justForwardHorizontal)
                hero.hitsManager.SetOn(CharacterHitsManager.types.KICK_FOWARD);
            else
                hero.hitsManager.SetOn(CharacterHitsManager.types.KICK_DOWN);
            newHorizontalDirection = 0;
        }

        if (justTurnedHorizontal || justForwardHorizontal)
            return;

        int _x = (int)Input.GetAxis("Horizontal" + hero.id);

        if (_x == -1 && newHorizontalDirection != -1)
        {
            if (direction > 0 && HorizontalDirection == 0)
                justTurnedHorizontal = true;
            else
                justForwardHorizontal = true;

            Invoke("ResetJustTurned", timeOut);
            newHorizontalDirection = -1;
        }
        else if (_x == 1 && newHorizontalDirection != 1)
        {
            if (direction < 0 && HorizontalDirection == 0)
                justTurnedHorizontal = true;
            else
                justForwardHorizontal = true;

            newHorizontalDirection = 1;
            Invoke("ResetJustTurned", timeOut);
        }
        else
        {
            newHorizontalDirection = 0;
            HorizontalDirection = 0;
        }

    }
    void ResetJustTurned()
    {     
        if (newHorizontalDirection != 0)
        {
            HorizontalDirection = newHorizontalDirection;
            hero.transform.localScale = new Vector3(newHorizontalDirection, 1, 1);
        }
        newHorizontalDirection = 0;
        justTurnedHorizontal = false;
        justForwardHorizontal = false;
    }
    public List<int> buttonsID;
    bool CheckForSpecialAttack(int buttonID)
    {
        buttonsID.Add(buttonID);
        Invoke("ResetAtatcks", 0.2f);
        if (buttonsID.Count == 2)
        {
            if (buttonsID[0] == 1 && buttonsID[1] == 2)
                return true;
        }
        return false;
    }
    void ResetAtatcks()
    {
        buttonsID.Clear();
    }

}
