using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerGeneral : MonoBehaviour
{
    float lastClickedTime = 0;
	float delayToReact = 0.5f;
	bool processAxis;

    void Update()
    {
        for (int a = 1; a < 5; a++)
        {
            if (Input.GetButtonDown("Hit" + a))
            {
                Events.OnKeyPress(a);
            }
            else if (Input.GetButtonDown("Kick" + a))
            {
                Events.OnKeyPress(a);
            }
            lastClickedTime += Time.deltaTime;
            if (lastClickedTime > delayToReact)
				processAxis = true;
	
			if (processAxis)
			{
				int h = (int)Input.GetAxis("Horizontal" + a);
				if (h < -0.5f)
					Events.OnAxisChange(a, 1);
				else if (h > 0.5f)
					Events.OnAxisChange(a, -1);
			}
        }
    }
}
