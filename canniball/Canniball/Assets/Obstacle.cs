using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {

    public float RepeatIn = 20;
    public List<GameObject> randomObjects;
    public List<GameObject> randomObjects_level2;

    public void Init()
    {
        int rand = Random.Range(14, 20);
        Vector3 pos = transform.localPosition;
        pos.x = rand;
        transform.localPosition = pos;

        foreach (GameObject go in randomObjects)
            go.SetActive(false);
        foreach (GameObject go in randomObjects_level2)
            go.SetActive(false);

        if (Data.Instance.GetComponent<LevelsData>().level == 2)
        {
            rand = Random.Range(0, randomObjects_level2.Count);
            randomObjects_level2[rand].SetActive(true);
        }
        else
        {
            rand = Random.Range(0, randomObjects.Count);
            randomObjects[rand].SetActive(true);
        }
    }
    public void Move(float _x)
    {
        Vector3 pos = transform.localPosition;
        pos.x -= _x;

        if (pos.x < -RepeatIn)
            Init();
        else
            transform.localPosition = pos;
        
    }
}
