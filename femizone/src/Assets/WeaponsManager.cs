using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {

	public WeaponPickable weapon1;
	public WeaponPickable weapon2;
    public WeaponPickable cinturonga;

    public Transform container;
	public List<WeaponPickable> all;

	public void InstantiateSceneOject(SceneObjectData data)
	{
        print("WeaponsManager InstantiateSceneOject " + data.type);
        WeaponPickable asset = null;
        if (data.type == SceneObjectData.types.WEAPON)
        {
            if(Random.Range(0,10)<5)
                asset = Instantiate(weapon2);
            else
                asset = Instantiate(weapon1);
        }
       else if (data.type == SceneObjectData.types.CINTURONGA)
            asset = Instantiate(cinturonga);

        if (asset == null)
        return;

		asset.transform.SetParent(container);
		asset.Init();
		data.pos.y = 0;
		asset.transform.localPosition = data.pos;
		all.Add (asset);
	}
}
