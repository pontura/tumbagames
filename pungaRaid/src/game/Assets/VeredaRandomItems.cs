using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VeredaRandomItems : MonoBehaviour {

    public List<ObjectFilter> randomItems;

    public void OnInit()
    {

        List<GameObject> newArray = new List<GameObject>();
        MoodsManager.moods mood = Data.Instance.moodsManager.currentMood;

        foreach (ObjectFilter of in randomItems)
        {
            if (of.CanBeAdded(mood, 0))
                newArray.Add(of.gameObject);
            else
                of.gameObject.SetActive(false);
        }
        
        if (newArray.Count > 0)
        {
          
            int rand = Random.Range(0, newArray.Count);

          //  print("Vereda: " + name + "   totoal: " + newArray.Count + " rand = " + rand);

            int id = 0;
            foreach(GameObject go in newArray)
            {
                if(id == rand)
                    go.SetActive(true);
                else
                    go.SetActive(false);
                id++;
            }
        }
	}
}
