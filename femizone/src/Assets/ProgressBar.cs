using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	public Image bar;
	Enemy enemy;
	public bool  isOn;

	public void Init(Enemy _enemy) {
		enemy = _enemy;
	}
	void Update()
	{
		if (!enemy) return;
		if (!isOn) return;

		Vector3 pos = enemy.bar.transform.position;
		Vector2 viewportPoint = Camera.main.WorldToScreenPoint(pos);  
		transform.position = Vector2.Lerp(transform.position, viewportPoint, 0.3f);
	}
	public void SetProgress(float value)
	{
		if (value < 0)
			value = 0;
		bar.fillAmount = value;
	}
	public void Hide()
	{
		isOn = false;
		transform.localPosition = new Vector3 (0, 1000, 0);
	}
	public void Show()
	{		
		Vector3 pos = enemy.bar.transform.position;
		Vector2 viewportPoint = Camera.main.WorldToScreenPoint(pos);  
		transform.position =viewportPoint;
		isOn = true;
	}


}
