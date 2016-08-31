using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MoodsManager : MonoBehaviour {

    public moods currentMood;
    public TextsMoods data;

    public GameObject mood_belgrano;
    public GameObject mood_madero;

    private int currentSeccionalID;

    public enum moods
    {
        GENERIC,
        BELGRANO,
        MADERO
    }

    void Start()
    {
        SocialEvents.ResetApp += ResetApp;
    }
    void OnDestroy()
    {
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
}
