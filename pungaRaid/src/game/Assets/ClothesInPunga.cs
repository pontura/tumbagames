using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClothesInPunga : MonoBehaviour {

    public GameObject[] bodyParts;
    public List<SpriteRenderer> parts;

    void Start()
    {
        foreach (GameObject go in bodyParts)
        {
            foreach (SpriteRenderer sr in go.GetComponentsInChildren<SpriteRenderer>())
                parts.Add(sr);
        }
		string specialCloth = Data.Instance.clothItemsManager.ClothesWearing;
        if (specialCloth != "")
            ChangeCloth(specialCloth);
    }
    public void ChangeCloth(string name)
    {
        foreach(SpriteRenderer sr in parts)
        {
            Sprite sp = Resources.Load("ropa/" + name + "/" + sr.name, typeof(Sprite)) as Sprite;
            if(sp)
                sr.sprite = sp;
        }
    }
}
