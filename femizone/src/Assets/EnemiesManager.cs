using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

	public GameObject warnesMan;
	public GameObject ceoMan;
	public GameObject opusMan;
    public GameObject cop;
	public GameObject cop_gun;

    public Enemy enemy_to_instantiate;
	public Transform container;
	public List<Enemy> all;

	void Start () {
		Events.OnCharacterDie += OnCharacterDie;
    }
	void OnCharacterDie(Character character)
	{
		Enemy enemy = character.GetComponent<Enemy> ();
		if (enemy == null)
			return;
		all.Remove (enemy);
		if (all.Count == 0) {
			World.Instance.levels.StageClear ();
		}
	}
    public void InstantiateSceneOject(SceneObjectData data)
    {
        GameObject so = ceoMan;
        switch (data.type)
        {
            case SceneObjectData.types.WARNES_MAN:
                so = warnesMan;
                break;
            case SceneObjectData.types.CEO:
                so = ceoMan;
                break;
            case SceneObjectData.types.OPUS:
                so = opusMan;
                break;
            case SceneObjectData.types.COP:
                so = cop;
                break;
			case SceneObjectData.types.COP_GUN:
				so = cop_gun;
				break;
        }
        Enemy enemy = Instantiate(enemy_to_instantiate);
        enemy.transform.SetParent(container);
        enemy.Init(so);
        data.pos.y = 0;
        enemy.transform.localPosition = data.pos;
		all.Add (enemy);
    }
}
