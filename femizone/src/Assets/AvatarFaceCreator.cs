using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarFaceCreator : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] avatars;

    void Start()
    {
        panel.SetActive(false);
    }
    public void Init(int characterID)
    {
        panel.SetActive(true);
        int id = 1;
        foreach (GameObject go in avatars)
        {
            if (id == characterID)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
            id++;
        }

    }
}
