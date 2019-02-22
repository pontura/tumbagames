using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
	public GameObject[] parts;

	void OnEnable()
	{
		if (parts.Length == 0)
			return;
		
		GameObject go;

		foreach (GameObject _go in parts) {
			_go.SetActive (false);
		}

		go = parts [Random.Range (0, parts.Length)];
		go.SetActive (true);

	}
}
