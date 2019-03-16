﻿using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class ScoreLine : MonoBehaviour {

    public Text num;
    public Text username;
    public Text score;

	public void Init (int _puesto, string _username, int _score) {
		
		if (_puesto != 0)
			num.text = _puesto.ToString();
        else 
            num.text = "";

		username.text = _username;
		score.text = Utils.FormatNumbers(_score);
	}
}
