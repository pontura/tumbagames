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
        AREA,
		MONEY
    }
    public string image;
    public bool ready;
    public int points;
    public int progress;
    public int pointsToBeReady;
	public string listID;

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
    }
	void CheckState()
	{
		points = PlayerPrefs.GetInt(type.ToString() + "_" + moodID + "_" + seccionalID);
		if (points >= pointsToBeReady)
			Ready ();
	}
	public void Save(int pointsDone)
	{
		string saveName = type.ToString () + "_" + moodID + "_" + seccionalID;
		Debug.Log ("Achievement _____________ SAVE " +  saveName + " points: " + pointsDone);
		PlayerPrefs.SetInt(saveName,  pointsDone);
	}
}
