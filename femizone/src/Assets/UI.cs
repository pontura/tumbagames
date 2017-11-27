using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

	static UI mInstance = null;
	public UIProgressBarManager progressBarManager;

	public static UI Instance
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
		else
		{
			Destroy(this.gameObject);
			return;
		}
		progressBarManager = GetComponent<UIProgressBarManager> ();
	}
}
