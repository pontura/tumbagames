using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPowerManager : MonoBehaviour
{
    Hero hero;
    float speedToStress = 0.25f;
    float speedToRecover = 0.3f;
    public float stressValue;
    float lastTimeAttack;
    public GameObject bar;
    public GameObject barContainer;

    private void Start()
    {
        hero = GetComponent<Hero>();
        Loop();
    }
    void Loop()
    {
        if (stressValue <= 0)
        {
            stressValue = 0;
            barContainer.SetActive(false);
        }
        else if (stressValue >= 9f)
        {
            Stress();
            stressValue = 1;
        }
        else
        {
            barContainer.SetActive(true);
        }
        stressValue -= speedToRecover;
        Invoke("Loop", 0.1f);
        bar.transform.localScale = new Vector3(stressValue/10, 1, 1);
    }
    public float CalculateStress()
    {
        if (hero.state == Hero.states.STRESS)
            return 1;
        float delay = Time.time - lastTimeAttack;
        lastTimeAttack = Time.time;
        if (delay<0.5f)
        {
            float value = 1 - (delay);
            stressValue += value + speedToStress;
            if (stressValue > 10)
                stressValue = 10;
        }
       
        return stressValue;
    }
    void Stress()
    {
        hero.Stress();
    }
}
