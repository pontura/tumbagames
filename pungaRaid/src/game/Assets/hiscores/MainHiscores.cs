using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainHiscores : MonoBehaviour {

    public ScoreLine scoreLineNewHiscore;
    public ScoreLine scoreLine;
    public Transform container;

    public Text titleField;
    public Text puestoField;
    public Text field;

    public LeterChanger[] letters;
    public int lettersID;
    public LeterChanger letterActive;
    public int puesto;

    string fileName_pr_1 = "C:\\tumbagames\\hiscores\\PR_1.txt";
	string fileName_pr_2 = "C:\\tumbagames\\hiscores\\PR_2.txt";
	string fileName_pr_3 = "C:\\tumbagames\\hiscores\\PR_3.txt";
	string fileName_pr_4 = "C:\\tumbagames\\hiscores\\PR_4.txt";
	string fileName_pr_5 = "C:\\tumbagames\\hiscores\\PR_5.txt";
	string fileName_pr_6 = "C:\\tumbagames\\hiscores\\PR_6.txt";

	string fileName_pr;

	int _hiscore = 0;

    public List<Hiscore> arrengedHiscores;
    public List<Hiscore> hiscores;
    [Serializable]
    public class Hiscore
    {
        public string username;
        public int hiscore;       
    }
	void Start () {
		_hiscore = Game.Instance.gameManager.score;

		if(Data.Instance.moodsManager.currentMood == MoodsManager.moods.BELGRANO && Data.Instance.areasManager.seccionalActiveID == 1)
			fileName_pr = fileName_pr_1;
		else if(Data.Instance.moodsManager.currentMood == MoodsManager.moods.BELGRANO && Data.Instance.areasManager.seccionalActiveID == 2)
			fileName_pr = fileName_pr_2;
		else if(Data.Instance.moodsManager.currentMood == MoodsManager.moods.BELGRANO && Data.Instance.areasManager.seccionalActiveID == 3)
			fileName_pr = fileName_pr_3;
		else if(Data.Instance.moodsManager.currentMood == MoodsManager.moods.MADERO && Data.Instance.areasManager.seccionalActiveID == 1)
			fileName_pr = fileName_pr_4;
		else if(Data.Instance.moodsManager.currentMood == MoodsManager.moods.MADERO && Data.Instance.areasManager.seccionalActiveID == 2)
			fileName_pr = fileName_pr_5;
		else if(Data.Instance.moodsManager.currentMood == MoodsManager.moods.MADERO && Data.Instance.areasManager.seccionalActiveID == 3)
			fileName_pr = fileName_pr_6;
		
        puesto = 1;
        Screen.fullScreen = true;
		LoadHiscores(fileName_pr);
        puestoField.text = "PUESTO " + puesto;
		field.text = _hiscore.ToString ();
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
			grabaEnd ();
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
    private bool yaAgrego;
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

                if (hiscore.hiscore < _hiscore && !yaAgrego)
                {
                    yaAgrego = true;
                    puesto = num;
                    if (num < 16)
                    {
                        ScoreLine newScoreLine = Instantiate(scoreLineNewHiscore);
						newScoreLine.Init(num, "XXXXX", _hiscore);
                        newScoreLine.transform.SetParent(container);
                        newScoreLine.transform.localScale = Vector3.one;
                        num++;
                    }                    
                }

                if(num<16)
                {
                    ScoreLine newScoreLine = Instantiate(scoreLine);                
                    newScoreLine.Init(num, hiscore.username, hiscore.hiscore);               
                    newScoreLine.transform.SetParent(container);
                    newScoreLine.transform.localScale = Vector3.one;
                }               

                num++;
            } 
     }
    void Save()
    {
        string username = "";
        foreach (LeterChanger letterChanger in letters)
        {
            string letra = letterChanger.field.text;
            if (letra == "_") letra = " ";
            username += letra;
        }
		SaveNew(fileName_pr, username, _hiscore);
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
        Invoke("grabaEnd", 0.1f);
    }
    void grabaEnd()
    {
		SceneManager.LoadScene("02_Intro");
    }
    List<Hiscore> OrderByHiscore(List<Hiscore> hs)
    {
        return hs.OrderBy(go => go.hiscore).Reverse().ToList();
    }

}
