using UnityEngine;
using System.Collections;

public class CharacterCollider : MonoBehaviour {

    public types type;
    public Character character;
    private BoxCollider2D collider2d;
    private Blocker lastBlocker;
    private bool alarmOn;

    public enum types
    {
        CENTER,
        TOP,
        BOTTOM,
        GUN,
        GIL_POWERUP,
        ALARM
    }
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        OnChangeLaneComplete();

        Events.OnChangeLaneComplete += OnChangeLaneComplete;
        Events.OnChangeLane += OnChangeLane;
        Events.OnPowerUpShoot += OnPowerUpShoot;
        Events.OnHeroPowerUpOff += OnHeroPowerUpOff;
        Events.OnVulnerability += OnVulnerability;
        
    }
    void OnDestroy()
    {
        Events.OnChangeLaneComplete -= OnChangeLaneComplete;
        Events.OnChangeLane -= OnChangeLane;
        Events.OnPowerUpShoot -= OnPowerUpShoot;
        Events.OnHeroPowerUpOff -= OnHeroPowerUpOff;
        Events.OnVulnerability -= OnVulnerability;
    }
    void OnVulnerability(bool isOn)
    {
        if (type == types.CENTER)
            collider2d.enabled = !isOn;
    }
    void OnChangeLaneComplete()
    {
        if (character && character.hero && character.hero.state == Hero.states.JUMP) return;
        if (type != types.GUN)
            collider2d.enabled = true;
    }
    void OnChangeLane()
    {
        if (type != types.GUN)
            collider2d.enabled = false;
    }
    void OnPowerUpShoot(PowerupManager.types newType)
    {
        if (type == types.GUN && newType == PowerupManager.types.CHUMBO)
        {
            collider2d.enabled = true;
            Invoke("PowerUpReady", 0.2f);
        }
    }
    void PowerUpReady()
    {
        collider2d.enabled = false;
    }
    void OnHeroPowerUpOff()
    {
       // collider2d.size = new Vector2(4, collider2d.size.y);
        if (type == types.GUN)
            collider2d.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(!enemy) return;
        Blocker blocker = enemy.GetComponent<Blocker>();
        Victim victim = other.GetComponent<Victim>();

        if (type == types.ALARM)
        {
            if (character.hero.state == Hero.states.JUMP) return;
            if (character.powerupManager.type == PowerupManager.types.MOTO) return;
            if (enemy.laneId != Game.Instance.gameManager.characterManager.lanes.laneActiveID) return;
            if (other.GetComponent<Coins>()) return;
            if (other.GetComponent<PowerUp>()) return;
            Events.OnOooops(true);
            alarmOn = true;
            return;
        } else
        if (type == types.GIL_POWERUP)
        {
            if (victim && !victim.loopStealing)
                victim.StealLoop_Gil();
        } else
        if (type == types.GUN && victim != null)
        {
            enemy.Explote();
            return;
        }
        else
        if (type == types.CENTER)
        {
            if (blocker)
            {
                if (character.hero.state != Hero.states.DEAD)
                {
                    if (character.hero.state == Hero.states.JUMP) return;
                    if (character.powerupManager.type == PowerupManager.types.MOTO)
                    {
                        character.CantMoveUp = false;
                        character.CantMoveDown = false;
                        Events.OnHeroPowerUpOff();
                        return;
                    }
                    else
                    {
                        Events.OnHeroDie();
                    }
                }
                return;
            }
            character.OnCollisionCenter(enemy);
        }
        else
        {
            if (!blocker) return;
            lastBlocker = blocker;
            character.OnCollisionWithBlocker(blocker, type);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (type == types.CENTER) return;
        if (type == types.GUN) return;

        if (type == types.ALARM && alarmOn)
        {
            alarmOn = false;
            Events.OnOooops(false);
        } else
        if (type == types.GIL_POWERUP)
        {
            Victim victim = other.GetComponent<Victim>();
            if (victim)
            {
                victim.StealLoopEnd_Gil();
            }
            return;
        } 
        if (lastBlocker == other.GetComponent<Blocker>())
        {
            if (type == types.TOP)
                character.CantMoveUp = false;
            else
                character.CantMoveDown = false;
        }
    }
}
