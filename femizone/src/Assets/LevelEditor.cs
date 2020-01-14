using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    public GameObject container;
    private void Start()
    {
        Level[] levels = container.GetComponentsInChildren<Level>();
        SceneManager.LoadScene("Game");
        Data.Instance.levelsManager.ForceLevel(levels);
    }
}
