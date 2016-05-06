using UnityEngine;
using System.Collections;
using System;

public class TutorialManager : MonoBehaviour {

    public bool ready;
    public Tutorial tutorial;
    public float distance;
    public int tutorialID;
    public tutorialData[] all;

    [Serializable]
    public class tutorialData
    {
        public float distance;
        public SwipeDetector.directions direction;
    }

	void Start () {
        tutorial.gameObject.SetActive(false);
        if (PlayerPrefs.GetString("tutorialReady") == "true")
        {
            ready = true;
            return;
        }

        Events.StartGame += StartGame;        
	}
    void OnDestroy()
    {
        Events.StartGame -= StartGame;    
    }
    void StartGame()
    {
        tutorialID = 0;
        
        tutorial.Init();
        Game.Instance.gameManager.state = GameManager.states.TUTORIAL;
        AddEnemy("Victim", 3, 30);
        AddEnemy("Victim", 3, 35);
        AddEnemy("Victim", 2, 42);
        AddEnemy("RatiEscudo", 3, 47);
        AddEnemy("Victim", 2, 58);
        AddEnemy("Victim", 3, 58);
    }
    void AddEnemy(string objectName, int lane, int distance)
    {
        Lanes lanes = Game.Instance.GetComponent<LevelsManager>().lanes;
        EnemySettings settings = new EnemySettings();
        settings.speed = 0.03f;
        lanes.AddObjectToLane(objectName, lane, distance, settings);
    }
    void Update()
    {
        if (ready) return;

        if (tutorialID >= all.Length)
        {
            PlayerPrefs.SetString("tutorialReady", "true");
            Game.Instance.gameManager.state = GameManager.states.ACTIVE;
            ready = true;
            return;
        }

        distance = Game.Instance.gameManager.distance;

        if (distance > all[tutorialID].distance)
        {
            tutorial.gameObject.SetActive(true);
            string title = Data.Instance.texts.tutorial[tutorialID];
            tutorial.InitSimpleButton(title, all[tutorialID].direction);
            tutorialID++;
        }
    }
}
