using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour {

	public Powerup energyAsset;

	public Transform container;
	public List<Powerup> all;

	void Start () {
		
	}
	public void InstantiateSceneOject(SceneObjectData data)
	{
		Powerup asset = Instantiate(energyAsset);
		asset.transform.SetParent(container);
		asset.Init();
		data.pos.y = 0;
		asset.transform.localPosition = data.pos;
		all.Add (asset);
		asset.transform.localEulerAngles = Vector3.zero;
	}
}
