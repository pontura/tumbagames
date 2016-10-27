using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public GameObject panel;
    public Image masker;

    void Start()
    {
        panel.SetActive(false);
        Events.OnFade += OnFade;
    }
    void OnDestroy()
    {
        Events.OnFade -= OnFade;
    }
    void OnFade(bool open)
    {
        panel.SetActive(true);
        StartCoroutine(FadeStart());
	}
    private IEnumerator FadeStart()
    {
        masker.fillAmount = 1;
        float t = 1;
		while (t > 0)
		{			
			t-=Time.deltaTime;
            masker.fillAmount = t;
            yield return new WaitForEndOfFrame();
        }
        masker.fillAmount = 0;
        panel.SetActive(false);
    }   
}