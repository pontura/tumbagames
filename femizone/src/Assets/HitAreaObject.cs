using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAreaObject : MonoBehaviour
{
   // [SerializeField] private SceneObject sceneObject;
    public List<GameObject> parts;
    int id = 0;

    void OnEnable()
    {
        id = 0;
        SetState();
    }
    private void Reset()
    {
        foreach (GameObject go in parts)
            go.SetActive(false);
    }
    void SetState()
    {
        Reset();
        parts[id].SetActive(true);
    }
    public void OnTriggerEnter(Collider col)
    {

        HitArea otherHitArea = col.gameObject.GetComponent<HitArea>();

        if (otherHitArea == null)
            return;
       
        id++;
        if (id >= parts.Count)
            return;

        SetState();

        //Hero hero = character.GetComponent<Hero>();
        //Enemy otherEnemy = otherHitArea.character.GetComponent<Enemy>();
    }
}
