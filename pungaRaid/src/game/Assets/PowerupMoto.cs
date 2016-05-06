using UnityEngine;
using System.Collections;

public class PowerupMoto : PowerUpOn {

    public GameObject rueda1;
    public GameObject rueda2;

    private float rot;

    public override void OnInit()
    {
        rot = 0;
        Events.OnSoundFXLoop("Motor");
        int laneActiveID = Game.Instance.gameManager.characterManager.lanes.laneActiveID;
        string lane = "lane" + laneActiveID;
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingLayerName = lane;
        }
    }
    void Update()
    {
        rot -= Time.deltaTime * 300;
        if(rot <0) rot = 360;
        rueda1.transform.localEulerAngles = new Vector3(0,0, rot);
        rueda2.transform.localEulerAngles = new Vector3(0, 0, rot);
    }
}
