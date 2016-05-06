using UnityEngine;
using System.Collections;

public class LevelLockedPopup : MonoBehaviour {

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
