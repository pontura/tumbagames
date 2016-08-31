using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenericPopup : MonoBehaviour {

    public Text titleField;
    public Text descField;

    public void Init(string title, string desc)
    {
        titleField.text = title;
        descField.text = desc;
    }
}
