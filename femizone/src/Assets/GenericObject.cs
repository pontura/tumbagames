using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObject : SceneObject
{

    void OnTriggerEnter(Collider other)
    {
        IsOverObject(true, other);
    }
    void OnTriggerExit(Collider other)
    {
        IsOverObject(false, other);
    }
    public virtual void IsOverObject(bool isOver, Collider other)
    {
        if (data.type != SceneObjectData.types.FIRE)
            return;

        Character character = other.GetComponent<Character>();
        if (character == null)
            return;

        character.OnFire(isOver);
    }
}
