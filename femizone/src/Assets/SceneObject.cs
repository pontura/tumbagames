using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	public Camera camera;
	void Start () {
		transform.LookAt (camera.transform);
	}
}
