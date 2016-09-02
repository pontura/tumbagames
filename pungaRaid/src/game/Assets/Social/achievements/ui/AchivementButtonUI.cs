using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchivementButtonUI : MonoBehaviour {

    public int id;
    public Image image;
    public Image mask;
    public Text field;
    private string url;
    public bool isReady;

    public void Init(bool isReady, string url, string _text)
    {
        field.text = _text;
        LoadImage(url);
        this.isReady = isReady;
        if (isReady) mask.enabled = false;
    }
    public void LoadImage(string url)
    {
        image.sprite = Resources.Load("achievements/" + url, typeof(Sprite)) as Sprite;
    }
}
