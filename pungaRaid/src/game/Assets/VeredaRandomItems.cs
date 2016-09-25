using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VeredaRandomItems : MonoBehaviour {

    public List<GameObject> randomItems;

	void OnEnable () {

        List<GameObject> newArray = new List<GameObject>();
        MoodsManager.moods mood = Data.Instance.moodsManager.currentMood;

        foreach (ObjectFilter of in GetComponentsInChildren<ObjectFilter>())
        {
            if (of.CanBeAdded(mood, 0))
                newArray.Add(of.gameObject);
            else
                of.gameObject.SetActive(false);
        }

        print("agrega: " + newArray.Count);

        if (newArray.Count > 0)
            newArray[Random.Range(0, newArray.Count)].SetActive(true);
	}
}
