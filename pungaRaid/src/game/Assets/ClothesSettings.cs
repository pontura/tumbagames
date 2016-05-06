using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class ClothesSettings : MonoBehaviour {

    public Parts[] parts;

    public List<string> skins;

    [Serializable]
    public class Parts
    {
        public List<ClothesPart> Bottom;
        public List<ClothesPart> Top;
        public List<FacesPart> Faces;
        public List<HairsPart> Hairs;
    }
    [Serializable]
    public class ClothesPart
    {
        public string sex;
        public string style;
        public string type;
    }

    [Serializable]
    public class FacesPart
    {
        public string type;
        public string sex;
        public string expresion;
    }

    [Serializable]
    public class HairsPart
    {
        public string type;
        public string sex;
    }
	void Start () {

        return;

        LoadSkins(@"Assets\Resources\Victims\A\Body");
        List<string> clothes = LoadArray(@"Assets\Resources\Victims\A\Clothes");
        List<string> faces = LoadArray(@"Assets\Resources\Victims\A\Face");
        List<string> hairs = LoadArray(@"Assets\Resources\Victims\A\Hair");

        GetClothes(clothes, parts[0].Top, "top");
        GetClothes(clothes, parts[0].Bottom, "bottom");
        GetFaces(faces, parts[0].Faces);
        GetHairs(hairs, parts[0].Hairs);
	}
    private void GetClothes(List<string> arr, List<ClothesPart> Arr, string partName)
    {
        string lastType = ""; 
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);
            if (partName == textSplit[0])
            {
                ClothesPart part = new ClothesPart();
                part.sex = textSplit[1];
                part.style = textSplit[2];
                part.type = textSplit[3];

                if (lastType != part.style + "_" + part.type)
                    Arr.Add(part);

                lastType = part.style + "_" + part.type;
            }
        }
    }
    private void GetFaces(List<string> arr, List<FacesPart> Arr)
    {
        string lastType = ""; 
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);

            FacesPart part = new FacesPart();
            part.type = textSplit[2];
            part.sex = textSplit[1];
            part.expresion = textSplit[3];

            if (lastType != part.type)
                Arr.Add(part);
            lastType = part.type;
        }
    }
    private void GetHairs(List<string> arr, List<HairsPart> Arr)
    {
        string lastType = ""; 
        foreach (string path in arr)
        {
            String[] textSplit = path.Split("_"[0]);

            HairsPart part = new HairsPart();
            part.type = textSplit[3];
            part.sex = textSplit[1];

            
            if(lastType != part.type)
                Arr.Add(part);
            lastType = part.type;
        }
    }
    private List<string> LoadArray(string path)
    {
        List<string> newArr = new List<string>();

        foreach (string name in System.IO.Directory.GetFiles(path, "*.png"))
        {
            String[] textSplit = name.Split(@"\"[0]);
            String[] textSplit2 = textSplit[5].Split("."[0]);
            newArr.Add(textSplit2[0]);
        }
        return newArr;
    }
    
    public string GetTop(int type, string style, string sex)
    {
        List<ClothesPart> newList = new List<ClothesPart>();
        foreach (ClothesPart part in parts[type].Top)
        {
            if (part.style == style && part.sex == sex)
                newList.Add(part);
        }
        return newList[UnityEngine.Random.Range(0, newList.Count)].type;
    }
    public string GetBottom(int type, string style, string sex)
    {
     //   print(type + " " + style  +" " + sex + "___________");
        List<ClothesPart> newList = new List<ClothesPart>();
        foreach (ClothesPart part in parts[type].Bottom)
        {
            if (part.style == style && part.sex == sex)
            {
              //  print(part.style + " " + part.sex+ " " + part.type);
                newList.Add(part);
            }
        }
        if (newList.Count == 0)
        {
          //  print("ERROR");
            return "";
        } else 
        return newList[UnityEngine.Random.Range(0, newList.Count)].type;
    }
    public string GetFace(int type, string sex)
    {
        List<FacesPart> newList = new List<FacesPart>();
        foreach (FacesPart part in parts[type].Faces)
        {
            if (part.sex == sex)
                newList.Add(part);
        }
        return newList[UnityEngine.Random.Range(0, newList.Count)].type;
    }
    public string GetHair(int type, string sex)
    {
        List<HairsPart> newList = new List<HairsPart>();
        foreach (HairsPart part in parts[type].Hairs)
        {
            if (part.sex == sex)
                newList.Add(part);
        }
        return newList[UnityEngine.Random.Range(0, newList.Count)].type;
    }
    





    private List<string> LoadSkins(string path)
    {
        List<string> newArr = new List<string>();

        foreach (string name in System.IO.Directory.GetFiles(path, "*.png"))
        {
            String[] textSplit = name.Split(@"\"[0]);
            String[] textSplit2 = textSplit[5].Split("."[0]);
            string skin = textSplit2[0].Split("_"[0])[1];
            bool repeated = false;
            foreach (string thisSkin in skins)
            {
                if (thisSkin == skin)
                    repeated = true;
            }
            if(!repeated)
                skins.Add(skin);
        }
        return newArr;
    }
    public string GetRandomSkin()
    {
        return skins[UnityEngine.Random.Range(0, skins.Count)];
    }
}
