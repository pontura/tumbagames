﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour {

	public Powerup energyAsset;
	public Powerup energyAssetBig;

	public Transform container;
	public List<Powerup> all;

	void Start () {
		
	}
	public void InstantiateSceneOject(SceneObjectData data)
	{
		int rand = Random.Range (0, 10);
		Powerup asset;
		if(rand >5)
			asset = Instantiate(energyAsset);
		else
			asset = Instantiate(energyAssetBig);
		
		asset.transform.SetParent(container);
		asset.Init();
		data.pos.y = 0;
		asset.transform.localPosition = data.pos;
		all.Add (asset);
		asset.transform.localEulerAngles = Vector3.zero;
	}
}
