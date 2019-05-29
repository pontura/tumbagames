using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextsManager : MonoBehaviour
{
    public int introID = 0;
    public Intro intro;

    [Serializable]
    public class Intro
    {
        public TextsContent[] all;
    }
    [Serializable]
    public class TextsContent
    {
        public string[] texts;
    }

    void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("intro");
        intro = JsonUtility.FromJson<Intro>(file.text);
        introID = UnityEngine.Random.Range(0, intro.all.Length);
    }
    public TextsContent GetNextIntro()
    {
        introID++;
        if (introID >= intro.all.Length)
            introID = 0;
        return intro.all[introID];        
    }
}
