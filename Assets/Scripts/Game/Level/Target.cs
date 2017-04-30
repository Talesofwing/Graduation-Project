using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour {

	public AudioClip damageClip;

	private int marks;

	public Text marksText;
	public string targetTag;

	void OnCollisionEnter (Collision other) {
		GameObject go = other.gameObject;
		if (!go.CompareTag (targetTag))
			return;

		switch (go.tag) {
		case "Ball":
			BallRoom (go);
			break;
		case "Bullet":
			GunRoom (go);
			break;
		}

	}

	void BallRoom (GameObject ball) {
		
		if (! ball.GetComponent <Ball> ().isHit) {
			SoundManager.Instance.PlaySFX (damageClip);
			ball.GetComponent <Ball> ().isHit = true;
			marks++;
			marksText.text = marks + "";
		}

	}

	void GunRoom (GameObject bullet) {

		if (!bullet.GetComponent <Bullet> ().isHit) {
			SoundManager.Instance.PlaySFX (damageClip);
			bullet.GetComponent <Bullet> ().isHit = true;
			marks++;
			marksText.text = marks + "";
		}

	}

}
