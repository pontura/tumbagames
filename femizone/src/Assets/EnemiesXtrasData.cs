using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemiesXtrasData : MonoBehaviour
{
    public List<Config> all;

    [Serializable]
    public class Config
    {
        public float delay;
        public SceneObjectData.types type;
        public PositionData positionData;
        public enum PositionData
        {
            LEFT,
            RIGHT
        }
    }
}
