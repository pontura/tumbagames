using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesManager : MonoBehaviour
{
    public Transform container;
    public GameObject heartAsset;
    public List<int> ids;
    public List<GameObject> hearts;

    void Start()
    {
        Events.AddHero += AddHero;
        Events.OnHeroDie += OnHeroDie;
    }
    void OnDestroy()
    {
        Events.AddHero -= AddHero;
        Events.OnHeroDie -= OnHeroDie;
    }
    void OnHeroDie(int id)
    {
        RemoveHeart();
    }
    void AddHero(int id)
    {
        foreach(int addedID in ids)
        {
            if (addedID == id)
                return;
        }        
        AddHeart();
        ids.Add(id);       
    }
    void AddHeart()
    {
        GameObject go = Instantiate(heartAsset);
        go.transform.SetParent(container);
        hearts.Add(go);
    }
    void RemoveHeart()
    {
        if (hearts.Count == 0)
            return;
        GameObject go = hearts[0];
        hearts.Remove(go);
        Destroy(go);
    }
    public int GetLifes()
    {
        return hearts.Count;
    }
}
