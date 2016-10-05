using UnityEngine;
using System.Collections;

public class AreaSet : MonoBehaviour
{

    public int competitionsPriority;
    public bool randomize = true;
    public Level[] levels;
    public float distance;

    private int id = 0;

    public Vector3 getCameraOrientation()
    {
        return new Vector3(0, 0, 0);
    }

    public Level GetLevel()
    {
        Level level;

        if (randomize)
            level = levels[Random.Range(0, levels.Length)];
        else
            level = levels[id];

     //   Debug.Log("level: " + level.name + " id: " + id + " randomize: " + randomize);

        if (id < levels.Length - 1)
            id++;
        else
            id = 0;

        return level;
    }
}
