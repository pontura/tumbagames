using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Achievement  {

    public string title;
    public string data;
    public types type;

	public int moodID;
	public int seccionalID;

    public enum types
    {
        MISSION_COMPLETE,
        DISTANCE,
        POWERUP,
		MONEY,
		UNLOCK,
        DEAD,
        NISMAN
    }
    public string image;
    public bool ready;
    public int points;
    public int progress;
    public int pointsToBeReady;
	public string listID;
	public int qty;

	public void Init () 
	{ 		
		CheckState ();

		if(ready) 
			return;

		OnInit ();
	}
	public virtual void OnInit ()  {  }

    public void Ready()
    {
        this.ready = true;
		AchievementsEvents.OnAchievementReady (this);
    }
	void CheckState()
	{
		//si es de cantidad:
		qty = GetQty (data);
		if (qty > 0 && qty >= pointsToBeReady) {
			Ready ();
			return;
		}		
		//si es unico y por barrio:
		points = PlayerPrefs.GetInt(type.ToString() + "_" + moodID + "_" + seccionalID);

		Debug.Log ("points: " + points + "   ________________  " + type.ToString () + "_" + moodID + "_" + seccionalID);

		if (points >= pointsToBeReady) {
			Ready ();
		}
	}
	public void Save(int pointsDone)
	{
		string saveName = type.ToString () + "_" + moodID + "_" + seccionalID;
		Debug.Log ("Achievement _____________ SAVE " +  saveName + " points: " + pointsDone);
		PlayerPrefs.SetInt(saveName,  pointsDone);
	}
	public void SaveInt(int qty)
	{
		Debug.Log ("Achievement _____________ SaveInt " +  data + " points: " + qty);
		PlayerPrefs.SetInt(data,  qty);
	}
	public int GetQty(string data)
	{
		Debug.Log ("GetQty " + data +  " es: " + PlayerPrefs.GetInt(data));
		return PlayerPrefs.GetInt(data);
	}
}
