using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectCar : GenericObject
{
    bool isOn;
    float speed = 10;

    Vector3 from;
    Vector3 to;

    public override void IsOverObject(bool isOver, Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character == null)
            return;

        speed += sceneObjectData.aditionalSpeed;

        HitArea hitArea = new HitArea();
        hitArea.type = CharacterHitsManager.types.SPECIAL;
        hitArea.force = 100;
        character.OnReceiveHit(hitArea, 100);
    }
    void OnEnable()
    {
        from = new Vector3(transform.localPosition.x, transform.localPosition.y, 30);
        to = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
        isOn = true;
    }
    void OnDisable()
    {
        isOn = false;
    }
    void Update()
    {
        if (!isOn)
            return;
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
        transform.localPosition = from;
    }
}