using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {

	public WeaponPickable weapon1;
	public WeaponPickable weapon2;

	public Transform container;
	public List<WeaponPickable> all;

	void Start () {

	}
	public void InstantiateSceneOject(SceneObjectData data)
	{
		WeaponPickable asset = Instantiate(weapon2);
		asset.transform.SetParent(container);
		asset.Init();
		data.pos.y = 0;
		asset.transform.localPosition = data.pos;
		all.Add (asset);
	}
}
