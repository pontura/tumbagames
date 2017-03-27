using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
		DASH,
		TIMEKILLED,
        NISMAN,
		MULTIPLE
    }
    public string image;
    public bool ready;
    public int points;
    public int pointsToBeReady;
	public string listID;
	public int qty;



	[Serializable]
	public class MultipleData
	{
		public types type;
		public string data;
		public int pointsToBeReady;
	}
	public List<MultipleData> multipleData;



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
		//Debug.Log ("Ready: " + type.ToString () );
        this.ready = true;
		AchievementsEvents.OnAchievementReady (this);
    }
	void CheckState()
	{
		if (type == types.MULTIPLE) {
			//Debug.Log ("Chequea_ : " + data + " resultado: " + PlayerPrefs.GetInt (data));

			if (PlayerPrefs.GetInt (data) == 1)
				Ready ();
			return;
		}
		//si es de cantidad:
		qty = GetQty (data);
		if (qty > 0 && qty >= pointsToBeReady) {
			Ready ();
			return;
		}		
		//si es unico y por barrio:
		points = PlayerPrefs.GetInt(type.ToString() + "_" + moodID + "_" + seccionalID);

		//Debug.Log ("points: " + points + "   ________________  " + type.ToString () + "_" + moodID + "_" + seccionalID);

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
		//Debug.Log ("Achievement _____________ SaveInt " +  data + " points: " + qty);
		PlayerPrefs.SetInt(data,  qty);
	}
	public void SaveMultipleData()
	{
		//usamos la variable: data
		Debug.Log ("SaveMultipleData _____________ SaveInt " +  data);
		PlayerPrefs.SetInt(data,  1);
	}
	public int GetQty(string data)
	{
		//Debug.Log ("GetQty " + data +  " es: " + PlayerPrefs.GetInt(data));
		return PlayerPrefs.GetInt(data);
	}
	public void SetMultiple(string data)
	{
		this.data = data;
		multipleData = new List<MultipleData> ();
		string[] d = data.Split(","[0]);
		foreach (string r in d) {
			MultipleData mData = new MultipleData ();
			string[] arr = r.Split("_"[0]);
			string type_string = arr[0];
			string dataLine_string = arr[1];

			string[] var_arr = dataLine_string.Split("="[0]);
			string dataName = var_arr[0];
			string pointsToBeReady = var_arr[1];

			switch(type_string)
			{
				case "DEAD":
					mData.type = types.DEAD;
					break;
				case "DASH":
					mData.type = types.DASH;
					break;
				case "TIME-KILLED":
					mData.type = types.TIMEKILLED;
					break;
			}
			mData.data = dataName;
			mData.pointsToBeReady = int.Parse (pointsToBeReady);
			multipleData.Add (mData);
		}
	}
}
