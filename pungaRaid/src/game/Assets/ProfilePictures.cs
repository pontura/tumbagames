using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePictures : MonoBehaviour
{
    public Sprite[] all;

    public Sprite GetSprite(int id)
    {
        return all[id];
    }
}
