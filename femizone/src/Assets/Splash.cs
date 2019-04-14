using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Linq;

public class Splash : MonoBehaviour {

 	[Serializable]
    public class Hiscore
    {
        public string username;
        public int hiscore;
    }
	public Transform container;
	public List<Hiscore> arrengedHiscores;
    public List<Hiscore> hiscores;
	//public Camera cam;
	public ScoreLine scoreLine;
    bool isReady;
    String[] arrLines;

	void Start () {		 		
        StartCoroutine(Delays());
	}
    IEnumerator Delays()
    {
        yield return new WaitForSeconds(0.25f);
        arrLines = File.ReadAllLines(Data.Instance.arcadeRanking.path);
        yield return new WaitForSeconds(1);
        LoadHiscores(Data.Instance.arcadeRanking.path);
        yield return new WaitForSeconds(2);
        Events.OnKeyPress += OnKeyPress;	
        isReady = true;
    }
	void OnDestroy () {
		 Events.OnKeyPress -= OnKeyPress;		
	}
	void OnKeyPress(int a)
	{
        if(isReady)
		    Data.Instance.LoadScene("Game");
	}
	/* void LoopCameraColors()
	{
		float r = (float)(Random.Range(0,1000))/1000;
		float g = (float)(Random.Range(0,1000))/1000;
		float b = (float)(Random.Range(0,1000))/1000;
		cam.backgroundColor = new Color(r,g,b,1);
		Invoke("LoopCameraColors", 0.5f);
	} */
	 void LoadHiscores(string fileName)
    {
       
        int num = 1;
        foreach (string line in arrLines)
        {

            string[] lines = line.Split("_"[0]);
            Hiscore hiscore = new Hiscore();
            hiscore.username = lines[0];
            hiscore.hiscore = int.Parse(lines[1]);
            hiscores.Add(hiscore);


            if (num < 16)
            {
                ScoreLine newScoreLine = Instantiate(scoreLine);
                newScoreLine.Init(num, hiscore.username, hiscore.hiscore);
                newScoreLine.transform.SetParent(container);
                newScoreLine.transform.localScale = Vector3.one;
            }

            num++;
        }
    }
	 List<Hiscore> OrderByHiscore(List<Hiscore> hs)
    {
        return hs.OrderBy(go => go.hiscore).Reverse().ToList();
    }
}
