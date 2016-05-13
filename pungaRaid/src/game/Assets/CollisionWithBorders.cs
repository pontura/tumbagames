using UnityEngine;
using System.Collections;

public class CollisionWithBorders : MonoBehaviour {

    void Update()
    {
        RaycastHit[] hits;
        Vector3 pos = transform.localPosition;
        pos.z = -10;
        hits = Physics.RaycastAll(pos, transform.forward, 100.0F);

        print(pos +  " transform.forward: " + transform.forward + " hits: " + hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            print(hit.transform.gameObject.name);

            //Renderer rend = hit.transform.GetComponent<Renderer>();

            //if (rend)
            //{
            //    // Change the material of all hit colliders
            //    // to use a transparent shader.
            //    rend.material.shader = Shader.Find("Transparent/Diffuse");
            //    Color tempColor = rend.material.color;
            //    tempColor.a = 0.3F;
            //    rend.material.color = tempColor;
            //}
        }
    }
}
