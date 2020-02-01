using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

	public GameObject deaths;
	public Transform container;

	void Start () {
		Events.OnCharacterDie += OnCharacterDie;
	}
	void OnDestroy () {
		Events.OnCharacterDie -= OnCharacterDie;
	}
	void OnCharacterDie(Character ch)
	{
		GameObject go = Instantiate (deaths);
        World.Instance.objectsManager.AddObject(go.gameObject, ch.transform.localPosition);

    
	}
}
