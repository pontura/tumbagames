using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fade : MonoBehaviour
{
    public Image masker;
    private string m_LevelName = "";
    private int m_LevelIndex = 0;
    private bool m_Fading = false;
    private GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        masker.gameObject.SetActive(true);
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>();
        masker.enabled = true;
		masker.color = new Color(0,0,0,0);
        graphicRaycaster.enabled = false;
	}
    private IEnumerator FadeStart(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        masker.enabled = true;
        graphicRaycaster.enabled = true;
        float t = 0;
		while (t < 1)
		{
			yield return new WaitForEndOfFrame();
			t+=Time.deltaTime;
			masker.color = new Color(0,0,0,t);
		}

        if (m_LevelName != "")
            Application.LoadLevel(m_LevelName);
        else
            Application.LoadLevel(m_LevelIndex);     
		while (t > 0f)
		{
			yield return new WaitForEndOfFrame();
			t-=Time.deltaTime;
			masker.color = new Color(0,0,0,t);
		}
        graphicRaycaster.enabled = false;
        m_Fading = false;
        masker.enabled = false;
    }
    private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        m_Fading = true;
        StartCoroutine(FadeStart(aFadeOutTime, aFadeInTime, aColor));
    }

    public void LoadLevel(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
    {
        //if (Fading) return;
        m_LevelName = aLevelName;
        StartFade(aFadeOutTime, aFadeInTime, aColor);
    }
}