using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public GameObject simpleButton;
    public Text simpleButtonTitle;
    public GameObject button;
    private SwipeDetector.directions direction;
    private Character character;
    public Animator hand;
    public Text buttonField;

    public void Init()
    {
        character = Game.Instance.characterManager.character;
        simpleButton.SetActive(false);
        Events.OnSwipe += OnSwipe;
    }
    void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
    }
	public void InitSimpleButton (string title, SwipeDetector.directions direction) {

        if (direction == SwipeDetector.directions.NONE || direction == SwipeDetector.directions.LEFT)
            button.gameObject.SetActive(true);
        else button.gameObject.SetActive(false);

        this.direction = direction;
        simpleButton.SetActive(true);
        simpleButtonTitle.text = title;
        Time.timeScale = 0;

        print("direction" + direction);

        if (direction == SwipeDetector.directions.UP)
        {
            hand.gameObject.SetActive(true);
            hand.updateMode = AnimatorUpdateMode.UnscaledTime;
            hand.Play("swipeUp",0,0);
            
        }
        else if (direction == SwipeDetector.directions.DOWN)
        {
            hand.gameObject.SetActive(true);
            hand.updateMode = AnimatorUpdateMode.UnscaledTime;
            hand.Play("swipeDown", 0, 0);
        }
        else if (direction == SwipeDetector.directions.RIGHT)
        {
            hand.gameObject.SetActive(true);
            hand.updateMode = AnimatorUpdateMode.UnscaledTime;
            hand.Play("tap", 0, 0);
        }
        else if (direction == SwipeDetector.directions.LEFT)
        {
            hand.gameObject.SetActive(true);
            hand.updateMode = AnimatorUpdateMode.UnscaledTime;
            hand.Play("fuckyou", 0, 0);
            buttonField.text = "FUCK YOU!";
        }
        else
        {
            hand.gameObject.SetActive(false);
        }
	}
    void OnSwipe(SwipeDetector.directions dir)
    {
        Lanes lanes = Game.Instance.GetComponent<LevelsManager>().lanes;
        if (dir.ToString() == direction.ToString())
        {
            if (dir == SwipeDetector.directions.UP)
            {
                lanes.TryToChangeLane(true);
                character.MoveUP();
                hand.gameObject.SetActive(false);
            }
            else if (dir == SwipeDetector.directions.DOWN)
            {
                lanes.TryToChangeLane(false);
                character.MoveDown();
                hand.gameObject.SetActive(false);
            }
            else if (dir == SwipeDetector.directions.RIGHT)
            {
                character.Dash();
                hand.gameObject.SetActive(false);
            }
            SimpleButtonOk();
        }

        
    }
    public void SimpleButtonOk()
    {
        Time.timeScale = 1;
        simpleButton.SetActive(false);
        gameObject.SetActive(false);
	}
}
