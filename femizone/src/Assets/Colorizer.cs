using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
	public SpriteRenderer[] parts;
	public Color[] colors;

	void OnEnable()
	{
		if (colors.Length == 0 || parts.Length == 0)
			return;
		Color c = colors [Random.Range (0, colors.Length)];
		foreach (SpriteRenderer sr in parts) {
			sr.color = c;
		}
	}
}
