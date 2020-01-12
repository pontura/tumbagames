using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectCar : GenericObject
{
    bool isOn;
    bool inDistance;
    float speed = 10;

    Vector3 from;
    Vector3 to;

    public override void IsOverObject(bool isOver, Collider other)
    {       
        Character character = other.GetComponent<Character>();
        if (character == null)
            return;

        HitArea hitArea = new HitArea();
        hitArea.type = CharacterHitsManager.types.SPECIAL;
        hitArea.force = 100;
        character.OnReceiveHit(hitArea, 100);
        if(!character.GetComponent<Hero>())
            Events.OnCarCrashEnemy();
    }
    public override void OnInit()
    {
        speed += data.aditionalSpeed;
    }
    void OnEnable()
    {
        from = new Vector3(transform.localPosition.x, transform.localPosition.y, 30);
        to = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
        isOn = true;
        inDistance = false;
    }
    void OnDisable()
    {
        isOn = false;
    }
    void IsInsideDistance()
    {
        float percentGirlsPosition = World.Instance.heroesManager.GetPercentPosition();
        if (percentGirlsPosition == 0) return;
        float distance = transform.position.x - percentGirlsPosition;
        if (distance < 10)
            inDistance = true;
    }
    void Update()
    {
        if (!isOn)
            return;
        if (!inDistance)
        {
            IsInsideDistance();
            return;
        }         

        Vector3 pos = transform.localPosition;
        pos.z -= speed * Time.deltaTime;

        if (pos.z < to.z)
        {
            pos.z = from.z;
            gameObject.SetActive(false);
            Invoke("Restart", 1);
        }

        transform.localPosition = pos;
    }
    void Restart()
    {
        gameObject.SetActive(true);
        transform.localPosition = from;
    }
}