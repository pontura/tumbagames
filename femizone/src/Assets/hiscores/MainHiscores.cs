using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainHiscores : MonoBehaviour
{
    public int _hiscore = 0;
    public GameObject panel;
    public ScoreLine scoreLineNewHiscore;
    public ScoreLine scoreLine;
    public Transform container;

    public LeterChanger[] letters;
    public int lettersID;
    public LeterChanger letterActive;
    public int puesto;



    public List<Hiscore> arrengedHiscores;
    public List<Hiscore> hiscores;

    bool done;

    public CharactersUI charactersUI;
    public int playerID;

    CharacterUI characterUI;

    [Serializable]
    public class Hiscore
    {
        public string username;
        public int hiscore;
    }
    public states state;
    public enum states
    {
        OFF,
        ADDING_NEW,
        DONE
    }
    void Start()
    {
        panel.SetActive(false);
    }
    public void GameOver()
    {
        StartCoroutine(InitRoutine());
    }
    IEnumerator InitRoutine()
    {
        Events.OnAxisChange += OnAxisChange;
        characterUI = charactersUI.GetHiscoreCharacterUI();
        this.playerID = characterUI.id;

        yield return new WaitForSeconds(2);
        Events.OnKeyPress += OnKeyPress;
        if (characterUI.score > Data.Instance.arcadeRanking.all[0].hiscore)
        {
            Init();
        }else{
            yield return new WaitForSeconds(2);
            state = states.DONE;
        }
    }
    void Done()
    {
        World.Instance.OnRestart();
        state = states.OFF;

        Events.OnKeyPress -= OnKeyPress;
        Events.OnAxisChange -= OnAxisChange;
    }
    void OnDestroy()
    {
        Events.OnKeyPress -= OnKeyPress;
        Events.OnAxisChange -= OnAxisChange;
    }
    void Init()
    {        
        LoadHiscores(Data.Instance.arcadeRanking.path);

        state = states.ADDING_NEW;
        panel.SetActive(true);

        Vector2 pos = panel.transform.localPosition;
        pos.x = characterUI.transform.localPosition.x;
        panel.transform.localPosition = pos;



        _hiscore = characterUI.score;

        done = false;

        puesto = 1;
        Screen.fullScreen = true;



        foreach (LeterChanger letterChanger in letters)
        {
            letterChanger.SetActivate(false);
            letterChanger.GetComponent<Animation>().Play("letterOff");
        }
        letters[0].SetActivate(true);
        letters[0].GetComponent<Animation>().Play("letterOn");
    }
    void OnAxisChange(int _playerID, int value)
    {
        if (playerID != _playerID)
            return;
        if (value < 1)
            letterActive.ChangeLetter(false);
        else
            letterActive.ChangeLetter(true);
    }
    void OnKeyPress(int _playerID)
    {
        if (state == states.DONE)
        {
            Done();
            return;
        }
        else if (state == states.ADDING_NEW)
        {
            if (playerID != _playerID)
                return;
            SetLetterActive(true);
        }
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
            lettersID = 0;
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

            if (num < 16)
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

        if (done)
            return;

        done = true;

        string username = "";
        foreach (LeterChanger letterChanger in letters)
        {
            string letra = letterChanger.field.text;
            if (letra == "_") letra = " ";
            username += letra;
        }
        SaveNew(Data.Instance.arcadeRanking.path, username, _hiscore);
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
            arrLines[a] = hs.username + "_" + hs.hiscore;
            a++;
        }
        File.WriteAllLines(fileName, arrLines);
        Invoke("grabaEnd", 0.25f);
    }
    void grabaEnd()
    {
        state = states.DONE;
        Events.RefreshHiscores();
    }
    List<Hiscore> OrderByHiscore(List<Hiscore> hs)
    {
        return hs.OrderBy(go => go.hiscore).Reverse().ToList();
    }

}
