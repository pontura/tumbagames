using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VeredaRandomItems : MonoBehaviour {

    public GameObject[] randomItems;

	void OnEnable () {
        if (randomItems.Length > 0)
        {
            List<GameObject> newArray = new List<GameObject>();
            MoodsManager.moods mood = Data.Instance.moodsManager.currentMood;

            foreach (GameObject item in randomItems)
            {
                if (item.GetComponent<ObjectFilter>())
                {
                    if (item.GetComponent<ObjectFilter>().CanBeAdded(mood, 0))
                        newArray.Add(item);
                }
                else
                {
                    newArray.Add(item);
                }
            }

            foreach (GameObject items in randomItems)
                items.SetActive(false);

            newArray[Random.Range(0, newArray.Count)].SetActive(true);
        }
	}
}
