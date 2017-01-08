using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro2 : MonoBehaviour {

    public Sprite[] all;
    public Image container;

    void Start () {
        Loop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Data.Instance.LoadScene("Game");
        }
    }

    int id = 0;

    void Loop()
    {
        if (id >= all.Length) id = 0;
        Invoke("Loop", 0.5f);
        container.sprite = all[id];
        id++;
    }
}
