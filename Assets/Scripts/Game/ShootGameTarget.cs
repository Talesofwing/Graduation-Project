using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGameTarget : MonoBehaviour {
	
	public MeshRenderer body;

	public int index;

	void Start () {
		RandomColor ();
	}
	
	void RandomColor () {
		Color color = ColorHelper.GetRandomColor ();

		body.material.color = color;
		body.material.SetColor ("_EmissionColor", color);
	}

}
