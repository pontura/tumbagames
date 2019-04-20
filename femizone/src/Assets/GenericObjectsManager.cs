using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectsManager : MonoBehaviour
{
	public GenericObject[] all;
	public Transform container;

	public void InstantiateSceneOject(SceneObjectData data)
	{
		GenericObject go = GetObject (data);
		if (go != null) {
			GenericObject asset = Instantiate (go);
            asset.Init(data);
            asset.transform.SetParent (container);
			data.pos.y = 0;
			asset.transform.localPosition = data.pos;
			asset.transform.localEulerAngles = new Vector3 (30, 0, 0);           

        }
	}
	GenericObject GetObject(SceneObjectData data)
	{
		foreach (GenericObject go in all)
			if (go.GetComponent<SceneObjectData> ().type == data.type)
				return go;

		return null;
	}
}
