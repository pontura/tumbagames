	using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class AchievementsManager : MonoBehaviour
{
	public List<string> rangos;
	public List<Achievement> achievements;
	public int totalAchievements;
	string jsonUrl = "achievements";
	string jsonUrl2 = "achievementsMultiple";

	const string PREFAB_PATH = "AchievementsManager";
	static AchievementsManager mInstance = null;
	public AchievementsMultipleManager achievementsMultipleManager;
    

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

		TextAsset file2 = Resources.Load(jsonUrl2) as TextAsset;
		LoadDataromServer(file2.text);

		SocialEvents.ResetApp += ResetApp;

		achievementsMultipleManager = GetComponent<AchievementsMultipleManager> ();
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
		//print (json_data);
		var Json = SimpleJSON.JSON.Parse(json_data);
		string arrayName = "achievements";
		//achievements = new List<Achievement>(Json[arrayName].Count);
		for (int a = 0; a < Json[arrayName].Count; a++)
		{
			totalAchievements++;
			string type = Json[arrayName][a]["type"];

			Achievement achievement = null;
			if (type == "UNLOCK") {
				achievement = new AchievementUnlock ();   
				achievement.type = Achievement.types.UNLOCK;
			} else
			if (type == "DISTANCE") {
				achievement = new AchievementDistance ();   
				achievement.type = Achievement.types.DISTANCE;
			} else if (type == "POWERUP") {
				achievement = new AchievementPowerup ();
				achievement.type = Achievement.types.POWERUP;
			} else if (type == "MONEY") {
				achievement = new AchievementMoney ();
				achievement.type = Achievement.types.MONEY;
			}
            else if (type == "MULTIPLE")
            {
				achievement = new AchievementMultiple();
				achievement.type = Achievement.types.MULTIPLE;
				achievement.SetMultiple( Json[arrayName][a]["multipleData"] );
            }
            else if (type == "NISMAN")
            {
                achievement = new AchievementDead();
                achievement.type = Achievement.types.NISMAN;
            }

            achievement.title = Json[arrayName][a]["title"];
			achievement.image = Json[arrayName][a]["image"];

			if(Json[arrayName][a]["pointsToBeReady"] != null)
				achievement.pointsToBeReady = int.Parse(Json[arrayName][a]["pointsToBeReady"]);
			
			achievement.listID = Json[arrayName][a]["listID"];

			if(Json[arrayName][a]["data"] != null)
				achievement.data = Json[arrayName][a]["data"];
			
			if(Json[arrayName][a]["mood"] != null)
				achievement.moodID = int.Parse(Json[arrayName][a]["mood"]);
			
			if(Json[arrayName][a]["seccional"] != null)
				achievement.seccionalID = int.Parse(Json[arrayName][a]["seccional"]);
			
			achievement.Init();
			achievements.Add(achievement);
		}
	}
	public List<Achievement> GetAchievementsByListID(string listID)
	{
		List<Achievement> list = new List<Achievement>();
		foreach(Achievement ach in achievements)
		{
			if(ach.listID == listID)
				list.Add(ach);
		}
		return list;
	}
	public Achievement GetAchievement(int id)
	{
		return achievements[id];
	}



}
