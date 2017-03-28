using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSignal : MonoBehaviour {

	public float speed = 1.2f;
	public Text field;

	public void Init(string _text) {
		field.text = _text;
	}
	void Update()
	{
		Vector2 pos = transform.localPosition;
		speed += 1;
		pos.y += speed * Time.deltaTime;
		transform.localPosition = pos;
		if (pos.y > 200)
			Destroy (gameObject);
	}
}
