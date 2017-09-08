using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	Camera camera;

	void Start () {
		camera = World.Instance.camera;
		transform.LookAt (camera.transform);
	}
}
