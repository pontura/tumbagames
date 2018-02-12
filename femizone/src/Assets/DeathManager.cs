using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

	public GameObject deaths;
	public Transform container;

	void Start () {
		Events.OnCharacterDie += OnCharacterDie;
	}
	void OnCharacterDie(Character ch)
	{
		GameObject go = Instantiate (deaths);
		go.transform.SetParent (container);
		go.transform.localPosition = ch.transform.localPosition;
		go.transform.localScale = Vector3.one;
		print ("_____________" + ch.transform.localPosition);
	}
}
