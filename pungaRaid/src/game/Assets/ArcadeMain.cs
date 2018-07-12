using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class ArcadeMain : MonoBehaviour {

	public GameObject[] buttons;
	public int id;
	public Text title;
	public Text desc;
	public ScoreLine scoreLine;
	public Transform container;

	string fileName_pr_1 = "C:\\tumbagames\\hiscores\\PR_1.txt";
	string fileName_pr_2 = "C:\\tumbagames\\hiscores\\PR_2.txt";
	string fileName_pr_3 = "C:\\tumbagames\\hiscores\\PR_3.txt";
	string fileName_pr_4 = "C:\\tumbagames\\hiscores\\PR_4.txt";
	string fileName_pr_5 = "C:\\tumbagames\\hiscores\\PR_5.txt";
	string fileName_pr_6 = "C:\\tumbagames\\hiscores\\PR_6.txt";

	void Start () {
		SetSelected ();
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			id++;
			if (id > buttons.Length - 1)
				id = 0;
			SetSelected ();
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			id--;
			if (id < 0)
				id = buttons.Length-1;
			SetSelected ();
		}  else if (Input.GetKeyDown (KeyCode.Space)) {
			Go();
		}

	}
	void Go()
	{
		Events.OnLoadCurrentAreas();
		Data.Instance.LoadLevel("03_PreloadingGame");
	}
	void SetSelected()
	{
		Utils.RemoveAllChildsIn (container);

		string _title = "";
		string _desc = "";

		int moodID;
		int seccionalID;

		if (id == 0) {
			_title = Data.Instance.moodsManager.data.data [0].seccional [0].title;
			_desc = Data.Instance.moodsManager.data.data [0].seccional [0].name;
			moodID = 1;
			seccionalID = 1;
			LoadAll (fileName_pr_1);
		} else if (id == 1) {
			_title = Data.Instance.moodsManager.data.data [0].seccional [1].title;
			_desc = Data.Instance.moodsManager.data.data [0].seccional [1].name;
			moodID = 1;
			seccionalID = 2;
			LoadAll (fileName_pr_2);
		}
		else if (id == 2){
			_title = Data.Instance.moodsManager.data.data [0].seccional [2].title;	
			_desc = Data.Instance.moodsManager.data.data [0].seccional [2].name;
			moodID = 1;
			seccionalID = 3;
			LoadAll (fileName_pr_3);
		}

		else if (id == 3){
			_title = Data.Instance.moodsManager.data.data [1].seccional [0].title;
			_desc = Data.Instance.moodsManager.data.data [1].seccional [0].name;
			moodID = 2;
			seccionalID = 1;
			LoadAll (fileName_pr_4);
		}
		else if (id == 4){
			_title = Data.Instance.moodsManager.data.data [1].seccional [1].title;
			_desc = Data.Instance.moodsManager.data.data [1].seccional [1].name;
			moodID = 2;
			seccionalID = 2;
			LoadAll (fileName_pr_5);
		}
		else {
			_title = Data.Instance.moodsManager.data.data [1].seccional [2].title;
			_desc = Data.Instance.moodsManager.data.data [1].seccional [2].name;
			moodID = 2;
			seccionalID = 3;
			LoadAll (fileName_pr_6);
		}

		Data.Instance.moodsManager.SetCurrentMood(moodID);
		Data.Instance.moodsManager.SetCurrentSeccional(seccionalID);


		title.text = _title;
		desc.text = _desc;
		foreach (GameObject go in buttons) {
			go.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}

		buttons[id].transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
	}





		public ScoreLine scoreLineNewHiscore;

		public ScoreData data;

		


		[Serializable]
		public class ScoreData
		{
			public string prefix;
			public string title;
			public int hiscore;
			public string url;
		}

		public List<Hiscore> arrengedHiscores;
		public List<Hiscore> hiscores;
		[Serializable]
		public class Hiscore
		{
			public string username;
			public int hiscore;       
		}
		void LoadAll (string fileName) {
			LoadHiscores(fileName);
		}
		void LoadHiscores(string fileName)
		{
			String[] arrLines = File.ReadAllLines(fileName);
			int num = 1;
			foreach (string line in arrLines)
			{
				string[] lines = line.Split("_"[0]);
				Hiscore hiscore = new Hiscore();
				hiscore.username = lines[0];
				hiscore.hiscore = int.Parse(lines[1]);
				hiscores.Add(hiscore);

				if(num<8)
				{
					ScoreLine newScoreLine = Instantiate(scoreLine);                
					newScoreLine.Init(num, hiscore.username, hiscore.hiscore);               
					newScoreLine.transform.SetParent(container);
					newScoreLine.transform.localScale = Vector3.one;
				}               

				num++;
			} 
		}

	}

