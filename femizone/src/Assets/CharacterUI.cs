using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {
	
	public states state;
	public enum states
	{
		WAITING,
		PLAYING
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
		SetState (state);
	}
	public void SetState(states _state)
	{
		foreach (GameObject go in statesGO)
			go.SetActive (false);
		switch (_state) {
		case states.PLAYING:
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
		life = Data.Instance.settings.totalLife;
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
}
