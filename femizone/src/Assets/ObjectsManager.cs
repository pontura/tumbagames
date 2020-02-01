using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public Transform container;
    [HideInInspector]
    public List<GameObject> all;

    private void Start()
    {
        Events.OnStageClear += OnStageClear;
    }
    private void OnDestroy()
    {
        Events.OnStageClear -= OnStageClear;
    }
    void OnStageClear()
    {
        print(" OnStageClear allAdded.Count : " + all.Count);
        int total = all.Count - 1;
        float distance = World.Instance.worldCamera.transform.position.x;
        while (total >= 0)
        {

            GameObject so = all[total];
            if (so.transform.position.x < distance - 10)
            {
                all.Remove(so);
                Destroy(so.gameObject);
            }
            print(so.transform.position.x + " distance: " + distance + " total: " + total);
            total--;
        }
    }
    public void AddObject(GameObject asset, Vector3 pos)
    {
        asset.transform.SetParent(container);
        asset.transform.localPosition = pos;
        asset.transform.localEulerAngles = new Vector3(30, 0, 0);
        asset.transform.localScale = Vector3.one;
        all.Add(asset);       
    }
}
