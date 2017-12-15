using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCamera : MonoBehaviour {

	public void UpdatePosition (float _x) {
		Vector3 pos = transform.position;
		Vector3 newPos = pos;
		newPos.x += _x;
		transform.position = Vector3.Lerp (pos, newPos, 0.1f);
	}
}
