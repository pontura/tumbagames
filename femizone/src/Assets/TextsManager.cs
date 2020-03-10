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
        public TextsContent[] intro1;
        public TextsContent[] intro2;
        public TextsContent[] intro3;
        public TextsContent[] rugbiers;
        public TextsContent[] feto;
        public TextsContent[] trump;
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
