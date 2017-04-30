using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBalloon : MonoBehaviour {

	public MeshRenderer body;

	void Start () {
		RandomPosition ();
		RandomColor ();
		RandomSize ();
	}

	void RandomPosition () {
		Border border = BorderHelper.GetBalloonBorder ();
		float x = Random.Range (border.minBorder.x, border.maxBorder.x);
		float y = Random.Range (border.minBorder.y, border.maxBorder.y);
		float z = Random.Range (border.minBorder.z, border.maxBorder.z);

		transform.localPosition = new Vector3 (x, y, z);
	}

	void RandomColor () {
		Color color = ColorHelper.GetRandomColor ();

		body.material.color = color;
		body.material.SetColor ("_EmissionColor", color);
	}

	void RandomSize () {
		float random = Random.Range (0.2f, 0.5f);

		transform.localScale = new Vector3 (random, random, random);
	}

}
