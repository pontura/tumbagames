using UnityEngine;
using System.Collections;

public class CombosManager : MonoBehaviour {

    private float distanceInCombo = 0.45f;
    private int MAX_COMBOS = 5;
    private float lastTime;
    public int comboID;

	void Start () {
        Events.OnCombo += OnCombo;
        Events.OnHeroDie += OnHeroDie;
	}
    void OnHeroDie()
    {
        comboID = 0;
    }
    public int GetMultiplier()
    {
        if (comboID < 4)
            return 1;
        else 
            return comboID -2;
    }
    void OnCombo (float distance)
    {
       // print("OnCombo time " + Time.time + "     lastTime: " + lastTime);
        float diffTime = Time.time - lastTime;

        if (diffTime > distanceInCombo)
            ResetCombo();

        comboID++;
        int comboSound = comboID;
        if (comboSound > MAX_COMBOS) comboSound = MAX_COMBOS;

        lastTime = Time.time;

        Events.OnComboDone(comboID);
        Events.OnSoundFX("CoinX" + comboSound);
	}
    void ResetCombo()
    {
        comboID = 0;
    }
}
