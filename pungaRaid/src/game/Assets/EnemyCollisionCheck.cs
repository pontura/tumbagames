using UnityEngine;
using System.Collections;

public class EnemyCollisionCheck : MonoBehaviour {

    public Enemy enemy;
	public bool onlyCheckEnemies;

	void Start () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {		

		if (onlyCheckEnemies) {
			
			if (other.GetComponent<Coins> ())
				return;

			Enemy _enemy = other.GetComponent<Enemy>();
			if (_enemy != null) {
				enemy.OnSecondaryCollision (other);
			}
			return;
		}
        if(other.tag == "Player")
            enemy.OnSecondaryCollision(other);
    }
}
