using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChallangesModeSelector : MonoBehaviour
{
    public Color op1Color;
    public Color op2Color;

    public Color UnselectedText;

    public GameObject interactiveBG;

    public Text op1Button;
    public Text op2Button;

    public Challenges challenges;

    void Start()
    {
        SetButton();
    }
    public void Toogle()
    {
        challenges.Switch();
        SetButton();
    }
    void SetButton()
    {
        if (challenges.type == Challenges.types.MADE)
        {
            op2Button.color = Color.white;
            op1Button.color = UnselectedText;
            MoveBg(false);
        }
        else
        {
            op1Button.color = Color.white;
            op2Button.color = UnselectedText;
            MoveBg(true);
        }
    }
    void MoveBg(bool left)
    {
        if (left)
            interactiveBG.GetComponent<Image>().color = op2Color;
        else
            interactiveBG.GetComponent<Image>().color = op1Color;
    }

}
