using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class Clothes : MonoBehaviour
{
    public string[] sex;
    public string[] styles;
    public List<SpriteRenderer> stolen;
    
    public SpriteRenderer headSkin;
    public SpriteRenderer bodySkin;
    public SpriteRenderer hipsSkin;

    public SpriteRenderer arm1Skin1;
    public SpriteRenderer arm1Skin2;
    public SpriteRenderer arm2Skin1;
    public SpriteRenderer arm2Skin2;

    public SpriteRenderer leg1Skin1;
    public SpriteRenderer leg1Skin2;
    public SpriteRenderer leg2Skin1;
    public SpriteRenderer leg2Skin2;
    

    public SpriteRenderer faceA;
    public SpriteRenderer faceB;

    public SpriteRenderer hairA;
    public SpriteRenderer hairB;

    public SpriteRenderer bodyTop;

    public SpriteRenderer arm1TopA;
    public SpriteRenderer arm1TopB;
    public SpriteRenderer arm2TopA;
    public SpriteRenderer arm2TopB;

    public SpriteRenderer hips;

    public SpriteRenderer leg1BottomA;
    public SpriteRenderer leg1BottomB;
    public SpriteRenderer leg2BottomA;
    public SpriteRenderer leg2BottomB;

    public SpriteRenderer objectsBag;
    public SpriteRenderer objectsPhones;

    private ClothesSettings clothSettings;

    void Start()
    {
        clothSettings = Data.Instance.clothesSettings;
        string randomSex = sex[UnityEngine.Random.Range(0, sex.Length)];
        string randomstyle = styles[UnityEngine.Random.Range(0, styles.Length)];
        Dress("A", randomSex, randomstyle);
    }
    public void Restart()
    {
        foreach (SpriteRenderer sr in stolen)
            sr.enabled = true;
    }
    public int Undress()
    {
        int mnultiplayerStolen = 0;
        int rand = UnityEngine.Random.Range(0, 100);

        //FORZAR  arobar todo:
        if (Game.Instance.gameManager.characterManager.character.powerupManager.type == PowerupManager.types.GIL) 
            rand = 1;

        stolen.Clear();
        if (rand < 10)
        {
            SteatUp();
            SteatDown();
            mnultiplayerStolen = 2;
        }
        if (rand < 55) 
        {
            SteatUp();
            mnultiplayerStolen = 1;
        }
        else
        {
            SteatDown();
            mnultiplayerStolen = 1;
        }
        
        foreach (SpriteRenderer sr in stolen)
            sr.enabled = false;

        return mnultiplayerStolen;
    }
    void SteatUp()
    {
        stolen.Add(bodyTop);
        stolen.Add(arm1TopA);
        stolen.Add(arm1TopB);
        stolen.Add(arm2TopA);
        stolen.Add(arm2TopB);
    }
    void SteatDown()
    {
        stolen.Add(hips);
        stolen.Add(leg1BottomA);
        stolen.Add(leg2BottomA);
        stolen.Add(leg1BottomB);
        stolen.Add(leg2BottomB);
    }
    void Dress(string type, string sex, string randomstyle)
    {
        //skins

        string skin = clothSettings.GetRandomSkin();
        bodySkin.sprite = Resources.Load("Victims/" + type + "/Body/body" + sex + "_" + skin, typeof(Sprite)) as Sprite;

        arm1Skin1.sprite = Resources.Load("Victims/" + type + "/Body/arm1" + "_" + skin, typeof(Sprite)) as Sprite;
        arm1Skin2.sprite = Resources.Load("Victims/" + type + "/Body/arm1" + "_" + skin, typeof(Sprite)) as Sprite;
        arm2Skin1.sprite = Resources.Load("Victims/" + type + "/Body/arm2" + "_" + skin, typeof(Sprite)) as Sprite;
        arm2Skin2.sprite = Resources.Load("Victims/" + type + "/Body/arm2" + "_" + skin, typeof(Sprite)) as Sprite;

        headSkin.sprite = Resources.Load("Victims/" + type + "/Body/head" + "_" + skin, typeof(Sprite)) as Sprite;

        hipsSkin.sprite = Resources.Load("Victims/" + type + "/Body/hips" + sex + "_" + skin, typeof(Sprite)) as Sprite;

        leg1Skin1.sprite = Resources.Load("Victims/" + type + "/Body/legA1" + "_" + skin, typeof(Sprite)) as Sprite;
        leg1Skin2.sprite = Resources.Load("Victims/" + type + "/Body/legA2" + "_" + skin, typeof(Sprite)) as Sprite;

        leg2Skin1.sprite = Resources.Load("Victims/" + type + "/Body/legB1" + "_" + skin, typeof(Sprite)) as Sprite;
        leg2Skin2.sprite = Resources.Load("Victims/" + type + "/Body/legB2" + "_" + skin, typeof(Sprite)) as Sprite;

        //
       string CanBeBisexual = sex;
       if (UnityEngine.Random.Range(0, 100)<30)
           CanBeBisexual = "B";

       string top = clothSettings.GetTop(0, randomstyle, sex);
       bodyTop.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/top_" + sex + "_" + randomstyle + "_" + top + "_A");
       arm1TopA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/top_" + sex + "_" + randomstyle + "_" + top + "_B");
       arm1TopB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/top_" + sex + "_" + randomstyle + "_" + top + "_C");
       arm2TopA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/top_" + sex + "_" + randomstyle + "_" + top + "_B");
       arm2TopB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/top_" + sex + "_" + randomstyle + "_" + top + "_C");

       string bottom = clothSettings.GetBottom(0, randomstyle, sex);
       if (bottom == "")
       {
           CanBeBisexual = "B";
           bottom = clothSettings.GetBottom(0, randomstyle, CanBeBisexual);
       }
       else CanBeBisexual = sex;
 
       hips.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/bottom_" + CanBeBisexual + "_" + randomstyle + "_" + bottom + "_A");
       leg1BottomA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/bottom_" + CanBeBisexual + "_" + randomstyle + "_" + bottom + "_B");
       leg1BottomB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/bottom_" + CanBeBisexual + "_" + randomstyle + "_" + bottom + "_C");
       leg2BottomA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/bottom_" + CanBeBisexual + "_" + randomstyle + "_" + bottom + "_B");
       leg2BottomB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Clothes/bottom_" + CanBeBisexual + "_" + randomstyle + "_" + bottom + "_C");

       string face = clothSettings.GetFace(0, sex);
       faceA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Face/face_" + sex + "_" + face + "_a");
       faceB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Face/face_" + sex + "_" + face + "_b");

        string hair = clothSettings.GetHair(0, sex);
        hairA.sprite = Resources.Load<Sprite>("Victims/" + type + "/Hair/hair_" + CanBeBisexual + "_1_" + hair + "_A");
        hairB.sprite = Resources.Load<Sprite>("Victims/" + type + "/Hair/hair_" + CanBeBisexual + "_1_" + hair + "_B");
    }
}
