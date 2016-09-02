using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class AchievementsManager : MonoBehaviour
{
    public List<Achievement> achievements;
    public int totalAchievements;
    string jsonUrl = "achievements";

    const string PREFAB_PATH = "AchievementsManager";
    static AchievementsManager mInstance = null;

    public static AchievementsManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<AchievementsManager>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<AchievementsManager>();
                    go.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            return mInstance;
        }
    }
    public int GetTotalReady()
    {
        int total = 0;
        foreach ( Achievement ach in achievements)
        {
            if(ach.ready) total++;
        }
        return total;
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    void OnDestroy()
    {
        SocialEvents.ResetApp -= ResetApp;
    }
    void Start()
    {
        TextAsset file = Resources.Load(jsonUrl) as TextAsset;
        LoadDataromServer(file.text);
        SocialEvents.ResetApp += ResetApp;
    }
    void ResetApp()
    {
        foreach ( Achievement ach in achievements)
        {
            ach.ready = false;
        }
    }
    public void LoadDataromServer(string json_data)
    {
        var Json = SimpleJSON.JSON.Parse(json_data);
        string arrayName = "achievements";
        achievements = new List<Achievement>(Json[arrayName].Count);
        for (int a = 0; a < Json[arrayName].Count; a++)
        {
            totalAchievements++;
            string type = Json[arrayName][a]["type"];

            switch (type)
            {
                case "DISTANCE":
                    AchievementDistance achievement_distance = new AchievementDistance();
                    achievement_distance.title = Json[arrayName][a]["title"];
                    achievement_distance.id = a;
                    achievement_distance.image = Json[arrayName][a]["image"];
                    achievement_distance.pointsToBeReady = int.Parse(Json[arrayName][a]["pointsToBeReady"]);
                    achievement_distance.Init();
                    achievements.Add(achievement_distance);
                    break;
                case "POWERUP":
                    AchievementPowerup achievement_powerup = new AchievementPowerup();
                    achievement_powerup.title = Json[arrayName][a]["title"];
                    achievement_powerup.id = a;
                    achievement_powerup.image = Json[arrayName][a]["image"];
                    achievement_powerup.data = Json[arrayName][a]["data"];
                    achievement_powerup.pointsToBeReady = int.Parse(Json[arrayName][a]["pointsToBeReady"]);
                    achievement_powerup.Init();
                    achievements.Add(achievement_powerup);
                    break;
                case "AREA":
                    AchievementArea achievement_area = new AchievementArea();
                    achievement_area.title = Json[arrayName][a]["title"];
                    achievement_area.id = a;
                    achievement_area.image = Json[arrayName][a]["image"];
                    achievement_area.pointsToBeReady = int.Parse(Json[arrayName][a]["pointsToBeReady"]);
                    achievement_area.Init();
                    achievements.Add(achievement_area);
                    break;
            }
        }
    }
    public Achievement GetAchievement(int id)
    {
        return achievements[id];
    }
    
    

}
