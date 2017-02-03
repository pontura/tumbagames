using UnityEngine;
using System.Collections;

public class Coins : Enemy {

    public int money;
    public states state;
    public GameObject una;
    public GameObject dos;
    public GameObject cuatro;
    public GameObject pila;

    public int multiplyier;

    public enum states
    {
        IDLE
    }
    private BoxCollider2D collider;

    override public void Enemy_Activate()
    {
        collider.enabled = true;
    }
    override public void Enemy_Init(EnemySettings settings, int laneId)
    {        
        collider = GetComponent<BoxCollider2D>();
        Invoke("SetValue", 0.1f);
    } 
    void SetValue()
    {
        una.SetActive(false);
        dos.SetActive(false);
        cuatro.SetActive(false);
        pila.SetActive(false);

        int rand = Random.Range(0, 10);

        if (Game.Instance != null && Game.Instance != null && Game.Instance.characterManager.character.powerupManager.type == PowerupManager.types.RICKYFORT)
        {
            if (rand > 4)
                SetAsset(15);
            else if (rand > 1)
                SetAsset(4);
            else
                SetAsset(2);
        }
        else
        {
            int seccionalID = Data.Instance.moodsManager.currentSeccionalID;
            int moodID = Data.Instance.moodsManager.GetCurrentMoodID();
           
            if (moodID > 1)
            {
                if(rand>5)
                    SetAsset(4);
                else
                    SetAsset(2);
            }
            else if (seccionalID > 1)
            {
                if (rand > 8)
                    SetAsset(2);
            }
            else
            {
                SetAsset(1);
            }
        }
    }
    void SetAsset(int qty)
    {
        multiplyier = qty;
        switch (qty)
        {
            case 1: una.SetActive(true);  break;
            case 2: dos.SetActive(true); break;
            case 4: cuatro.SetActive(true); break;
            case 15: pila.SetActive(true); break;
        }
    }
    public void Activate()
    {
        Pool();
    }   
    
}
