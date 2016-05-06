using UnityEngine;
using System.Collections;

public class VeredaRandomItems : MonoBehaviour {

    public GameObject[] randomItems;

	void OnEnable () {
        if (randomItems.Length > 0)
        {
            foreach (GameObject items in randomItems)
                items.SetActive(false);

            randomItems[Random.Range(0, randomItems.Length)].SetActive(true);
        }
	}
}
