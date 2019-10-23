using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	
	public int score;
	public states state;
	public enum states
	{
		WAITING,
		PLAYING,
        DEAD
	}
	public GameObject[] statesGO;
	public int id;
	public int life;
	public Text scoreField;
	public Text playerField;
	//public Image image;
	public Image bar;
	public int heroID;
	Color color;

	void Start()
	{
		state = states.WAITING;
		SetScore(0);
		SetState (state);
        Events.OnHeroDie += OnHeroDie;
    }
    void SetInitialState()
    {
        bar.fillAmount = 1;
        life = Data.Instance.settings.totalLife;
    }
    void OnHeroDie(int avatarID)
    {
        if (id != avatarID)
            return;
        state = states.DEAD;
    }
    private void OnDestroy()
    {
        Events.OnHeroDie -= OnHeroDie;
    }
    public void SetState(states _state)
	{
		foreach (GameObject go in statesGO)
			go.SetActive (false);
		switch (_state) {
		case states.PLAYING:
            SetInitialState();
            statesGO [0].SetActive (true);
			break;
		case states.WAITING:
			statesGO [1].SetActive (true);
			break;
		}
		state = _state;
	}
	public void Init(int id) {
		this.id = id;
		playerField.text = "P" + (id);
		color = Data.Instance.settings.colors [id-1];
		playerField.color = color;
		bar.color = color;
        SetInitialState();
    }
	public void GrabPowerUp(Powerup powerup)
	{
		if (powerup.type == Powerup.types.BIRRA)
			life += Data.Instance.settings.totalLife/3;
		else if (powerup.type == Powerup.types.BIRRA_BIG)
			life += Data.Instance.settings.totalLife/2;
		
		if (life >Data.Instance.settings.totalLife) {
			life = Data.Instance.settings.totalLife;
		} 
		bar.fillAmount = ((float)life)/(float)Data.Instance.settings.totalLife;
	}
	public void OnHeroHitted(float force)
	{
		life -= (int)force;
		if (life < 0) {
			life = 0;
			Events.OnHeroDie (id);
		} 
		bar.fillAmount = ((float)life)/(float)Data.Instance.settings.totalLife;
	}
	public void SetScore(int value)
	{
		score += value;
		string s = score.ToString();
		if(score<10)
		s = "0000" + score;
		else if(score<100)
		s = "000" + score;
		else if(score<1000)
		s = "00" + score;
		else if(score<10000)
		s = "0" + score;
		scoreField.text = s;
	}
}
