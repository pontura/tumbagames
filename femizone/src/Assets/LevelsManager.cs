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
}
