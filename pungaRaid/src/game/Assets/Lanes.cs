using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lanes : MonoBehaviour {

    //public GameObject background;
   // public List<GameObject> backgrounds;
    public Lane[] all;
    public int laneActiveID = 3;
    public GameObject enemy;
    public Vereda vereda;

	void Start () {
        Events.OnPoolAllItemsInScene += OnPoolAllItemsInScene;
	}
    void OnDestroy()
    {
        Events.OnPoolAllItemsInScene -= OnPoolAllItemsInScene;
    }
    void OnPoolAllItemsInScene()
    {
        List<Enemy> enemies = new List<Enemy>();

        //busca veredas en el ROOT
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Enemy>())
            {
                Enemy enemy = child.GetComponent<Enemy>();
                if (enemy)
                    enemies.Add(enemy);
            }
        }
        //busca enemigos en cada lane:
        foreach (Lane lane in all)
        {
            foreach (Transform child in lane.transform)
            {
                if (child.GetComponent<Enemy>())
                {
                    Enemy enemy = child.GetComponent<Enemy>();
                    if (enemy)
                        enemies.Add(enemy);
                }
                else
                {
                    Destroy(child.gameObject);
                }
            }
        }
        foreach (Enemy enemy in enemies)
        {
            Data.Instance.enemiesManager.Pool(enemy);
        }
    }
    public Lane GetActivetLane()
    {
        return all[laneActiveID];
    }
    public void AddBackground(Vereda _vereda, int _x, int _sceneWidth)
    {
        //print(_vereda.name + "    : AddBackground  in " + _x);
        Vereda vereda = Data.Instance.enemiesManager.GetEnemy(_vereda.name) as Vereda;
        GameObject go = vereda.gameObject;
        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(_x, 0, 0);

        if (vereda.blockLane0) AddBlocker(0, _x, _sceneWidth);
        if (vereda.blockLane1) AddBlocker(1, _x, _sceneWidth);
        if (vereda.blockLane2) AddBlocker(2, _x, _sceneWidth);
        if (vereda.blockLane3) AddBlocker(3, _x, _sceneWidth);
        if (vereda.blockLane4) AddBlocker(4, _x, _sceneWidth);
       


        //GameObject go = Instantiate(Resources.Load<GameObject>("backgrounds/" + name)) as GameObject;
        //backgrounds.Add(go);
        //go.transform.SetParent(background.transform);
        //go.transform.localPosition = new Vector3(_x, 0, 0);
        //if (backgrounds.Count > 2)
        //{
        //    GameObject b = backgrounds[0];
        //    Destroy(b);
        //    backgrounds.RemoveAt(0);
        //}
    }
    void AddBlocker(int laneID, int _x, int _width)
    {
        Enemy enemy = Data.Instance.enemiesManager.GetEnemy("Blocker");

        enemy.Init(new EnemySettings(), laneID);
        GameObject go = enemy.gameObject;
       // sortInLayersByLane(go, laneID);

        if (enemy == null)
            return;

        go.transform.SetParent(all[laneID].transform);
        go.transform.localPosition = new Vector3(_x + _width/2, 0, 0);
        go.transform.localScale = new Vector3(_width, 1,1 );
    }
    public void AddObjectToLane(string name, int laneId, int _x, EnemySettings settings )
    {
      //  print("new : " + name);
        Enemy enemy = null;


        switch (name)
        {
            case "CoinParticles":                
                enemy = Data.Instance.enemiesManager.GetEnemy("CoinParticles");
                enemy.GetComponent<CoinsParticles>().particles.Emit(settings.qty);
                enemy.GetComponent<CoinsParticles>().InitParticles();
                break;
            case "Skate":
                enemy = Data.Instance.enemiesManager.GetEnemy("Skate");
                break;
            case "ObstacleGeneric":
                enemy = Data.Instance.enemiesManager.GetEnemy("ObstacleGeneric");
                break;
            case "Victim":
                enemy = Data.Instance.enemiesManager.GetEnemy("Victim");
                break;
            case "RatiJump":
                enemy = Data.Instance.enemiesManager.GetEnemy("RatiJump");
                break;
            case "RatiEscudo":
                enemy = Data.Instance.enemiesManager.GetEnemy("RatiEscudo");
                break;
            case "PowerUp":
                if(Game.Instance.gameManager.characterManager.character.powerupManager.type == PowerupManager.types.NONE)
                    enemy = Data.Instance.enemiesManager.GetEnemy("PowerUp");
                break;
            case "Coin":
                enemy = Data.Instance.enemiesManager.GetEnemy("Coins");
                break;
            case "ExplosionRatis":
                enemy = Data.Instance.enemiesManager.GetEnemy("ExplosionRatis");
                break;
            case "Resorte":
                enemy = Data.Instance.enemiesManager.GetEnemy("Resorte");
                break;
            case "PowerDown":
              //  if (Game.Instance.gameManager.characterManager.character.powerupManager.type == PowerupManager.types.NONE)
                    enemy = Data.Instance.enemiesManager.GetEnemy("PowerDown");
                break;
        }
        if (enemy == null)
            return;
               
        GameObject go = enemy.gameObject;
        sortInLayersByLane(go, laneId);      

        go.transform.SetParent(all[laneId].transform);
        go.transform.localPosition = new Vector3(_x, 0, 0);
        enemy.Init(settings, laneId);

    }
    public void sortInLayersByLane(GameObject go, int laneId)
    {
         SpriteRenderer[] renderers = go.GetComponentsInChildren<SpriteRenderer>(true);
         foreach (SpriteRenderer sr in renderers)
             sr.sortingLayerName = "lane" + laneId;
    }
    public void changeEnemyLane(Enemy enemy, Lane lane)
    {
        enemy.transform.SetParent(lane.transform);
        sortInLayersByLane(enemy.gameObject, lane.id);

        Vector2 pos = enemy.transform.localPosition;  
        pos.y = 0;
        enemy.transform.localPosition = pos;

        enemy.laneId = lane.id;
    }
    public bool TryToChangeLane(bool up)
    {
        if (up && laneActiveID < all.Length - 1)
        {
            laneActiveID++;
            return true;
        } else if (!up && laneActiveID > 0)
        {
            laneActiveID--;
            return true;
        }
        return false;
    }
}
