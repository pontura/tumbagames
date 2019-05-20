using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextsManager : MonoBehaviour
{
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
      
    }
}
