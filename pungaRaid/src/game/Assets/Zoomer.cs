using UnityEngine;
using System.Collections;

public class Zoomer : MonoBehaviour {

    float CameraSize = 5;
    private Camera camera;

	void Start () {
        camera = GetComponent<Camera>();
        StartCoroutine(ZoomOutFrom(2, 1.2f));
        
	}

    IEnumerator ZoomOutFrom(float zoomOutfrom, float speed)
    {
       
        float i = zoomOutfrom;
        while (i < CameraSize)
        {
            Vector2 pos = transform.localPosition;
            pos.x = 3 - (CameraSize - i);
            transform.localPosition = pos;

            i += Time.deltaTime * speed;
            camera.orthographicSize = i;
            yield return new WaitForEndOfFrame();
        }
        camera.orthographicSize = CameraSize;
        yield return null;
    }
}
