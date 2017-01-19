using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ClothItemsManager : MonoBehaviour {

    public string ClothesWearing = "";
    public List<string> items;

    void Start()
    {
        Events.OnClothes += OnClothes;
        Events.OnResetClothes += OnResetClothes;
        Events.OnHeroDie += OnHeroDie;
       // Add();
    }
    void OnHeroDie()
    {
        OnResetClothes();
    }
    void Add()
    {
       // foreach (string name in System.IO.Directory.GetFiles(@"Assets\Resources\ropa_thumbs", "*.png"))
        //    items.Add(name);
    }
    void OnClothes(string name)
    {
        ClothesWearing = name;
    }
    void OnResetClothes()
    {
        ClothesWearing = "";
    }
}
