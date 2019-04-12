using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelsManager : MonoBehaviour {
	public List<LevelsByDificulty> levelsByDificulty;
	[Serializable]
	public class LevelsByDificulty
	{
		public List<Level> all;
		public int total;
	}
}
