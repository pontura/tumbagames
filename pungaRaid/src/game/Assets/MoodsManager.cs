using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MoodsManager : MonoBehaviour {

    public moods currentMood;
    public int currentSeccionalID;

    public TextsMoods data;

    public GameObject mood_belgrano;
    public GameObject mood_madero;    

    public enum moods
    {
        GENERIC,
        BELGRANO,
        MADERO
    }

    void Start()
    {
        Events.UnlockSeccional += UnlockSeccional;
        SocialEvents.ResetApp += ResetApp;
    }
    void OnDestroy()
    {
        Events.UnlockSeccional -= UnlockSeccional;
        SocialEvents.ResetApp -= ResetApp;
    }
    void ResetApp()
    {
      //  unblocked.Clear();
       // unblocked.Add(1);
    }
	public void Init () {
        //for(int a=1; a<10; a++)
        //{
        //    if (PlayerPrefs.GetInt("mood" + a) == 1)
        //        unblocked.Add(a);
        //}
	}
    public int GetCurrentMoodID()
    {
        switch (currentMood)
        {
            case moods.BELGRANO:    return 1;
            case moods.MADERO: return 2; 
        }
        return 1;
    }
    public Seccional GetCurrentSeccional()
    {
        int moodID = GetCurrentMoodID();
        return data.data[moodID - 1].seccional[currentSeccionalID-1];
    }
    public void SetCurrentSeccional(int id)
    {
        currentSeccionalID = id;
    }
    public void SetCurrentMood(int id)
    {
        switch (id)
        {
            case 1: currentMood = moods.BELGRANO; break;
            case 2: currentMood = moods.MADERO; break;
        }
    }
    public void UnlockMood(int id)
    {
        foreach (TextsMoods.Data moodData in data.data)
            if (moodData.id == id) moodData.unlocked = true;

        PlayerPrefs.SetInt("mood" + id, 1);
    }
    public void UnlockSeccional(int moodID, int seccionalID)
    {
        string seccional_name = "";
        foreach (TextsMoods.Data moodData in data.data)
        {
            if (moodData.id == moodID)
            {
                foreach (Seccional seccional in moodData.seccional)
                {
                    if (seccional.id == seccionalID)
                    {
                        seccional.unlocked = true;
                        seccional_name = seccional.title;
                    }
                    
                }
            }
        }
        SetCurrentSeccional(seccionalID);
        SetCurrentMood(moodID);

        string saver = "mood_" + moodID + "_" + seccionalID;
        print("UnlockSeccional saver: " + saver);
        
        PlayerPrefs.SetInt(saver, 1);
        StatsManager.TrackEvent("Unlock_" + seccional_name, moodID);
    }
    public GameObject GetCurrentMoodAsset()
    {
        switch (currentMood)
        {
            case moods.BELGRANO: return mood_belgrano;
            case moods.MADERO: return mood_madero; 
        }
        return mood_belgrano; 
    }
    public bool IsMoodUnlocked(int id)
    {
        foreach (TextsMoods.Data moodData in data.data)
            if (moodData.id == id) return moodData.unlocked;
        return false;
    }
    public Seccional GetNextSeccionalToBuy()
    {
        int money = SocialManager.Instance.userHiscore.money;
        foreach(TextsMoods.Data d in data.data)
        {
            int id=0;
            foreach (Seccional seccional in d.seccional)
            {
                if (seccional.unlocked == false && seccional.price < money)
                {
                    return seccional;
                }
                id++;
            }
        }
        return null;
    }
}
