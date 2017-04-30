using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBody : MonoBehaviour {

	public GameObject brokenParticle;

	public AudioClip damageClip;

	void OnTriggerEnter (Collider other) {

		switch (other.tag) {
		case "Ball":
		case "Bullet":
			GetDamage ();
			break;
		}

	}

	void GetDamage () {
		BalloonGameController.Instance.BrokenBalloon (1);

		GameObject go = Instantiate (brokenParticle, this.transform.position, this.transform.rotation) as GameObject;
		ParticleSystem.MainModule main = go.GetComponent <ParticleSystem> ().main;
		Color color = GetComponent <MeshRenderer> ().material.color;
		main.startColor = color;
		SoundManager.Instance.PlaySFX (damageClip);
		Destroy (go, 10f);
		Destroy (gameObject);
	}
}
