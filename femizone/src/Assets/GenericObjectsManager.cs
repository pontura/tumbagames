using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectsManager : MonoBehaviour
{
	public GenericObject[] all;
	public Transform container;
    [HideInInspector]

    public void InstantiateSceneOject(SceneObjectData data)
	{
		GenericObject go = GetObject (data);
		if (go != null) {
			GenericObject asset = Instantiate (go);            
			data.pos.y = 0;
            World.Instance.objectsManager.AddObject(asset.gameObject, data.pos);
            asset.Init(data);
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
