using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsManager : MonoBehaviour {
	[SerializeField] private List<LevelsByDificulty> levelsByDificulty;
	[Serializable]
	public class LevelsByDificulty
	{
		public List<Level> all;
		public int total;
	}

    public List<LevelsByDificulty> All
    {
        get { return levelsByDificulty; }
    }
    public void ForceLevel(Level[] levels)
    {
        levelsByDificulty[1].all.Clear();
        foreach (Level l in levels)
        {
            Level level = Resources.Load<Level>("levels/" + l.name);
            levelsByDificulty[1].all.Add(level);
        }
       
    }
}
