using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;


public class Main : MonoBehaviour {

    public ScoreLine scoreLine;
    public Transform container;

    public Text titleField;
    public Text puestoField;
    public Text field;

    public Data data;
    public LeterChanger[] letters;
    public int lettersID;
    public LeterChanger letterActive;
    public int puesto;

    string fileName_data = "C:\\tumbagames\\hiscores\\data.txt";
    string fileName_ss = "C:\\tumbagames\\hiscores\\SS.txt";
    string fileName_iid = "C:\\tumbagames\\hiscores\\IID.txt";
    string fileName_bb = "C:\\tumbagames\\hiscores\\BB.txt";
    string fileName_sd = "C:\\tumbagames\\hiscores\\SD.txt";
    string fileName_pr = "C:\\tumbagames\\hiscores\\PR.txt";

    public GameObject poster_ss;
    public GameObject poster_iid;
    public GameObject poster_bb;
    public GameObject poster_sd;
    public GameObject poster_pr;

    public GameObject newTop15Hiscore;

    [Serializable]
    public class Data
    {
        public string prefix;
        public string title;
        public int hiscore;
        public string url;
    }

    public List<Hiscore> arrengedHiscores;
    public List<Hiscore> hiscores;
    [Serializable]
    public class Hiscore
    {
        public string username;
        public int hiscore;       
    }


	void Start () {
        Cursor.visible = false;
        newTop15Hiscore.SetActive(false);
        poster_ss.SetActive(false);
        poster_iid.SetActive(false);
        poster_bb.SetActive(false);
        poster_sd.SetActive(false);
        poster_pr.SetActive(false);

        puesto = 1;
        Screen.fullScreen = true;
        var sr = new StreamReader(fileName_data);
        var fileContents = sr.ReadToEnd();
        sr.Close(); 
        string[] lines = fileContents.Split("_"[0]);

        data.hiscore = int.Parse(lines[1]);
        data.prefix = lines[0];
        switch ( data.prefix )
        {
            case "SS": data.title = "SUICIDE SNIPER"; data.url = fileName_ss;               poster_ss.SetActive(true); break;
            case "IID": data.title = "INSANE IMBECILE DANCING"; data.url = fileName_iid;    poster_iid.SetActive(true); break;
            case "BB": data.title = "BRUTAL BATTLE"; data.url = fileName_bb;                poster_bb.SetActive(true); break;
            case "SD": data.title = "SUBWAY DOMINATOR"; data.url = fileName_sd;             poster_sd.SetActive(true); break;
            case "PR": data.title = "PUNGA RAID"; data.url = fileName_pr;                   poster_pr.SetActive(true); break;
        }        

        //gameField.text = data.title;
        field.text = data.hiscore.ToString();

        //////////////va a la primer letra:
        lettersID = -1;  SetLetterActive(true);

        LoadHiscores(data.url);
        puestoField.text = "PUESTO " + puesto;
        if (puesto < 16)
        {
            NewTop15HiscoreOn();
        }
       // SaveNew(data.url, "CACA", 1233);PUESTO
	}
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            letterActive.ChangeLetter(false);
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            letterActive.ChangeLetter(true);
        else if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
            SetLetterActive(true);
        else if (Input.GetKeyUp(KeyCode.Alpha1))
            SetLetterActive(false);
    }
    void SetLetterActive(bool next)
    {
        foreach (LeterChanger letterChanger in letters)
        {
            letterChanger.SetActivate(false);
            letterChanger.GetComponent<Animation>().Play("letterOff");
        }
        if (next) lettersID++;
        else lettersID--;
        if (lettersID < 0)
        {
            Application.Quit();
            return;
        }
        if (lettersID > letters.Length - 1)
        {
            Save();
            return;
        }

        letterActive = letters[lettersID];

        letterActive.SetActivate(true);
        letterActive.GetComponent<Animation>().Play("letterOn");
    }
    void NewTop15HiscoreOn()
    {
        newTop15Hiscore.SetActive(true);
        Invoke("hoscoreOff", 4);
    }
    void hoscoreOff()
    {
        newTop15Hiscore.SetActive(false);
    }
    void LoadHiscores(string fileName)
    {
            String[] arrLines = File.ReadAllLines(fileName);
            int num = 1;
            foreach (string line in arrLines)
            {
                string[] lines = line.Split("_"[0]);
                Hiscore hiscore = new Hiscore();
                hiscore.username = lines[0];
                hiscore.hiscore = int.Parse(lines[1]);
                hiscores.Add(hiscore);
                num++;
                if(num<16)
                {
                    ScoreLine newScoreLine = Instantiate(scoreLine);                
                    newScoreLine.Init(num, hiscore.username, hiscore.hiscore);               
                    newScoreLine.transform.SetParent(container);
                    newScoreLine.transform.localScale = Vector3.one;
                }

                if (hiscore.hiscore > data.hiscore)
                    puesto = num;
            } 
            // 
     }
    void Save()
    {
        string username = "";
        foreach (LeterChanger letterChanger in letters)
        {
            username += letterChanger.field.text;
        }
        SaveNew(data.url, username, data.hiscore);
    }
    public void SaveNew(string fileName, string username, int newHiscoreToSave)
    {
        Hiscore newHiscore = new Hiscore();
        newHiscore.username = username;
        newHiscore.hiscore = newHiscoreToSave;
        hiscores.Add(newHiscore);

        arrengedHiscores = OrderByHiscore(hiscores);

        String[] arrLines = new String[hiscores.Count];
        int a = 0;
        foreach (Hiscore hs in arrengedHiscores)
        {
            print(hs);
            arrLines[a] = hs.username + "_" + hs.hiscore;
            a++;
        }
        File.WriteAllLines(fileName, arrLines);
        Application.Quit();
    }
    List<Hiscore> OrderByHiscore(List<Hiscore> hs)
    {
        return hs.OrderBy(go => go.hiscore).Reverse().ToList();
    }

}
