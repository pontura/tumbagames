using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{

    public int HorizontalDirection;
    public int VerticalDirection;
    Hero hero;

    public List<int> combo;
    public bool checkingCombos;
    public bool justTurnedHorizontal;
    public bool justForwardHorizontal;
    int newHorizontalDirection;
    public int direction;

    void Start()
    {
        hero = GetComponent<Hero>();
    }
    public float timeSinceStopped = 0;
    public float _x;

    float timeOut = 0.15f;

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
            // hero.Defense();
            hero.Jump();
        }
        else if (Input.GetButtonUp("Defense" + hero.id))
        {
           // hero.Idle();
        }
        else if (Input.GetButtonDown("Hit" + hero.id))
        {
            if (CheckForSpecialAttack(1))
            {
                OnAttack(CharacterHitsManager.types.SPECIAL);
            }
            else if (hero.weaponPickable != null)
                hero.OnPick();
            else if (hero.weapons.HasWeapon())
            {
                hero.weapons.Use();
            }
            else if (justTurnedHorizontal)
                OnAttack(CharacterHitsManager.types.HIT_BACK);
            else if (justForwardHorizontal)
                OnAttack(CharacterHitsManager.types.HIT_UPPER);
            else
                OnAttack(CharacterHitsManager.types.HIT_FORWARD);
            newHorizontalDirection = 0;
        }
        else
     if (Input.GetButtonDown("Kick" + hero.id))
        {
            if (justTurnedHorizontal)
                OnAttack(CharacterHitsManager.types.KICK_BACK);
            else if (justForwardHorizontal)
                OnAttack(CharacterHitsManager.types.KICK_FOWARD);
            else
                OnAttack(CharacterHitsManager.types.KICK_DOWN);
            newHorizontalDirection = 0;
        }

       _x = (int)Input.GetAxis("Horizontal" + hero.id);

        if (_x < 0 )
        {
            if (newHorizontalDirection != -1)
            {
                checkingCombos = true;
                if (direction > 0)
                    justTurnedHorizontal = true;
                else
                    justForwardHorizontal = true;
                newHorizontalDirection = -1;
            }            
        }
        else if (_x > 0)
        {
            if (newHorizontalDirection != 1)
            {
                checkingCombos = true;
                if (direction < 0)
                    justTurnedHorizontal = true;
                else
                    justForwardHorizontal = true;
                newHorizontalDirection = 1;
            }
           
        }
        else if(_x == 0)
        {
            if (newHorizontalDirection != 0)
            {
                if (justTurnedHorizontal)
                {
                    OnAttack(CharacterHitsManager.types.HEAD);
                }
                else if (justForwardHorizontal)
                {
                    //un palancaso para adelante.
                }
            }
            HorizontalDirection = newHorizontalDirection;
            newHorizontalDirection = 0;
        }
        if (checkingCombos)
        {
            if (timeSinceStopped < timeOut)
            {
                timeSinceStopped += Time.deltaTime;
            }
            else
            {
                checkingCombos = false;
                timeSinceStopped = 0;
                ResetJustTurned();
            }
        }
    }
    void UpdateOrientation()
    {
        if (newHorizontalDirection != 0)
        {
            HorizontalDirection = newHorizontalDirection;
            hero.transform.localScale = new Vector3(newHorizontalDirection, 1, 1);
        }
    }
    void ResetJustTurned()
    {
        UpdateOrientation();
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
    void OnAttack(CharacterHitsManager.types type)
    {
        hero.hitsManager.SetOn(type);
		hero.move.ResetMove();
    }

}
