using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public Game game;

	void Start () {
        game = GetComponent<Game>();
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            game.Run();

        if(Input.GetKeyDown(KeyCode.Space))
            game.character.Jump();

	}
}
