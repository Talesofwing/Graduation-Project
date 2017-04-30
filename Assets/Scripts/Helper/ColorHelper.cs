using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour {

	public static Color GetRandomColor () {
		Color color = new Color (Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, 1f);

		return color;
	}

}
