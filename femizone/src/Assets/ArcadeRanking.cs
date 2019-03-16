using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.IO;
using System.Linq;

public class ArcadeRanking : MonoBehaviour {

	public string path;
	public List<Hiscore> all;

	[Serializable]
	public class Hiscore
	{
		public string username;
		public int hiscore;       
	}
	void Start () {
		Events.RefreshHiscores += RefreshHiscores;
		path = Application.streamingAssetsPath + "/hiscores.txt";
		LoadHiscores(path);
	}
	void RefreshHiscores()
	{
		LoadHiscores (path);
	}
	void LoadHiscores(string fileName)
	{
		String[] arrLines = File.ReadAllLines(fileName);
		all.Clear ();
		foreach (string line in arrLines)
		{
			string[] lines = line.Split("_"[0]);
			Hiscore hiscore = new Hiscore();
			hiscore.username = lines[0];
			hiscore.hiscore = int.Parse(lines[1]);
			all.Add(hiscore);
		} 
	}

}
