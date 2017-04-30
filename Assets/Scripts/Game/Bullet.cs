using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public bool isHit = false;

	public MeshRenderer meshRenderer;

	void Start () {
		Color color = ColorHelper.GetRandomColor ();

		meshRenderer.material.color = color;
		meshRenderer.material.SetColor ("_EmissionColor", color);
	}

}
