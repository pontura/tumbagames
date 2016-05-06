using UnityEngine;
using System.Collections;

public class CombosManager : MonoBehaviour {

    private float distanceInCombo = 4;
    private int MAX_COMBOS = 5;
    private float lastDistance;
    public int comboID;

	void Start () {
        Events.OnCombo += OnCombo;
        Events.OnHeroDie += OnHeroDie;
	}
    void OnHeroDie()
    {
        comboID = 0;
    }
    void OnCombo (float distance)
    {
        float diffDistance = distance - lastDistance;

        if (diffDistance > distanceInCombo)
            ResetCombo();
        else comboID++;

        if (comboID > MAX_COMBOS) comboID = MAX_COMBOS;

        lastDistance = distance;
        
        Events.OnSoundFX("CoinX" + comboID);
	}
    void ResetCombo()
    {
        comboID = 1;
    }
}
