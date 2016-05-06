using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoodsManager : MonoBehaviour {

    public int currentMood;

    public List<int> unblocked;

    public GameObject mood1;
    public GameObject mood2;

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
        unblocked.Clear();
        unblocked.Add(1);
    }
	public void Init () {
        for(int a=1; a<10; a++)
        {
            if (PlayerPrefs.GetInt("mood" + a) == 1)
                unblocked.Add(a);
        }
	}
    public void SetCurrentMood(int id)
    {
        currentMood = id;
    }
    public void UnlockMood(int id)
    {        
        foreach (int mood in unblocked)
            if (id == mood) return;

        Debug.Log("UnlockMood:::: " + id);

        PlayerPrefs.SetInt("mood" + id, 1);
        unblocked.Add(id);
    }
    public GameObject GetCurrentMoodAsset()
    {
        switch (currentMood)
        {
            case 2: return mood2;
            default: return mood1;
        }
    }
    public bool IsMoodUnlocked(int id)
    {
        foreach (int mood in unblocked)
            if (id == mood) return true;
        return false;
    }
}
