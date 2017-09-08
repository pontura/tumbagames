using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
	static World mInstance = null;
	public Camera camera;

	public static World Instance
	{
		get
		{
			return mInstance;
		}
	}

	void Awake()
	{
		if (!mInstance)
			mInstance = this;
	}
}
