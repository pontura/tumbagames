using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarsSignal : MonoBehaviour {

	public AvatarSignal signal;
	public Transform container;

	void Start () {
		Events.OnAvatarSignal += OnAvatarSignal;
	}

	void OnAvatarSignal (Vector3 pos, string avatarName) {
		AvatarSignal avatarSignal = Instantiate (signal);
		avatarSignal.transform.SetParent (container);
		avatarSignal.transform.localScale = Vector3.one;
		avatarSignal.transform.position = pos;
		avatarSignal.Init (avatarName);
	}
}
