using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {

    public float RepeatIn = 20;
    public List<GameObject> randomObjects;

    void Start()
    {
        Init();
    }
    void Init()
    {
        foreach (GameObject go in randomObjects)
            go.SetActive(false);
        int rand = Random.Range(0, randomObjects.Count);
        randomObjects[rand].SetActive(true);
    }
    public void Move(float _x)
    {
        Vector3 pos = transform.localPosition;
        pos.x -= _x;

        if (pos.x < -RepeatIn)
        {
            Init();
            int rand = Random.Range(8, 14);
            pos.x = rand;
        }

        transform.localPosition = pos;
        
    }
}
