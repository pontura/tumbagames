using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class ScoreLine : MonoBehaviour {

    public Text num;
    public Text username;
    public Text score;

	public void Init (int n, string u, int s) {
        num.text = n.ToString();
        username.text = u;
        score.text = s.ToString();
	}
}
