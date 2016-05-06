using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchivementButtonUI : MonoBehaviour {

    public Color readyColor;
    public int id;
    public Image container;
    private string url;
    public GameObject progress1;
    public GameObject progress2;
    public GameObject progress3;
    public GameObject progress4;
    public GameObject progress5;
    int progressId = 1;

    public void SetReady(bool isReady)
    {
        Color myColor = Color.white;
        if (!isReady)
            myColor = readyColor;

        container.color = myColor;

        switch (progressId)
        {
            case 1: progress1.GetComponent<Image>().color = myColor; break;
            case 2: progress2.GetComponent<Image>().color = myColor; break;
            case 3: progress3.GetComponent<Image>().color = myColor; break;
            case 4: progress4.GetComponent<Image>().color = myColor; break;
            case 5: progress5.GetComponent<Image>().color = myColor; break;
        }
    }

    public void SetProgress(int progressId)
    {
        this.progressId = progressId;
        progress1.SetActive(false);
        progress2.SetActive(false);
        progress3.SetActive(false);
        progress4.SetActive(false);
        progress5.SetActive(false);

        switch (progressId)
        {
            case 1: progress1.SetActive(true); break;
            case 2: progress2.SetActive(true); break;
            case 3: progress3.SetActive(true); break;
            case 4: progress4.SetActive(true); break;
            case 5: progress5.SetActive(true); break;
        }
    }
    public void LoadImage(string _url)
    {
        this.url =  @"file://" + @"images\achievements\" + _url;
        StartCoroutine("LoadRoutine");        
    }
    private IEnumerator LoadRoutine()
    {
        print("LoadImages url: " + url);
        WWW www = new WWW(url);
        yield return www;

        if (www.error != null)
        {
            container.sprite = null;
        }
        else
        {
            Sprite sprite = new Sprite();
            sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0), 100.0f);
            container.sprite = sprite;
        }
    }
}
