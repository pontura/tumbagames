using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboProgress : MonoBehaviour
{
    public Text label;
    int comboID;
    float lastTimeCombo;
    Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        Events.OnComboDone += OnComboDone;
        Reset();
    }
    void OnDestroy()
    {
        Events.OnComboDone -= OnComboDone;
    }
    public void OnComboDone(int comboID)
    {
        if (comboID >= 4)
        {
            anim.Play("on");
            anim["on"].normalizedTime = 0;
            this.comboID = comboID;
            SetField();
            lastTimeCombo = Time.time;
        }    
    }
    void SetField()
    {
        if (comboID == 4)
            label.text = "Bien!";
        else if (comboID == 5)
            label.text = "Excelente!";
        else if (comboID > 5)
        {
            int newNum = 5 - comboID;
            label.text = "x" + newNum + "!";
        }   
    }
    void Update()
    {
        if (comboID == 0) return;
        if (Time.time > lastTimeCombo + 2)
            Reset();
    }
    void Reset()
    {
        label.text = "";
        comboID = 0;
    }
}
