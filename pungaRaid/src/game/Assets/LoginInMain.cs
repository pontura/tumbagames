using UnityEngine;
using System.Collections;

public class LoginInMain : MonoBehaviour {

    public GameObject panel;

	void Start () {
	    if(SocialManager.Instance.userData.facebookID == "")
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
	}
	public void Clicked()
    {
        Events.OnLoginAdvisor();
    }
}
