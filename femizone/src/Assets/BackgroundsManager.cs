using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsManager : MonoBehaviour {

	public Transform container;

	public void AddBackground (GameObject go, Vector3 offset) {
		GameObject newBG = Instantiate(go);
		newBG.transform.SetParent (container);
		newBG.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
		newBG.transform.localEulerAngles = Vector3.zero;
		newBG.transform.localPosition = offset;
	}

}
