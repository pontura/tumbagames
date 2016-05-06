using UnityEngine;
using System.Collections;

public class AreaSet : MonoBehaviour
{

    public int competitionsPriority;
    public bool randomize = true;
    public Level[] levels;
    public int distance;

    [HideInInspector]
    public int id = 0;

    public Vector3 getCameraOrientation()
    {
        return new Vector3(0, 0, 0);
    }

    public Level GetLevel()
    {
        Level level;

        Random.seed = (int)System.DateTime.Now.Ticks;

        if (randomize)
            level = levels[Random.Range(0, levels.Length)];
        else
            level = levels[id];

        //Debug.Log(randomize + " area name: " + area.name + " id : " + num + " areas length: " + areas.Length);

        if (id < levels.Length - 1)
            id++;

        return level;
    }
}
