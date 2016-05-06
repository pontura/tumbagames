using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedbackSignal : MonoBehaviour {

    [SerializeField]
    Text field;

    void Start()
    {
        field.text = "";
    }
    public void Init(bool isOk)
    {
        GetComponent<Animation>().Rewind();
        GetComponent<Animation>().Play();
        if (isOk)
        {
            field.text = "+1";
            field.color = Color.yellow;
        }
        else
        {
            field.text = "-1";
            field.color = Color.red;
        }
    }
    public void Reset()
    {
       // Destroy(gameObject);
    }
}
