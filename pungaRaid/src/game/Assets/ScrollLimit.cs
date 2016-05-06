using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollLimit : MonoBehaviour {

    public Vector2 Limits_y;

    void Start()
    {

    }
	void Update () 
    {
        Vector3 pos =  transform.localPosition;
        if (pos.y < Limits_y.x)
        {
            pos.y = 0;
        }
        else if (pos.y > Limits_y.y)
        {
            pos.y = Limits_y.y;
        }
        if(transform.localPosition != pos)
         transform.localPosition = pos;
	}
}
