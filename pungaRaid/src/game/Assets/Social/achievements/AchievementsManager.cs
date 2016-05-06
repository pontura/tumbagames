using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class AchievementsManager : MonoBehaviour
{
    public List<Achievement> achievements;
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
        //string filepath = (Application.dataPath + jsonUrl);
        //TextAsset file = Resources.Load(jsonUrl) as TextAsset;
        //LoadDataromServer(file.text);
        //SocialEvents.ResetApp += ResetApp;
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
            string type = Json[arrayName][a]["type"];

            switch (type)
            {
                case "MISSION": 
                    AchievementMission achievement_mission = new AchievementMission();
                    achievement_mission.title = Json[arrayName][a]["title"];
                    achievement_mission.id = a;
                    achievement_mission.progress = int.Parse(Json[arrayName][a]["progress"]);
                    achievement_mission.image = Json[arrayName][a]["image"];
                    achievement_mission.mission = int.Parse(Json[arrayName][a]["mission"]);
                    achievement_mission.Init();
                    achievements.Add(achievement_mission);
                    break;
                case "DISTANCE":
                    AchievementDistance achievement_distance = new AchievementDistance();
                    achievement_distance.title = Json[arrayName][a]["title"];
                    achievement_distance.id = a;
                    achievement_distance.progress = int.Parse(Json[arrayName][a]["progress"]);
                    achievement_distance.image = Json[arrayName][a]["image"];
                    achievement_distance.pointsToBeReady = int.Parse(Json[arrayName][a]["distance"]);
                    achievement_distance.Init();
                    achievements.Add(achievement_distance);
                    break;
            }
        }
    }
    public Achievement GetAchievement(int id)
    {
        return achievements[id];
    }
    
    

}
