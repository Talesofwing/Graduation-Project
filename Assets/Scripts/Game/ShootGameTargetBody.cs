using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGameTargetBody : MonoBehaviour {
	
	public GameObject brokenParticle;

	public AudioClip damageClip;

	public ShootGameTarget targetScript;

	void OnTriggerEnter (Collider other) {

		switch (other.tag) {
		case "Ball":
			
			if (other.GetComponent <Ball> ().isHit)
				return;

			other.GetComponent <Ball> ().isHit = true;
			GetDamage ();
			break;
		case "Bullet":

			if (other.GetComponent <Bullet> ().isHit)
				return;

			other.GetComponent <Bullet> ().isHit = true;
			GetDamage ();
			break;
		}

	}

	void GetDamage () {
		GameObject go = Instantiate (brokenParticle, this.transform.position, this.transform.rotation) as GameObject;
		ParticleSystem.MainModule main = go.GetComponent <ParticleSystem> ().main;
		Color color = GetComponent <MeshRenderer> ().material.color;
		main.startColor = color;
		SoundManager.Instance.PlaySFX (damageClip);
		Destroy (go, 10f);
		Destroy (gameObject);

		ShootGameController.Instance.BrokenTarget ();
	}

}
